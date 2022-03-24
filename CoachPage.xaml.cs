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
    /// Interaction logic for CoachPage.xaml
    /// </summary>
    /// /*Juan Alvarez
    /// West lothian College
    /// HND Software Development 2018/9
    /// */
    public partial class CoachPage : Window
    {
        // variables
        private string conectionString;
        public CoachPage()
        {
            InitializeComponent();
            // giving location of database
            string destinationdb = System.AppDomain.CurrentDomain.BaseDirectory;
            conectionString = string.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = {0}SimplyHandy.mdf; Integrated Security = True", destinationdb);
        }
        #region Clear Command
        // clear command to ensure all fields are cleared
        private void clear()
        {
            txtBlockName.Text = "";
            txtBlockSRU.Text = "";
            daPiDOB.Text = "";
            txtBlockPhone.Text = "";
            txtBlockEmail.Text = "";
            txtBlockSquad.Text = "";
            chkBoxParentalConsent.IsChecked = false;
            txtBoxCommentsKicking.Text = "";
            txtBoxCommentsPassing.Text = "";
            txtBoxCommentsTackling.Text = "";
            txtBoxKicDrop.Text = "";
            txtBoxKicGoal.Text = "";
            txtBoxKicGrubber.Text = "";
            txtBoxKicPunt.Text = "";
            txtBlockDate.Text = "";
            txtBoxPassPop.Text = "";
            txtBoxPassSpin.Text = "";
            txtBoxPassStandard.Text = "";
            txtBoxTacFront.Text = "";
            txtBoxTacRear.Text = "";
            txtBoxTacScrabble.Text = "";
            txtBoxTacSide.Text = "";
            
            
        }
        #endregion

        #region Search Button
        // Search button
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            clear(); // clearing text boxes

            try
            {
                // creating sql conection
                using (SqlConnection sqlCon = new SqlConnection())
                {
                    sqlCon.ConnectionString = conectionString;
                    //sqlCon.Open(); // Opening sql conextion

                    // checks that the text boxes are not empty
                    if (string.IsNullOrEmpty(txtBoxNameSearch.Text) && string.IsNullOrEmpty(txtBoxSruNSearch.Text))
                    {
                        MessageBox.Show("Please enter some data to search for.");

                    }
                    else
                    {
                        sqlCon.Open(); // Opening sql conection
                        SqlCommand command = new SqlCommand("SELECT SRUNumber, Name, DOB, Phone, Email, Squad, ParentalConsent, Standard, Spin, Pop, PassingComments, Front, Rear, Side, Scrabble, TacklingComments, KDrop, Punt, Grubber, Goal, KickingComments, Date FROM Players WHERE (Name='" + txtBoxNameSearch.Text + "' OR SRUNumber='" + txtBoxSruNSearch.Text + "');", sqlCon);

                        // opening reader and populating the fields
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string col1Value = reader["SRUNumber"].ToString();
                                txtBlockSRU.Text = col1Value;
                                string col2Value = reader["Name"].ToString();
                                txtBlockName.Text = col2Value;
                                string col3Value = reader["DOB"].ToString();
                                daPiDOB.SelectedDate = Convert.ToDateTime(col3Value);
                                string col4Value = reader["Phone"].ToString();
                                txtBlockPhone.Text = col4Value;
                                string col5Value = reader["Email"].ToString();
                                txtBlockEmail.Text = col5Value;
                                string col6Value = reader["Squad"].ToString();
                                txtBlockSquad.Text = col6Value;
                                string col7Value = reader["ParentalConsent"].ToString();
                                if (col7Value == "1")
                                {
                                    chkBoxParentalConsent.IsChecked = true;
                                }
                                else
                                {
                                    chkBoxParentalConsent.IsChecked = false;
                                }
                                string col8Value = reader["Standard"].ToString();
                                txtBoxPassStandard.Text = col8Value;
                                string col9Value = reader["Spin"].ToString();
                                txtBoxPassSpin.Text = col9Value;
                                string col10Value = reader["Pop"].ToString();
                                txtBoxPassPop.Text = col10Value;
                                string col11Value = reader["PassingComments"].ToString();
                                txtBoxCommentsPassing.Text = col11Value;
                                string col12Value = reader["Front"].ToString();
                                txtBoxTacFront.Text = col12Value;
                                string col13Value = reader["Rear"].ToString();
                                txtBoxTacRear.Text = col13Value;
                                string col14Value = reader["Scrabble"].ToString();
                                txtBoxTacSide.Text = col14Value;
                                string col21Value = reader["Side"].ToString();
                                txtBoxTacScrabble.Text = col21Value;
                                string col15Value = reader["TacklingComments"].ToString();
                                txtBoxCommentsTackling.Text = col15Value;
                                string col16Value = reader["KDrop"].ToString();
                                txtBoxKicDrop.Text = col16Value;
                                string col17Value = reader["Punt"].ToString();
                                txtBoxKicPunt.Text = col17Value;
                                string col18Value = reader["Grubber"].ToString();
                                txtBoxKicGrubber.Text = col18Value;
                                string col19Value = reader["Goal"].ToString();
                                txtBoxKicGoal.Text = col19Value;
                                string col20Value = reader["KickingComments"].ToString();
                                txtBoxCommentsKicking.Text = col20Value;
                                string col22Value = reader["Date"].ToString();
                                txtBlockDate.Text = col22Value;
                                

                            }
                        }
                        else // if there are no records that match the search data
                        {
                            MessageBox.Show("There are no players that match your search.");
                        }
                        reader.Close();  // closing reader
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

        #region Exit Button
        private void btnExit_Click(object sender, RoutedEventArgs e)  // exit button
        {
            LoginIn dashboard = new LoginIn();  // sending back to login page
            dashboard.Show();
            this.Close();
        }
        #endregion

        #region Save button
        private void btnSave_Click(object sender, RoutedEventArgs e) // save button to update player details
        {
            // all fields relating to performance (0 to 5) are limited in xaml to just 1 character
            if (!Int32.TryParse(txtBoxPassStandard.Text, out int value1) || value1 < 0 || value1 > 5) // validating that the field Standard is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Standard field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxPassSpin.Text, out int value2) || value2 < 0 || value2 > 5) // validating that the field Spin is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Spin field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxPassPop.Text, out int value3) || value3 < 0 || value3 > 5) // validating that the field Pop is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Pop field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxTacFront.Text, out int value4) || value4 < 0 || value4 > 5) // validating that the field Front is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Front field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxTacRear.Text, out int value5) || value5 < 0 || value5 > 5) // validating that the field Rear is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Rear field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxTacSide.Text, out int value6) || value6 < 0 || value6 > 5) // validating that the field Side is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Side field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxTacScrabble.Text, out int value7) || value7 < 0 || value7 > 5) // validating that the field Scrabble is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Scrabble field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxKicDrop.Text, out int value8) || value8 < 0 || value8 > 5) // validating that the field Drop is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Drop field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxKicPunt.Text, out int value9) || value9 < 0 || value9 > 5) // validating that the field Punt is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Punt field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxKicGrubber.Text, out int value10) || value10 < 0 || value10 > 5) // validating that the field Grubber is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Grubber field can only be a number between 0 and 5.");
            }
            else if (!Int32.TryParse(txtBoxKicGoal.Text, out int value11) || value11 < 0 || value11 > 5) // validating that the field Grubber is only a numeric value between 0 and 5
            {
                MessageBox.Show("The Goal field can only be a number between 0 and 5.");
            }
            else try
            {

                using (SqlConnection sqlCon = new SqlConnection(conectionString))
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open();  // opening sql conection
                    SqlCommand cmd = new SqlCommand("UPDATE Players SET Standard = @Standard, Spin = @Spin, Pop = @Pop, PassingComments =@PassingComments, Front = @Front, Rear = @Rear, Scrabble = @Scrabble, TacklingComments = @TacklingComments, KDrop = @KDrop, Punt = @Punt, Grubber = @Grubber, Goal = @Goal, KickingComments = @KickingComments, Side = @Side, Date = @Date WHERE SRUNumber= @SRUNumber ", sqlCon);  // sql query to update player's details

                    // populating the query
                    cmd.Parameters.AddWithValue("@Standard", Convert.ToInt16(txtBoxPassStandard.Text));
                    cmd.Parameters.AddWithValue("@SRUNumber", txtBlockSRU.Text);
                    cmd.Parameters.AddWithValue("@Spin", Convert.ToInt16(txtBoxPassSpin.Text));
                    cmd.Parameters.AddWithValue("@Pop", Convert.ToInt16(txtBoxPassPop.Text));
                    cmd.Parameters.AddWithValue("@PassingComments", txtBoxCommentsPassing.Text);
                    cmd.Parameters.AddWithValue("@Front", Convert.ToInt16(txtBoxTacFront.Text));
                    cmd.Parameters.AddWithValue("@Rear", Convert.ToInt16(txtBoxTacRear.Text));
                    cmd.Parameters.AddWithValue("@Side", Convert.ToInt16(txtBoxTacSide.Text));
                    cmd.Parameters.AddWithValue("@Scrabble", Convert.ToInt16(txtBoxTacScrabble.Text));
                    cmd.Parameters.AddWithValue("@TacklingComments", txtBoxCommentsTackling.Text);
                    cmd.Parameters.AddWithValue("@KDrop", Convert.ToInt16(txtBoxKicDrop.Text));
                    cmd.Parameters.AddWithValue("@Punt", Convert.ToInt16(txtBoxKicPunt.Text));
                    cmd.Parameters.AddWithValue("@Grubber", Convert.ToInt16(txtBoxKicGrubber.Text));
                    cmd.Parameters.AddWithValue("@Goal", Convert.ToInt16(txtBoxKicGoal.Text));
                    cmd.Parameters.AddWithValue("@KickingComments", txtBoxCommentsKicking.Text);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtBoxLastEditDate.Text)); // originally coach had to input date, now date gets saved automatically every time coach updates player

                    cmd.ExecuteNonQuery();  // executing the query
                    sqlCon.Close(); // closing sql conection
                       
                    clear();  // clearing screen
                    MessageBox.Show("Player data updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
