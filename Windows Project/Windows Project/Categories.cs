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
    public partial class Categories : Form
    {

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);
        // hold id number
        int categoryID;
        // For add or edit decider
        bool isAdd = false;
        public Categories()
        {
            InitializeComponent();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            //Load data to combo on load form
            loadcboCategories();
        }

        // method to gather info for combox read data to combobox
        private void loadcboCategories()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboCategories.DataSource = null;
                cboCategories.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectCategories";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblCat = new DataTable();
                tblCat.Load(rd);
                cboCategories.DataSource = tblCat;
                cboCategories.DisplayMember = "CategoryName";
                cboCategories.ValueMember = "CategoryID";

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            txtDescription.Clear();
            txtName.Clear();
            txtName.Select();
            
        }

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAdd = false;
            if (cboCategories.SelectedIndex > 0)
            {
                loadCategoryDetails();
            }
        }

        //method Load details onto form
        private void loadCategoryDetails()
        {

            try
            {
                categoryID = int.Parse(cboCategories.SelectedValue.ToString());
                SqlDataReader rd;
                //Load data of selected record
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SSelectCategoryByID";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
                rd = comm.ExecuteReader();

                while (rd.Read())
                {
                    txtName.Text = rd["CategoryName"].ToString();
                    txtDescription.Text = rd["Description"].ToString();
                }
                if (rd.IsClosed == false)
                    rd.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Validate data
        private bool ValidateData()
        {
            try
            {
                if (txtName.Text == "")
                {
                    err.SetError(txtName, "Please enter a Category Name");
                    return false;
                }
                else
                    err.SetError(txtName, "");
                if (txtDescription.Text == "")
                {
                    err.SetError(txtDescription, "Please enter a Description");
                    return false;
                }
                else
                    err.SetError(txtDescription, "");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Method insert 
        private void InsertCategory()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SInsertCategory";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 15).Value = txtName.Text.ToString();
                comm.Parameters.Add("@Description", SqlDbType.NText).Value = txtDescription.Text.ToString();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Method Update
        private void UpdateCategory()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SUpdateCategory";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
                comm.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 15).Value = txtName.Text.Trim();
                comm.Parameters.Add("@Description", SqlDbType.NText).Value = txtDescription.Text.Trim();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
                return;
            if (isAdd == true)
                InsertCategory();
            else
                UpdateCategory();
            MessageBox.Show("Record successfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadcboCategories();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

