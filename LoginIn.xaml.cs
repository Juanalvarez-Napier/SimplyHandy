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

namespace SimplyHandy
{
    /// <summary>
    /// Interaction logic for LoginIn.xaml
    /// </summary>
    /// /*Juan Alvarez
    /// West lothian College
    /// HND Software Development 2018/9
    /// */
    public partial class LoginIn : Window
    {
        #region Variables
        private string conectionString;
        #endregion

        #region Constructor
        public LoginIn()
        {
            InitializeComponent();
            string destinationdb = System.AppDomain.CurrentDomain.BaseDirectory;
            conectionString = string.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = {0}SimplyHandy.mdf; Integrated Security = True", destinationdb);
        }
        #endregion

        #region Login Button
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                
                using (SqlConnection sqlCon = new SqlConnection())
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open();


                    String query = "SELECT COUNT(1) FROM logindetails WHERE username=@username AND Password=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        if (txtUserName.Text == "Admin")
                        {
                            AdminPage dashboard = new AdminPage();
                            sqlCon.Close();
                            dashboard.Show();
                            this.Close();
                        }
                        else if (txtUserName.Text == "Coach")
                        {
                            CoachPage dashboard = new CoachPage();
                            sqlCon.Close();
                            dashboard.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        txtUserName.Text = "";
                        txtPassword.Password = "";
                        MessageBox.Show("Username or Password is incorrect.");
                    }
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
