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
using System.Text.RegularExpressions;

namespace Supermarket_Application
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=BUDDY-007\SQLEXPRESS;Initial Catalog=supermarketdb;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void fillcombo()
        {
            // This Methord will bind the combobox with the database
            Con.Open();

            SqlCommand cmd = new SqlCommand("select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "CatName";
            CatCb.DataSource = dt;
            
            Con.Close();
        }
        private void populate()
        {
            Con.Open();

            string query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();

                string query = "insert into ProductTbl values(" + ProdId.Text + ", '" + ProdName.Text + "'," + ProdQty.Text + "," + ProdPrice.Text + ",'" + CatCb.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Product Added Successfully");

                Con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ProductForm main = new ProductForm();
            main.Show();
            this.ResumeLayout(false);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                ProdId.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
                ProdName.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
                ProdQty.Text = ProdDGV.SelectedRows[0].Cells[2].Value.ToString();
                ProdPrice.Text = ProdDGV.SelectedRows[0].Cells[3].Value.ToString();
                CatCb.SelectedValue = ProdDGV.SelectedRows[0].Cells[4].Value.ToString();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "")
                {
                    MessageBox.Show("Salect the Product to Delete");
                }
                else
                {
                    Con.Open();

                    string query = "delete from ProductTbl where Prodid=" + ProdId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Delete Successfully");

                    Con.Close();
                }
                ProductForm main = new ProductForm();
                main.Show();
                this.ResumeLayout(false);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "" || ProdName.Text == "" || ProdQty.Text == "" || ProdPrice.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductTbl set ProdName='" + ProdName.Text + "',ProdPrice='" + ProdPrice.Text + "',ProdQty='" + ProdQty.Text + "',ProdCat='" + CatCb.SelectedValue.ToString() + "' Where Prodid=" + ProdId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ProductForm main = new ProductForm();
            main.Show();
            this.ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SallerForm2 sel = new SallerForm2();
            sel.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void CatCb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void Datelbl_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ProductForm main = new ProductForm();
            main.Show();
            this.ResumeLayout(false);
        }
    }
    
}
