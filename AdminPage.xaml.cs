using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace SimplyHandy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// /*Juan Alvarez
    /// West lothian College
    /// HND Software Development 2018/9
    /// */
    public partial class AdminPage  : Window
    {
        #region Variables
        // variables
        private string conectionString;
        #endregion

        #region Constructor
        public AdminPage() // constructor
        {
            InitializeComponent();
            // giving location of database
            string destinationdb = System.AppDomain.CurrentDomain.BaseDirectory;
            conectionString = string.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = {0}SimplyHandy.mdf; Integrated Security = True", destinationdb);
        }
        #endregion

        #region Clear Command
        private void clear() // clear command to avoid mistakes by the user when receiving the data
        {
            txtBoxName.Text = "";
            txtBoxSRU.Text = "";
            daPiDOB.Text = "";
            txtBoxPhone.Text = "";
            txtBoxEmail.Text = "";
            txtBoxSquad.Text = "";
            chkBoxParentalConsent.IsChecked = false;
            txtBoxEmails.Text = "";
        }
        #endregion

        #region Search Button
        private void btnSearch_Click(object sender, RoutedEventArgs e)  // search button
        {
            clear(); // clearing text boxes

            try
            {
                // creating sql conection
                using (SqlConnection sqlCon = new SqlConnection())
                {
                    sqlCon.ConnectionString = conectionString;
                    
                    // checks that at least one of the search text boxes are not empty
                    if (string.IsNullOrEmpty(txtBoxNameSearch.Text) && string.IsNullOrEmpty(txtBoxSruNSearch.Text))
                    {
                        MessageBox.Show("Please enter some data to search for.");

                    }
                    else
                    {
                        sqlCon.Open(); // Opening sql conection
                        SqlCommand command = new SqlCommand("SELECT SRUNumber, Name, DOB, Phone, Email, Squad, ParentalConsent FROM Players WHERE (Name='" + txtBoxNameSearch.Text + "' OR SRUNumber='" + txtBoxSruNSearch.Text + "');", sqlCon);
                        
                        // opening reader and populating the fields
                        SqlDataReader reader = command.ExecuteReader(); // opening the reader

                        if (reader.HasRows)
                        {
                            while (reader.Read())  // populating the text boxes
                            {
                                string col1Value = reader["SRUNumber"].ToString();
                                txtBoxSRU.Text = col1Value;
                                string col2Value = reader["Name"].ToString();
                                txtBoxName.Text = col2Value;
                                string col3Value = reader["DOB"].ToString();
                                daPiDOB.SelectedDate = Convert.ToDateTime(col3Value);
                                string col4Value = reader["Phone"].ToString();
                                txtBoxPhone.Text = col4Value;
                                string col5Value = reader["Email"].ToString();
                                txtBoxEmail.Text = col5Value;
                                string col6Value = reader["Squad"].ToString();
                                txtBoxSquad.Text = col6Value;
                                string col7Value = reader["ParentalConsent"].ToString(); 
                                if (col7Value == "1")
                                {
                                    chkBoxParentalConsent.IsChecked = true;
                                }
                                else
                                {
                                    chkBoxParentalConsent.IsChecked = false;
                                }
                                
                            }
                        }
                        else // if there are no records that match the search data
                        {
                            MessageBox.Show("We don't have any players in the club matching your search.");
                        }
                        reader.Close();  // closing the reader
                        sqlCon.Close();  // closing sql conection
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Button Add Player
        private void btnAdd_Click(object sender, RoutedEventArgs e) // button to add a player to db
        {

            if ((Int32.TryParse(txtBoxName.Text, out int value)) || (txtBoxName.Text.Length == 0)) // validating that the field name is not a numeric value and is not empty
            {
                MessageBox.Show("The Name field has to contain a name.");
            }
            else if (!Int32.TryParse(txtBoxSRU.Text, out int value1)) // validating that the field SRU is only a numeric value
            {
                MessageBox.Show("The SRU field can only be a number.");
            }
            else if (daPiDOB.SelectedDate == null) // validating that the field DOB is not null
            {
                MessageBox.Show("You must enter a date of birth.");
            }
            else if ((DateTime.Now.Year - daPiDOB.SelectedDate.Value.Year) < 18 && chkBoxParentalConsent.IsChecked == false)  // validating that if player is under age parental consent is needed
            {
                MessageBox.Show("The player you are trying to add is under aged, please get parental consent to store this data");
            }
            else if (!long.TryParse(txtBoxPhone.Text, out long value2) /*|| (PhoneNumber <= 9999999999)*/) // validating phone number, making sure it is a number with maximun 11 digits long, max length already set in xaml
            {
                MessageBox.Show("The Phone field has to be a valid phone number");
            }
            else if (!this.txtBoxEmail.Text.Contains('@') || !this.txtBoxEmail.Text.Contains('.')) // validating email address field
            {
                MessageBox.Show("The Email field needs to have an email");
            }
            else if (txtBoxSquad.Text.Length == 0) // validating that the field Squad is not empty
            {
                MessageBox.Show("The Squad field has to be filled in.");
            }
            else try
                {

                    using (SqlConnection sqlCon = new SqlConnection(conectionString)) // conecting to database
                    {
                        sqlCon.ConnectionString = conectionString;
                        sqlCon.Open();  // opening sql conection
                        SqlCommand cmd = new SqlCommand("INSERT INTO Players (SRUNumber, Name, DOB, Phone, Email, Squad, ParentalConsent) Values (@SRUNumber, @Name, @DOB, @Phone, @Email, @Squad, @ParentalConsent)", sqlCon);  // sql query
                        
                        // populating the database
                        cmd.Parameters.AddWithValue("@Name", txtBoxName.Text);
                        cmd.Parameters.AddWithValue("@SRUNumber", txtBoxSRU.Text);
                        cmd.Parameters.AddWithValue("@DOB", daPiDOB.SelectedDate);
                        cmd.Parameters.AddWithValue("@Phone", txtBoxPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtBoxEmail.Text);
                        cmd.Parameters.AddWithValue("@Squad", txtBoxSquad.Text);
                        cmd.Parameters.AddWithValue("@ParentalConsent", chkBoxParentalConsent.IsChecked);

                        cmd.ExecuteNonQuery();  // executing sql query
                        MessageBox.Show("Player data saved successfully");
                        //}

                        sqlCon.Close(); // closing sql conection
                        clear();  // clearing the fields

                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("There is already a player in the system with that SRU Number.");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }


        
        #endregion

        #region Button Edit Player
        private void btnUpdate_Click(object sender, RoutedEventArgs e) // button to update details of a player
        {
            // I tried to validate the phone number so it was only 11 digits, failled, managed to make it just numbers
            // and ensuring it is maximun 11 numbers long (this one with xmal code) with more time this will be fixed
            /*long.TryParse(txtBoxPhone.Text, out long PhoneNumber); // needed for phone number validation*/

            if ((Int32.TryParse(txtBoxName.Text, out int value)) || (txtBoxName.Text.Length == 0)) // validating that the field name is not a numeric value and is not empty
            {
                MessageBox.Show("The Name field has to contain a name.");
            }
            else if (!Int32.TryParse(txtBoxSRU.Text, out int value1)) // validating that the field SRU is only a numeric value
            {
                MessageBox.Show("The SRU field can only be a number.");
            }
            else if (daPiDOB.SelectedDate == null) // validating that the field DOB is not null
            {
                MessageBox.Show("You must enter a date of birth.");
            }
            else if ((DateTime.Now.Year - daPiDOB.SelectedDate.Value.Year) < 18 && chkBoxParentalConsent.IsChecked == false)  // validating that if player is under age parental consent is needed
            {
                MessageBox.Show("The player you are trying to add is under aged, please get parental consent to store this data");
            }
            else if (!long.TryParse(txtBoxPhone.Text, out long value2) /*|| (PhoneNumber <= 9999999999)*/) // validating phone number, making sure it is a number with maximun 11 digits long, max length already set in xaml
            {
                MessageBox.Show("The Phone field has to be a valid phone number");
            }
            else if (!this.txtBoxEmail.Text.Contains('@') || !this.txtBoxEmail.Text.Contains('.')) // validating email address field
            {
                MessageBox.Show("The Email field needs to have an email");
            }
            else if (txtBoxSquad.Text.Length == 0) // validating that the field Squad is not empty
            {
                MessageBox.Show("The Squad field has to be filled in.");
            }
            else try
            {
                    // assigning conection to db
                using (SqlConnection sqlCon = new SqlConnection(conectionString))
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open();  // opening sql conection
                    SqlCommand cmd = new SqlCommand("UPDATE Players SET Name = @Name, DOB = @DOB, Phone = @Phone, Email =@Email, Squad = @Squad, ParentalConsent = @ParentalConsent WHERE SRUNumber= @SRUNumber ", sqlCon);  // query to update player's details

                    // assigning values to the query
                    cmd.Parameters.AddWithValue("@Name", txtBoxName.Text);
                    cmd.Parameters.AddWithValue("@SRUNumber", txtBoxSRU.Text);
                    cmd.Parameters.AddWithValue("@DOB", daPiDOB.SelectedDate);
                    cmd.Parameters.AddWithValue("@Phone", txtBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@Squad", txtBoxSquad.Text);
                    cmd.Parameters.AddWithValue("@ParentalConsent", chkBoxParentalConsent.IsChecked);

                    cmd.ExecuteNonQuery();  // executing query

                    sqlCon.Close();  // closing conection to database
                    clear();  // clearing text boxes
                    MessageBox.Show("Player data updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Button Delete Player
        private void btnDelete_Click(object sender, RoutedEventArgs e) // button to delete a player
        {
            try
            {

                // confirming that admin wants to delete player
                if (MessageBox.Show("Are you sure you want to delete this player?", "Deleting a player", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return; // back to program
                }
                else    using (SqlConnection sqlCon = new SqlConnection(conectionString))  // actually deleting the player from here
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open(); // opening sql conection
                    SqlCommand cmd = new SqlCommand("DELETE FROM Players WHERE SRUNumber= @SRUNumber ", sqlCon); // sql query to delete player

                    cmd.Parameters.AddWithValue("@SRUNumber", txtBoxSRU.Text);

                    cmd.ExecuteNonQuery(); // execute queries

                    sqlCon.Close(); // Closing sql conection
                    clear(); // clearing text boxes
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Button to show all Emails
        private void btnEmails_Click(object sender, RoutedEventArgs e) // button to show all emails from the db
        {
            clear(); // clearing the text boxes
            try
            {
                
                using (SqlConnection sqlCon = new SqlConnection(conectionString))
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open();   // opening sql conection
                    SqlCommand cmd = new SqlCommand("SELECT Email FROM Players", sqlCon);  // sql query for showing emails

                    cmd.ExecuteNonQuery(); // executing sql query

                    SqlDataReader reader = cmd.ExecuteReader(); // opening data reader

                    if (reader.HasRows)
                    {
                        while (reader.Read()) // while there are records on db it will keep adding emails
                        {
                            string colEmail = reader["Email"].ToString();
                            txtBoxEmails.Text += colEmail +"\n"; // ensuring the emails are showed in different lines
                            
                        }
                    }
                    else // if there are no records that match the search data
                    {
                        MessageBox.Show("No information found.");
                    }
                    reader.Close(); // closing reader
                    sqlCon.Close(); // closing sql conection
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Exit Button
        private void btnExit_Click(object sender, RoutedEventArgs e)  // exiting back to the login page
        {
            LoginIn dashboard = new LoginIn();
            dashboard.Show();
            this.Close();
        }
        #endregion
    }
}
