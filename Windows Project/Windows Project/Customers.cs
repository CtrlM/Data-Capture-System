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
    public partial class Customers : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);
        string CustomerID;
        bool isAdd = false;
        public Customers()
        {
            InitializeComponent();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            loadCustomers();
        }
        private void loadCustomers()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboCompany.DataSource = null;
                cboCompany.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectCustomers";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblCus = new DataTable();
                tblCus.Load(rd);
                cboCompany.DataSource = tblCus;
                cboCompany.DisplayMember = "CompanyName";
                cboCompany.ValueMember = "CustomerID";

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
        private bool ValidateData()
        {
            try
            {
                if (txtID.Text == "")
                {
                    err.SetError(txtID, "Please enter a ID");
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
        }

        private void InsertCustomer()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SInsertCustomer";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@CustomerID", SqlDbType.NChar, 5).Value = txtID.Text.ToString();
                comm.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40).Value = txtID.Text.ToString();
                comm.Parameters.Add("@ContactName", SqlDbType.NVarChar, 30).Value = txtContact.Text.ToString();
                comm.Parameters.Add("@ContactTitle", SqlDbType.NVarChar, 30).Value = txtTitle.Text.ToString();
                comm.Parameters.Add("@Address", SqlDbType.NVarChar, 60).Value = txtAddress.Text.ToString();
                comm.Parameters.Add("@City", SqlDbType.NVarChar, 15).Value = txtCity.Text.ToString();
                comm.Parameters.Add("@Region", SqlDbType.NVarChar, 15).Value = txtRegion.Text.ToString();
                comm.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10).Value = txtPostal.Text.ToString();
                comm.Parameters.Add("@Country", SqlDbType.NVarChar, 15).Value = txtCountry.Text.ToString();
                comm.Parameters.Add("@Phone", SqlDbType.NVarChar, 24).Value = txtPhone.Text.ToString();
                comm.Parameters.Add("@Fax", SqlDbType.NVarChar, 24).Value = txtFax.Text.ToString();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateCustomer()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SUpdateCustomer";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@CustID", SqlDbType.NChar, 5).Value = CustomerID;
                comm.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40).Value = txtID.Text.ToString();
                comm.Parameters.Add("@ContactName", SqlDbType.NVarChar, 30).Value = txtContact.Text.ToString();
                comm.Parameters.Add("@ContactTitle", SqlDbType.NVarChar, 30).Value = txtTitle.Text.ToString();
                comm.Parameters.Add("@Address", SqlDbType.NVarChar, 60).Value = txtAddress.Text.ToString();
                comm.Parameters.Add("@City", SqlDbType.NVarChar, 15).Value = txtCity.Text.ToString();
                comm.Parameters.Add("@Region", SqlDbType.NVarChar, 15).Value = txtRegion.Text.ToString();
                comm.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10).Value = txtPostal.Text.ToString();
                comm.Parameters.Add("@Country", SqlDbType.NVarChar, 15).Value = txtCountry.Text.ToString();
                comm.Parameters.Add("@Phone", SqlDbType.NVarChar, 24).Value = txtPhone.Text.ToString();
                comm.Parameters.Add("@Fax", SqlDbType.NVarChar, 24).Value = txtFax.Text.ToString();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void loadCustomerDetails()
        {
            try
            {
              
                    CustomerID = (string)cboCompany.SelectedValue;
                    //string cus = cboCompany.SelectedValue.ToString();
                    //CustomerID = (cus.ToString());
                    txtID.Text = CustomerID;
                    txtName.Text = cboCompany.Text;



                    SqlDataReader rd;


                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "usp_SSelectCustomerDetails";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Add("@CustomerID", SqlDbType.Char, 5).Value = CustomerID;
                    rd = comm.ExecuteReader();

                    while (rd.Read())
                    {


                        txtContact.Text = (rd["ContactName"].ToString());
                        txtTitle.Text = (rd["ContactTitle"].ToString());
                        txtAddress.Text = (rd["Address"].ToString());
                        txtCity.Text = (rd["City"].ToString());
                        txtRegion.Text = (rd["Region"].ToString());
                        txtPostal.Text = (rd["PostalCode"].ToString());
                        txtCountry.Text = (rd["Country"].ToString());
                        txtPhone.Text = (rd["Phone"].ToString());
                        txtFax.Text = (rd["Fax"].ToString());


                    }
                    if (rd.IsClosed == false)
                        rd.Close();
                
            }
            catch (Exception ex)
            {
               //MessageBox.Show(ex.Message, "Exception Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
                return;
            if (isAdd == true)
                InsertCustomer();
            else
                UpdateCustomer();
            MessageBox.Show("Record successfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadCustomers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAdd = false;
            //if (cboCompany.SelectedIndex > 0)
            {
                loadCustomerDetails();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
