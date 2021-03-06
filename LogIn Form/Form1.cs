using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace LogIn_Form
{
    public partial class LogIn_Form : Form
    {
        public static string saveUsername;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public LogIn_Form()
        {
            InitializeComponent();
            
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
         {
             bool status = checkBox_A.Checked;
             switch (status)
             {
                 case true:
                     textBox_A_password.UseSystemPasswordChar = false;
                     break;
                 default:
                     textBox_A_password.UseSystemPasswordChar = true;
                     break;
             }
         }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_A_UserName.Text != "" && textBox_A_password.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from Admin_Info where UserNmae=@user and Pass =@pass";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@user", textBox_A_UserName.Text);
                cmd.Parameters.AddWithValue("@pass", textBox_A_password.Text);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    this.Hide();
                    Form4 dsh_frm = new Form4();
                    dsh_frm.ShowDialog();                    
                }
                else
                {
                    MessageBox.Show("Wrong Password");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Enter LogIn info");
            }            
        }

        Color select_color = Color.FromArgb(37, 89, 121);
        Color unSelected_color = Color.FromArgb(18, 28, 37);
        Color bar_selected = Color.FromArgb(52, 152, 219);
        Color bar_unselected = Color.FromArgb(33, 43, 52);
        
        
        private void Admin_Click(object sender, EventArgs e)
        {
            textBox_M_Username.Text = "Username";
            textBox_M_Password.Text = "Password";

            panel_AdminLogIn.BringToFront();
            Admin_Button.BackColor = select_color;            
            Manager_button.BackColor = unSelected_color;

            panel1.BackColor = bar_selected;
            panel2.BackColor = bar_unselected;
        }
        

        private void Manager_Click(object sender, EventArgs e)
        {
            textBox_A_UserName.Text = "Username";
            textBox_A_password.Text = "Password";

            panel_Manager_LogIn.BringToFront();
            Manager_button.BackColor = select_color;            
            Admin_Button.BackColor = unSelected_color;

            panel2.BackColor = bar_selected;
            panel1.BackColor = bar_unselected;
        }
            

        private void checkBox_M_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox_M.Checked;
            switch (status)
            {
                case true:
                    textBox_M_Password.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox_M_Password.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void button_M_LogIn_Click(object sender, EventArgs e)
        {
            if (textBox_M_Username.Text != "" && textBox_M_Password.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from Manager_Info where Username=@User and Password =@Pass";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@User", textBox_M_Username.Text);                
                cmd.Parameters.AddWithValue("@Pass", textBox_M_Password.Text);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    saveUsername = textBox_M_Username.Text;
                    this.Hide();
                    Form_Manager ManagerForm = new Form_Manager();
                    ManagerForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Wrong Password");
                }
                con.Close();
            }
        }

        private void textBox_A_UserName_Click(object sender, EventArgs e)
        {
            textBox_A_UserName.Clear();
        }

        private void textBox_A_password_Click(object sender, EventArgs e)
        {
            textBox_A_password.Clear();
            textBox_A_password.UseSystemPasswordChar = true;
        }

        private void textBox_M_Username_Click(object sender, EventArgs e)
        {
            textBox_M_Username.Clear();
        }

        private void textBox_M_Password_Click(object sender, EventArgs e)
        {
            textBox_M_Password.Clear();
            textBox_M_Password.UseSystemPasswordChar = true;
        }
    }
}
