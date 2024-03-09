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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Supermarket_Application
{
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=BUDDY-007\SQLEXPRESS;Initial Catalog=supermarketdb;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
        private void populate()
        {
            Con.Open();

            string query = "select ProdName,ProdPrice from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void populatebills()
        {
            Con.Open();

            string query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillsDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            SellerNamelbl.Text = Form1.Sellername;
        }
        
        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            ProdPrice.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void Datelbl_Click(object sender, EventArgs e)
        {
            
        }
        int Grdtotal = 0;
        int n = 0;

        public object ProdQty { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (ProdName.Text == "" || SelProdQty.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(SelProdQty.Text);

                DataGridViewRow newRow = new DataGridViewRow();

                newRow.CreateCells(ORDERDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdName.Text;
                newRow.Cells[2].Value = ProdPrice.Text;
                newRow.Cells[3].Value = SelProdQty.Text;
                newRow.Cells[4].Value = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(SelProdQty.Text);
                ORDERDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + total;
                Amtlbl.Text = " " + Grdtotal;
            }
        }

        private void ProdAddbtn_Click(object sender, EventArgs e)
        {
            if (BillID.Text == "")
            {
                MessageBox.Show("Missing Bill Id");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query = "insert into BillTbl values(" + BillID.Text + ", '" + SellerNamelbl.Text + "','" + Datelbl.Text + "'," + Amtlbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Order Added Successfully");

                    Con.Close();

                    populatebills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BillID.Text = BillsDGV.SelectedRows[0].Cells[0].Value.ToString();
            int flag = 1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("FAMILY SUPERMARKET", new Font("Century Gothic", 25, FontStyle.Bold),Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill ID:               "+BillsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(100,70));
            e.Graphics.DrawString("Seller Name:    " + BillsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(100, 110));
            e.Graphics.DrawString("Date: " + BillsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, new Point(100, 150));
            e.Graphics.DrawString("................................................................................", new Font("Century Gothic", 25, FontStyle.Regular), Brushes.Black, new Point(35, 165));
            e.Graphics.DrawString("Product Name:       " + ProdName.Text, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(100, 240));
            e.Graphics.DrawString("Product Price:         " + ProdPrice.Text, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(100, 280));
            e.Graphics.DrawString("Quantity:                   " + SelProdQty.Text, new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(100, 320));
            e.Graphics.DrawString("Total Amount:      " + BillsDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 380));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
            SellingForm main = new SellingForm();
            main.Show();
            this.ResumeLayout(false);
        }

        private void CatCb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SellingDel_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (BillID.Text == "")
                {
                    MessageBox.Show("Salect the Billid to Delete");
                }
                else
                {
                    Con.Open();

                    string query = "delete from BillTbl where Billid=" + BillID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Delete Successfully");

                    Con.Close();
                     populate();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        private void ORDERDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
