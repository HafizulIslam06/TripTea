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
    public partial class Form4 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form4()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Select Name,Product,Cost,Sell from Analysis_Table";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            chart1.DataSource = data;

            chart1.Titles.Add("Analysis Chart");
            chart1.Series["Product"].XValueMember = "Name";
            chart1.Series["Product"].YValueMembers = "Product";
            chart1.Series["Cost"].YValueMembers = "Cost";
            chart1.Series["Sale"].YValueMembers = "Sell";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_Form AdminForm = new Admin_Form();
            AdminForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult check;
            check=MessageBox.Show("Are you sure?","Log Out",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes)
            {
                this.Close();
                LogIn_Form LgIn_frm = new LogIn_Form();
                LgIn_frm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome!\nThe Support Inbox is your place to get updates about things that you've reported, check and reply to messages from the Help Team, and see important messages about your account\n\nFor help visit: www.hchgjvjfuyvg.com", "Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pendding...", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
