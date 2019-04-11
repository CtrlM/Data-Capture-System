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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);
        int UserID;
        int UserRole;
        bool isAdd = false;
        ErrorProvider err = new ErrorProvider();

        private void loadcboUser()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboUser.DataSource = null;
                cboUser.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectUsers";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblUsers = new DataTable();
                tblUsers.Load(rd);
                cboUser.DataSource = tblUsers;
                cboUser.DisplayMember = "UserName";
                cboUser.ValueMember = "Pk_UserID";

                if (rd.IsClosed == false)
                {
                    rd.Close();
                }
            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void loadcboUserRole()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboUserRole.DataSource = null;
                cboUserRole.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectUserRoles";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblUsers = new DataTable();
                tblUsers.Load(rd);
                cboUserRole.DataSource = tblUsers;
                cboUserRole.DisplayMember = "UserRole";
                cboUserRole.ValueMember = "Pk_UserRoleId";

                if (rd.IsClosed == false)
                {
                    rd.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadUserDetails()
        {
            try
            {
                UserID = int.Parse(cboUser.SelectedValue.ToString());
                txtID.Text = UserID.ToString();
                txtUserName.Text = cboUser.Text;

               
                SqlDataReader rd;
               
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SelectUsersById";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                rd = comm.ExecuteReader();

                while (rd.Read())
                {
                    txtID.Text = UserID.ToString();
                    txtName.Text = rd["U.UserFirstName"].ToString();
                    txtSurname.Text = rd["U.UserLastName"].ToString();
                    txtPassword.Text = rd["U.UserPassword"].ToString();
                    cboUserRole.Text = rd["UR.UserRole"].ToString();
                    
                }

                if (rd.IsClosed == false)
                
                    rd.Close();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Exception Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void Users_Load(object sender, EventArgs e)
        {
            loadcboUser();
            loadcboUserRole();
        }

        private void InsertUser()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_InsertUsers";
                comm.CommandType = CommandType.StoredProcedure;
                //comm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserID;
                comm.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = txtUserName.Text.Trim();
                comm.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = txtName.Text.Trim();
                comm.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = txtSurname.Text.Trim();
                comm.Parameters.Add("@Pass", SqlDbType.VarChar, 8).Value = txtPassword.Text.Trim();
                comm.Parameters.Add("@RoleID", SqlDbType.Int).Value = cboUserRole.SelectedValue;
                comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            isAdd = false;

        }
        private void UpdateUser()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_UpdateUsers";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@PK_UserID", SqlDbType.Int).Value = UserID;
                comm.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = txtName.Text.Trim();
                comm.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = txtSurname.Text.Trim();
                comm.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = txtUserName.Text.Trim();
                comm.Parameters.Add("@Pass", SqlDbType.VarChar, 8).Value = txtPassword.Text.Trim();
                comm.Parameters.Add("@RoleID", SqlDbType.Int).Value = UserRole;

                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAdd = false;
                loadUserDetails();
          
        }

      
        private bool validateData()
        {
            try
            {
                if (txtName.Text == "")
                {
                    err.SetError(txtName, "Please enter a First Name");//make error provider flash next to txtDescription
                    return false;
                }
                else
                {
                    err.SetError(txtName, "");
                }

                if (txtSurname.Text == "")
                {
                    err.SetError(txtSurname, "Please enter a Last Name");//make error provider flash next to txtDescription
                    txtSurname.Select();
                    return false;
                }
                else
                {
                    err.SetError(txtSurname, "");//clear the error provider
                }

                if (txtName.Text == "")
                {
                    err.SetError(txtUserName, "Please enter a Username");//make error provider flash next to txtDescription
                    return false;
                }
                else
                {
                    err.SetError(txtUserName, "");
                }

                if (txtPassword.Text == "")
                {
                    err.SetError(txtPassword, "Please enter a Password");//make error provider flash next to txtDescription
                    return false;
                }
                else
                {
                    err.SetError(txtPassword, "");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (validateData() == false)
                return;

            if (isAdd == true)
                InsertUser();
            else
                UpdateUser();
            MessageBox.Show("Record succesfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadcboUser();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            txtID.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtPassword.Clear();
            txtName.Select();
        }
    }
}
