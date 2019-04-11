using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome");
        }

        private void productsAndRelatedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            user.WindowState = FormWindowState.Normal;
            user.StartPosition = FormStartPosition.CenterScreen;
            user.MdiParent = this;
            user.Show();
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSuppliers supply = new frmSuppliers();
            supply.WindowState = FormWindowState.Normal;
            supply.StartPosition = FormStartPosition.CenterScreen;
            supply.MdiParent = this;
            supply.Show();
        }

        private void shippersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shippers ship = new shippers();
            ship.WindowState = FormWindowState.Normal;
            ship.StartPosition = FormStartPosition.CenterScreen;
            ship.MdiParent = this;
            ship.Show();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories category = new Categories();
            category.WindowState = FormWindowState.Normal;
            category.StartPosition = FormStartPosition.CenterScreen;
            category.MdiParent = this;
            category.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            product.WindowState = FormWindowState.Normal;
            product.StartPosition = FormStartPosition.CenterScreen;
            product.MdiParent = this;
            product.Show();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customers customer = new Customers();
            customer.WindowState = FormWindowState.Normal;
            customer.StartPosition = FormStartPosition.CenterScreen;
            customer.MdiParent = this;
            customer.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            user.WindowState = FormWindowState.Normal;
            user.StartPosition = FormStartPosition.CenterScreen;
            user.MdiParent = this;
            user.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmSuppliers supply = new frmSuppliers();
            supply.WindowState = FormWindowState.Normal;
            supply.StartPosition = FormStartPosition.CenterScreen;
            supply.MdiParent = this;
            supply.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            shippers ship = new shippers();
            ship.WindowState = FormWindowState.Normal;
            ship.StartPosition = FormStartPosition.CenterScreen;
            ship.MdiParent = this;
            ship.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Categories category = new Categories();
            category.WindowState = FormWindowState.Normal;
            category.StartPosition = FormStartPosition.CenterScreen;
            category.MdiParent = this;
            category.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            product.WindowState = FormWindowState.Normal;
            product.StartPosition = FormStartPosition.CenterScreen;
            product.MdiParent = this;
            product.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Customers customer = new Customers();
            customer.WindowState = FormWindowState.Normal;
            customer.StartPosition = FormStartPosition.CenterScreen;
            customer.MdiParent = this;
            customer.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
