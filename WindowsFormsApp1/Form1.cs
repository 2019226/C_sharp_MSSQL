using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string connectionString = "Data Source=DESKTOP-OOJCSSL;" +
                              "Initial Catalog=test;" +
                              "Integrated Security=SSPI;";
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            /*連線後將顯示資料庫狀態在label1上*/
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                label1.Text = "State:" + connection.State
                    + "\n"
                    + connection.ConnectionString
                    + "\n" + connection.ServerVersion;
                connection.Close();
            }

            button2.Enabled = true;
            button3.Enabled = true;

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            /*新增*/
            using (SqlConnection connection = new SqlConnection())
            {
                string command_string ="Insert into[dbo].[Table](message_data, datatime) Values('"
                    + textBox1.Text
                    +"', '"
                    + DateTime.Now.ToString()
                    + "')";
                
                SqlCommand command = new SqlCommand(command_string, connection);
                connection.ConnectionString = connectionString;
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("已送出!!");
                }
                catch (Exception)
                {
                    throw;
                }
               
                
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*查詢資料庫內的資料*/
            using (SqlConnection connection = new SqlConnection())
            {
                string command_string = "SELECT * FROM [dbo].[Table];";
                SqlCommand command = new SqlCommand(command_string, connection);
                connection.ConnectionString = connectionString;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    textBox2.Text = "";
                    while (reader.Read())
                    {
                        textBox2.Text += String.Format("{0}\t\t{1}"+"\r\n",
                            reader[1], reader[2]);
                    }
                }
                connection.Close();
            }
        }
    }

}
