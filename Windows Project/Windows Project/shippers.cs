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
    public partial class shippers : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);
        int ShipperID;
        bool isAdd = false;

        private void LoadcboShipper()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboShipper.DataSource = null;
                cboShipper.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectShippers";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblShi = new DataTable();
                tblShi.Load(rd);
                cboShipper.DataSource = tblShi;
                cboShipper.DisplayMember = "CompanyName";
                cboShipper.ValueMember = "ShipperID";

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
        public shippers()
        {
            InitializeComponent();
        }

        private void shippers_Load(object sender, EventArgs e)
        {
            LoadcboShipper();
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

        private void ShipperDetails()
        {
            try
            {
                ShipperID = int.Parse(cboShipper.SelectedValue.ToString());
                txtID.Text = ShipperID.ToString();
                txtName.Text = cboShipper.Text;

                SqlDataReader rd;
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SSelectShippersById";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@ShipperID", SqlDbType.Int).Value = ShipperID;
                rd = comm.ExecuteReader();
                while (rd.Read())
                {
                    txtPhone.Text = rd["Phone"].ToString();
                }
                if (rd.IsClosed == false)
                {
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                comm.Parameters.Add("@ShipperID", SqlDbType.NChar, 5).Value = ShipperID;
                comm.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 50).Value = txtName.Text.ToString();
                comm.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = txtPhone.Text.ToString();
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
                comm.CommandText = "SInsertShipper";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@ShipperID", SqlDbType.NChar, 5).Value = txtID.Text.ToString();
                comm.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 50).Value = txtName.Text.ToString();
                comm.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = txtPhone.Text.ToString();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboShipper_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAdd = false;
            if (cboShipper.SelectedIndex > 0)
                ShipperDetails();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            txtID.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtID.Select();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
                return;
            if (isAdd == true)
            {
                InsertSupplier();
          }
            else
                InsertSupplier();
                MessageBox.Show("Record successfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);   
            LoadcboShipper();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
