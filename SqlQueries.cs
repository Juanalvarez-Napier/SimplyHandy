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
    class SqlQueries : Window
    {/*

         //start experimental
                    string name = txtBoxName.Text;
                    string sru = txtBoxSRU.Text;
                    string dob = daPiDOB.SelectedDateFormat.ToString();
                    string phone = txtBoxPhone.Text;
                    string email = txtBoxEmail.Text;
                    string squad = txtBoxSquad.Text;
                    string parental = chkBoxParentalConsent.IsChecked.ToString();
                    
                    string query = "INSERT INTO Players(SRUNumber, Name, DOB, Phone, Email, Squad) Values('"+sru+"', '"+name+"', '"+dob+"', '"+phone+ "', '"+email+ "', '"+squad+"')";
                    //string query = "INSERT INTO Players(SRUNumber, Name, DOB, Phone, Email, Squad) Values('vdfd', 'dfdfd', 'dfdfdf', 'dfdfdfsf', 'dfsdfs', 'fdfsdfsdf')";
                    SqlCommand cmd2 = new SqlCommand(query, sqlCon );

                    cmd2.ExecuteNonQuery();

        // end of experimental block

        Here as a backup
         cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Convert.ToString(txtBoxName.Text);
                    cmd.Parameters.Add("@SRUNumber", SqlDbType.Int).Value = Convert.ToInt16(txtBoxSRU.Text);
                    cmd.Parameters.Add("@DOB", SqlDbType.VarChar).Value = Convert.ToString(daPiDOB.Text);
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Convert.ToString(txtBoxPhone.Text);
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Convert.ToString(txtBoxEmail.Text);
                    cmd.Parameters.Add("@Squad", SqlDbType.VarChar).Value = Convert.ToString(txtBoxSquad.Text);
                    cmd.Parameters.Add("@ParentalConsent", SqlDbType.VarChar).Value = Convert.ToString(chkBoxParentalConsent.IsChecked);

        //cmd.CommandType = CommandType.StoredProcedure;

        // variables
        private string conectionString;
        public SqlQueries()
        {
            //InitializeComponent();
            // giving location of database
            string destinationdb = System.AppDomain.CurrentDomain.BaseDirectory;
            conectionString = string.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = {0}SimplyHandy.mdf; Integrated Security = True", destinationdb);
        }

        public void AdminSearch()
        {
            try
            {
                // creating sql conection
                using (SqlConnection sqlCon = new SqlConnection())
                {
                    sqlCon.ConnectionString = conectionString;
                    sqlCon.Open(); // Opening sql conextion

                    if (AdminPage.txtBoxNameSearch.TextLength >= 1 && (AdminPage.txtBoxSruNSearch.TextLength >= 1)
                    {
                        MessageBox.Show("This will work when you put it a word!");

                        // Search code //

                    }
                }
            }
            }
        }*/
    }
}
