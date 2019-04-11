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
    public partial class Products : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);

        int categoryid;
        int productid;
        int suppllierid;
        bool Discontinued = false;
        bool isAdd = false ;
        public Products()
        {
            InitializeComponent();
        }

        private void loadcboCategories()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboCategory.DataSource = null;
                cboCategory.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectCategories";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblCat = new DataTable();
                tblCat.Load(rd);
                cboCategory.DataSource = tblCat;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";

                if (rd.IsClosed == false)
                {
                    rd.Close();
                }
                
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadcboProducts()
        {
            try
            {
                SqlDataReader rd;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cboProducts.DataSource = null;
                cboProducts.Items.Clear();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "usp_SSelectProducts";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                rd = comm.ExecuteReader();

                DataTable tblprod = new DataTable();
                tblprod.Load(rd);
                cboProducts.DataSource = tblprod;
                cboProducts.DisplayMember = "ProductName";
                cboProducts.ValueMember = "ProductID";

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
                comm.CommandText = "usp_SSelectSuppliersFK";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            loadcboProducts();
            loadcboCategories();
            loadcboSupplier();
        }

        private void cboProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAdd = false;

           // if (cboProducts.SelectedIndex > 0)
                loadProductDetails();   
        }
        private void loadProductDetails()
        {
            try
            {
               
                productid = int.Parse(cboProducts.SelectedValue.ToString());
             
                txtID.Text = productid.ToString();
                txtName.Text = cboProducts.Text;
                
                SqlDataReader rd;


                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SSelectProductById";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@ProductId", SqlDbType.Int).Value = productid;
                rd = comm.ExecuteReader();

                while (rd.Read())
                {

                    cboSupplier.Text = rd["CompanyName"].ToString();
                    cboCategory.Text = rd["CategoryName"].ToString();
                    txtQty.Text = rd["QuantityPerUnit"].ToString();
                    nudPrice.Value = Decimal.Parse(rd["UnitPrice"].ToString());
                    nudStock.Value = int.Parse(rd["UnitsInStock"].ToString());
                    nudOrder.Value = int.Parse(rd["UnitsOnOrder"].ToString());
                    nudLevel.Value = int.Parse(rd["ReorderLevel"].ToString());
                    Discontinued = bool.Parse(rd["Discontinued"].ToString());
                }

                if (rd.IsClosed == false)
                    rd.Close();
                if (Discontinued == true)
                {
                    rbYes.Checked = true;
                    rbNo.Checked = false;
                }
                else
                {
                    rbYes.Checked = false;
                    rbNo.Checked = true;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Exception Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSupplier.SelectedIndex > 0)
                suppllierid = int.Parse(cboSupplier.SelectedValue.ToString());
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex > 0)
                categoryid = int.Parse(cboCategory.SelectedValue.ToString());
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            Discontinued = true;
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            Discontinued = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            txtID.Clear();
            txtName.Clear();
            txtQty.Clear();
            nudLevel.Value = 0;
            nudOrder.Value = 0;
            nudPrice.Value = 0;
            nudStock.Value = 0;             
        }
        private bool ValidateData()
        {
            try
            {
               
                if (txtName.Text == "")
                {
                    err.SetError(txtName, "Please enter a Product Name");
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

        private void InsertProduct()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SInsertProduct";
                comm.CommandType = CommandType.StoredProcedure;
             
                comm.Parameters.Add("@ProductName", SqlDbType.NVarChar, 40).Value = txtName.Text.ToString();
                comm.Parameters.Add("@SupplierID", SqlDbType.Int).Value = cboSupplier.SelectedIndex;
               // comm.Parameters.Add("@CategpryID", SqlDbType.Int).Value = cboCategory.SelectedIndex;
                 comm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = cboCategory.SelectedIndex;
                comm.Parameters.Add("@QuantityPerUnit", SqlDbType.NVarChar,20).Value = txtQty.Text.ToString();
                comm.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = nudPrice.Value;
                comm.Parameters.Add("@UnitsInStock", SqlDbType.SmallInt).Value = nudStock.Value;
                comm.Parameters.Add("@UnitsOnOrder", SqlDbType.SmallInt).Value = nudOrder.Value;
                comm.Parameters.Add("@ReOrderLevel", SqlDbType.SmallInt).Value = nudLevel.Value;
                comm.Parameters.Add("@Discontinued", SqlDbType.Bit).Value = false;
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record not saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void UpdateProduct()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "usp_SUpdateProduct";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@ProductId", SqlDbType.Int).Value = productid;
                comm.Parameters.Add("@ProductName", SqlDbType.NVarChar, 40).Value = txtName.Text.Trim();
                comm.Parameters.Add("@SupplierID", SqlDbType.Int).Value = cboSupplier.SelectedIndex;
                comm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = cboCategory.SelectedIndex;
                comm.Parameters.Add("@QuantityPerUnit", SqlDbType.NVarChar).Value = txtQty.Text.Trim();
                comm.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = nudPrice.Value.ToString();
                comm.Parameters.Add("@UnitsInStock", SqlDbType.SmallInt).Value = nudStock.Value.ToString();
                comm.Parameters.Add("@UnitsOnOrder", SqlDbType.SmallInt).Value = nudOrder.Value.ToString();
                comm.Parameters.Add("@ReOrderLevel", SqlDbType.SmallInt).Value = nudLevel.Value.ToString();
                comm.Parameters.Add("@Discontinued", SqlDbType.Bit).Value = false;
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
                InsertProduct();
            else
                UpdateProduct();
            MessageBox.Show("Record successfully saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadcboProducts();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
