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

namespace Supermarket_Application
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=BUDDY-007\SQLEXPRESS;Initial Catalog=supermarketdb;Integrated Security=True");
        private void CatAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();

                string query = "insert into CategoryTbl values("+CatidTxt.Text+", '"+CatNameTxt.Text+"','"+CatDescTxt.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Category Added Successfully");
                Con.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CategoryForm main = new CategoryForm();
            main.Show();
            this.ResumeLayout(false);
        }
        private void populate()
        {
            Con.Open();

            string query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatidTxt.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTxt.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTxt.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatidTxt.Text == "")
                {
                    MessageBox.Show("Salect the Category to Delete");
                }
                else
                {
                    Con.Open();

                    string query = "delete from CategoryTbl where Catid=" + CatidTxt.Text + "";
                    SqlCommand cmd = new SqlCommand(query,Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Delete Successfully");

                    Con.Close();

                    
                }
                CategoryForm main = new CategoryForm();
                main.Show();
                this.ResumeLayout(false);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatidTxt.Text == "" || CatNameTxt.Text == "" || CatDescTxt.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "Update Categorytbl set CatName='" + CatNameTxt.Text + "',CatDesc='" + CatDescTxt.Text + "' Where Catid=" + CatidTxt.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Successfully Updated");
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CategoryForm main = new CategoryForm();
            main.Show();
            this.ResumeLayout(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           SallerForm2 sell = new SallerForm2();
            sell.Show();
            this.Hide();
        }

        private void CatidTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
           /* CategoryForm main = new CategoryForm();
            main.Show();
            this.ResumeLayout(false);*/
        }
    }
    
}
