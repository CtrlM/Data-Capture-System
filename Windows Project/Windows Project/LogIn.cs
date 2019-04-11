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

namespace Windows_Project
{
    public partial class LogIn : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);

        //
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                int countrows = 0;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SLogin";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@UserName", SqlDbType.VarChar,10).Value = txtName.Text.ToString();
                comm.Parameters.Add("@Password", SqlDbType.VarChar).Value = txtPassword.Text.ToString();
                countrows = (int)comm.ExecuteScalar();

                if (countrows > 0)
                {
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password, Please try again!", "Failed Log in", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Clear();
                    txtPassword.Clear();
                    txtName.Select();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
