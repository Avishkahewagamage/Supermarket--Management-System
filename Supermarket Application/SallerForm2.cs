using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_Application
{
    public partial class SallerForm2 : Form
    {
        public SallerForm2()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=BUDDY-007\SQLEXPRESS;Initial Catalog=supermarketdb;Integrated Security=True");

        private void populate()
        {
            Con.Open();

            string query = "select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];
            
            Con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Sid.Text = SellerDGV.SelectedRows[0].Cells[0].Value.ToString();
            Sname.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            Sage.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            Sphone.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            Spass.Text = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sid.Text == "" || Sname.Text == "" || Sage.Text == "" || Sphone.Text == "" || Spass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update SellerTbl set SellerName='" + Sname.Text + "',SellerAge='" + Sage.Text + "',SellerPhone='" + Sphone.Text + "',SellerPass='" + Spass.Text + "' Where SellerId=" + Sid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Successfully Updated");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SallerForm2 main = new SallerForm2();
            main.Show();
            this.ResumeLayout(false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sid.Text == "")
                {
                    MessageBox.Show("Salect the Saller to Delete");
                }
                else
                {
                    Con.Open();

                    string query = "delete from SellerTbl where SellerId=" + Sid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Delete Successfully");

                    Con.Close();
;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SallerForm2 main = new SallerForm2();
            main.Show();
            this.ResumeLayout(false);
        }

        private void SallerForm2_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();

                string query = "insert into SellerTbl values(" + Sid.Text + ", '" + Sname.Text + "'," + Sage.Text + "," + Sphone.Text + ",'" + Spass.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Seller Added Successfully");

                Con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SallerForm2 main = new SallerForm2();
            main.Show();
            this.ResumeLayout(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
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
            SallerForm2 main = new SallerForm2();
            main.Show();
            this.ResumeLayout(false);
        }
    }
    
}