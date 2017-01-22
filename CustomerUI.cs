using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicCustomer;
using Dal;
using System.Configuration;

namespace CustomerProject_Lab4_
{
    public partial class CustomerUI : Form
    {
        public CustomerUI()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            CustomerDal dal = new CustomerDal();
            DataSet customers = dal.Read();
            dtgCustomers.DataSource = customers.Tables[0];

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Customer custobj = new Customer();
             
                custobj.CustomerName = txtCustomerName.Text;
                Console.WriteLine(custobj.CustomerName);
                custobj.PhoneNumber = txtPhoneNumber.Text;
                Console.WriteLine(custobj.PhoneNumber);
                custobj.ProductName = txtProduct.Text;
                Console.WriteLine(custobj.ProductName);
                custobj.BillAmount = Convert.ToDecimal(txtBillAmount.Text);
                Console.WriteLine(custobj.BillAmount);
             
                
                
                if (custobj.Validate())//thrown
                    {
                    CustomerDal dal1 = new CustomerDal();
                    dal1.Add(custobj);
                    }
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message)  ;

            }

        }

        private void lblBillAmount_Click(object sender, EventArgs e)
        {
            

        }

        private void CustomerUI_Load(object sender, EventArgs e)
        {
            //configuration xml is applied to the screen
            this.Text = ConfigurationManager.AppSettings["NameoftheApplication"].ToString();
            btnAdd.Text = ConfigurationManager.AppSettings["AddButton"].ToString();
            btnDelete.Text = ConfigurationManager.AppSettings["UpdateButton"].ToString();
            btnDelete.Text = ConfigurationManager.AppSettings["DeleteButton"].ToString();
            LoadGrid();
        }



        private void dtgCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            int rowselected = e.RowIndex;//current row
            txtId.Text = dtgCustomers.Rows[rowselected].Cells[0].Value.ToString();
            txtCustomerName.Text = dtgCustomers.Rows[rowselected].Cells[1].Value.ToString();
            txtPhoneNumber.Text = dtgCustomers.Rows[rowselected].Cells[2].Value.ToString();
            txtProduct.Text = dtgCustomers.Rows[rowselected].Cells[3].Value.ToString();
           txtBillAmount.Text = dtgCustomers.Rows[rowselected].Cells[4].Value.ToString();

        }

        private void dtgCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Loading  values from the ui
            Customer updatedCustomer = new Customer();

            updatedCustomer.CustomerName = txtCustomerName.Text;
            updatedCustomer.PhoneNumber = txtPhoneNumber.Text;
            updatedCustomer.ProductName = txtProduct.Text;
            updatedCustomer.BillAmount = Convert.ToDecimal(txtBillAmount.Text);

            //updating from the server
            CustomerDal dal = new CustomerDal();
            dal.Update(updatedCustomer, Convert.ToInt16(txtId.Text));
           
            //refreshing the grid
            LoadGrid();
            ClearUI();

        }
        private void ClearUI()
        {

            txtCustomerName.Text="";
            txtPhoneNumber.Text="";
            txtProduct.Text="";
            txtBillAmount.Text="";

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Loading values from the ui
            Customer DeletedCustomer = new Customer();

            DeletedCustomer.CustomerName = txtCustomerName.Text;
            DeletedCustomer.PhoneNumber = txtPhoneNumber.Text;
            DeletedCustomer.ProductName = txtProduct.Text;
            DeletedCustomer.BillAmount = Convert.ToDecimal(txtBillAmount.Text);

            //updating from the server
            CustomerDal dal = new CustomerDal();
            dal.Delete(DeletedCustomer, Convert.ToInt16(txtId.Text));

            //refreshing the grid
            LoadGrid();
            ClearUI();

        }
    }
}
