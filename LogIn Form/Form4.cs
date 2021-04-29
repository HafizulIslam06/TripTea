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
    }
}
