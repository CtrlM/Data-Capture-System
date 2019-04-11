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
    public partial class frmSuppliers : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);
        int SupplierID;
        bool isAdd = false;

        private void loadcboSupplier()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboSupplier.DataSource = null;
                cboSupplier.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SelectSuppliers";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblSup = new DataTable();
                tblSup.Load(rd);
                cboSupplier.DataSource = tblSup;
                cboSupplier.DisplayMember = "CompanyName";
                cboSupplier.ValueMember = "SupplierID";

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


        public frmSuppliers()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAdd = false;
            if (cboSupplier.SelectedIndex > 0)
                loadSupplierDetails();
        }

        private bool ValidateData()
        {
            try
            {
                if (txtID.Text == "")
                {
                    err.SetError(txtID, "Please enter a Supplier ID");
                    return false;
                }
                else
                    err.SetError(txtID, "");
                if (txtName.Text == "")
                {
                    err.SetError(txtName, "Please enter a Company Name");
                    return false;
                }
                else
                    err.SetError(txtName, "");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void frmSuppliers_Load(object sender, EventArgs e)
        {
            loadcboSupplier();
        }

        private void UpdateSupplier()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SUpdateSuppliers";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@SupplierID", SqlDbType.NChar, 5).Value = SupplierID;
                comm.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40).Value = txtName.Text.ToString();
                comm.Parameters.Add("@ContactName", SqlDbType.NVarChar, 30).Value = txtContact.Text.ToString();
                comm.Parameters.Add("@ContactItile", SqlDbType.NVarChar, 30).Value = txtTitle.Text.ToString();
                comm.Parameters.Add("@Address", SqlDbType.NVarChar, 60).Value = txtAddress.Text.ToString();
                comm.Parameters.Add("@City", SqlDbType.NVarChar, 15).Value = txtCity.Text.ToString();
                comm.Parameters.Add("@Region", SqlDbType.NVarChar, 15).Value = txtRegion.Text.ToString();
                comm.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10).Value = txtPostal.Text.ToString();
                comm.Parameters.Add("@Country", SqlDbType.NVarChar, 15).Value = txtCountry.Text.ToString();
                comm.Parameters.Add("@Phone", SqlDbType.NVarChar, 25).Value = txtPhone.Text.ToString();
                comm.Parameters.Add("@Fax", SqlDbType.NVarChar, 25).Value = txtFax.Text.ToString();
                comm.Parameters.Add("@HomePage", SqlDbType.NVarChar).Value = txtHomepage.Text.ToString();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void InsertSupplier()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SInsertSuppliers";
                comm.CommandType = CommandType.StoredProcedure;
               
                comm.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40).Value = txtName.Text.ToString();
                comm.Parameters.Add("@ContactName", SqlDbType.NVarChar, 30).Value = txtContact.Text.ToString();
                comm.Parameters.Add("@ContactTitle", SqlDbType.NVarChar, 30).Value = txtTitle.Text.ToString();
                comm.Parameters.Add("@Address", SqlDbType.NVarChar, 60).Value = txtAddress.Text.ToString();
                comm.Parameters.Add("@City", SqlDbType.NVarChar, 15).Value = txtCity.Text.ToString();
                comm.Parameters.Add("@Region", SqlDbType.NVarChar, 15).Value = txtRegion.Text.ToString();
                comm.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10).Value = txtPostal.Text.ToString();
                comm.Parameters.Add("@Country", SqlDbType.NVarChar, 15).Value = txtCountry.Text.ToString();
                comm.Parameters.Add("@Phone", SqlDbType.NVarChar, 25).Value = txtPhone.Text.ToString();
                comm.Parameters.Add("@Fax", SqlDbType.NVarChar, 25).Value = txtFax.Text.ToString();
                comm.Parameters.Add("@HomePage", SqlDbType.NVarChar).Value = txtHomepage.Text.ToString();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void loadSupplierDetails()
        {
            try
            {

                SupplierID = int.Parse(cboSupplier.SelectedValue.ToString());

                txtID.Text = SupplierID.ToString();
                txtName.Text = cboSupplier.Text;

                SqlDataReader rd;
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SSelectSuppliersById";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID;
                rd = comm.ExecuteReader();

                while (rd.Read())
                {

                    txtContact.Text = rd["ContactName"].ToString();
                    txtTitle.Text = rd["ContactTitle"].ToString();
                    txtAddress.Text = rd["Address"].ToString();
                    txtCity.Text = rd["City"].ToString();
                    txtRegion.Text = rd["Region"].ToString();
                    txtPostal.Text = rd["PostalCode"].ToString();
                    txtCountry.Text = rd["Country"].ToString();
                    txtPhone.Text = rd["Phone"].ToString();
                    txtFax.Text = rd["Fax"].ToString();
                    txtHomepage.Text = rd["HomePage"].ToString();

                }

                if (rd.IsClosed == false) { 
                rd.Close(); }
               
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            txtPhone.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtContact.Clear();
            txtCountry.Clear();
            txtFax.Clear();
            txtName.Clear();
            txtPostal.Clear();
            txtRegion.Clear();
            txtTitle.Clear();
            txtID.Clear();
            txtID.Select();
            txtHomepage.Clear();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
                return;
            if (isAdd == true)
            {
                InsertSupplier();

                MessageBox.Show("Record successfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadcboSupplier();
            }
            else
            {
                UpdateSupplier();

                MessageBox.Show("Record successfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadcboSupplier();
            }
            
          
        }
    }
}
