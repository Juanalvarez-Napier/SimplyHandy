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
    public partial class AdminPage  : Window
    {
        // variables
        private string conectionString;
        public AdminPage()
        {
            InitializeComponent();
            // giving location of database
            string destinationdb = System.AppDomain.CurrentDomain.BaseDirectory;
            conectionString = string.Format("Data Source = (LocalDB)/SimplyHandy/Bin/Debug/MSSQLLocalDB; AttachDbFilename = {0}SimplyHandy.mdf; Integrated Security = True", destinationdb);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            txtBoxName.Text = "";
            txtBoxSRU.Text = "";
            daPiDOB.Text = "";
            txtBoxPhone.Text = "";
            txtBoxEmail.Text = "";
            txtBoxSquad.Text = "";
            chkBoxParentalConsent.IsChecked = false;

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
                        sqlCon.Open(); // Opening sql conextion
                        SqlCommand command = new SqlCommand("SELECT SRUNumber, Name, DOB, Phone, Email, Squad, ParentalConsent FROM Players WHERE (Name='" + txtBoxNameSearch.Text + "' OR SRUNumber='" + txtBoxSruNSearch.Text + "');", sqlCon);
                        
                        // opening reader and populating the fields
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string col1Value = reader["SRUNumber"].ToString();
                                txtBoxSRU.Text = col1Value;
                                string col2Value = reader["Name"].ToString();
                                txtBoxName.Text = col2Value;
                                string col3Value = reader["DOB"].ToString();
                                daPiDOB.Text = col3Value;
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
                            MessageBox.Show("No information found.");
                        }
                        reader.Close();
                        sqlCon.Close();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                using (SqlConnection sqlCon = new SqlConnection(conectionString))
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Players (SRUNumber, Name, DOB, Phone, Email, Squad, ParentalConsent) Values (@SRUNumber, @Name, @DOB, @Phone, @Email, @Squad, @ParentalConsent)", sqlCon);
                    /*cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Convert.ToString(txtBoxName.Text);
                    cmd.Parameters.Add("@SRUNumber", SqlDbType.Int).Value = Convert.ToInt16(txtBoxSRU.Text);
                    cmd.Parameters.Add("@DOB", SqlDbType.VarChar).Value = Convert.ToString(daPiDOB.Text);
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Convert.ToString(txtBoxPhone.Text);
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Convert.ToString(txtBoxEmail.Text);
                    cmd.Parameters.Add("@Squad", SqlDbType.VarChar).Value = Convert.ToString(txtBoxSquad.Text);
                    cmd.Parameters.Add("@ParentalConsent", SqlDbType.VarChar).Value = Convert.ToString(chkBoxParentalConsent.IsChecked);*/
                   
                    //cmd.CommandType = CommandType.StoredProcedure;

                    // Original block
                    /*cmd.Parameters.AddWithValue("@Name", txtBoxName.Text);
                    cmd.Parameters.AddWithValue("@SRUNumber", txtBoxSRU.Text);
                    cmd.Parameters.AddWithValue("@DOB", daPiDOB.SelectedDateFormat.ToString());
                    cmd.Parameters.AddWithValue("@Phone", txtBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@Squad", txtBoxSquad.Text);
                    cmd.Parameters.AddWithValue("@ParentalConsent", chkBoxParentalConsent.IsChecked);
                    */

                    //start experimental
                    /*string name = txtBoxName.Text;
                    int sru = Convert.ToInt32(txtBoxSRU.Text);
                    string dob = daPiDOB.SelectedDateFormat.ToString();
                    int phone = 1234;
                    string email = txtBoxEmail.Text;
                    string squad = txtBoxSquad.Text;
                    string parental = chkBoxParentalConsent.IsChecked.ToString();
                    */
                    //string query = "INSERT INTO Players(SRUNumber, Name, DOB, Phone, Email, Squad) Values('"+sru+"', '"+name+"', '"+dob+"', '"+phone+ "', '"+email+ "', '"+email+"'";
                    string query = "INSERT INTO Players(SRUNumber, Name, DOB, Phone, Email, Squad) Values('vdfd', 'dfdfd', 'dfdfdf', 'dfdfdfsf', 'dfsdfs', 'fdfsdfsdf')";
                    SqlCommand cmd2 = new SqlCommand(query, sqlCon );

                    cmd2.ExecuteNonQuery();

                    // end of experimental block

                    //cmd.ExecuteNonQuery(); original

                    sqlCon.Close();
                    //MessageBox.Show("Data inserted successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
