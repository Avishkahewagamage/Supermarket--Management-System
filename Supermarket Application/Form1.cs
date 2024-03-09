﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string Sellername = "";
        SqlConnection Con = new SqlConnection(@"Data Source=BUDDY-007\SQLEXPRESS;Initial Catalog=supermarketdb;Integrated Security=True");

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ClearLbl_Click(object sender, EventArgs e)
        {
            UsernameTxt.Text = "";
            PasswordTxt.Text = "";
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UsernameTxt.Text == "" || PasswordTxt.Text == null)
            {
                MessageBox.Show("Emter the Username and Password");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {
                    if (RoleCb.SelectedItem.ToString() == "ADMIN")
                    {
                        if (UsernameTxt.Text == "Admin" && PasswordTxt.Text == "Admin")
                        {
                            CategoryForm cat = new CategoryForm();
                            cat.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If You are the admin, Enter the correct Id and Password");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Select a Role");
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(8) from SellerTbl where SellerName='" + UsernameTxt.Text + "' and SellerPass='" + PasswordTxt.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Sellername = UsernameTxt.Text;
                            SellingForm selling = new SellingForm();
                            selling.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select A Role");
                }
            }
        }
    }
}
