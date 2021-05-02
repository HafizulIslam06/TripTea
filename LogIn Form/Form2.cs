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
using System.IO;

namespace LogIn_Form
{
    public partial class Admin_Form : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        
        public Admin_Form()
        {
            InitializeComponent();
            BindGridView();
            dataGridView_show();
            dataGridView_showInventory();
        }

        Color select_color = Color.FromArgb(37, 89, 121);
        Color unSelected_color = Color.FromArgb(18, 28, 37);

        Color bar_selected = Color.FromArgb(52, 152, 219);
        Color bar_unselected = Color.FromArgb(18, 28, 37);

        private bool Manager_button1WasClicked = false;
        private bool sell_button1WasClicked = false;
        private bool inventory_button1WasClicked = false;
        private bool Plant_button1WasClicked = false;

        private void UserManagement_Click(object sender, EventArgs e)
        {
            Manager_button1WasClicked = true;
            Button_Click(sender, e);
        }  

        private void SellManagement_Click(object sender, EventArgs e)
        {
            sell_button1WasClicked = true;
            Button_Click(sender, e);
            dataGridView_show();
        }

        private void dataGridView_show()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Sell_Record";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView_SellRecord.DataSource = data;

            dataGridView_SellRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        
        private void InventoryManagement_Click(object sender, EventArgs e)
        {
            inventory_button1WasClicked = true;
            Button_Click(sender, e);
            dataGridView_showInventory();
        }

        private void dataGridView_showInventory()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Inventory_Record";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView_InventoryManagement.DataSource = data;

            dataGridView_InventoryManagement.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void PlantManagement_Click(object sender, EventArgs e)
        {
            Plant_button1WasClicked = true;
            Button_Click(sender, e);
            dataGridView_showTeaRecord();
        }

        private void dataGridView_showTeaRecord()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from TeaPlant_Record";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView_TeaRecord.DataSource = data;

            dataGridView_TeaRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (Manager_button1WasClicked == true)
            {
                Panel_UserManagement.BringToFront();

                button_UserManagement.BackColor = select_color;

                button_SellManagement.BackColor = unSelected_color;
                button_TeaPlant.BackColor = unSelected_color;
                button_Inventory.BackColor = unSelected_color;

                UserManagement_Bar.BackColor = bar_selected;

                TeaPlant_Bar.BackColor = bar_unselected;
                InventoryManagement_Bar.BackColor = bar_unselected;
                SellManagement_Bar.BackColor = bar_unselected;

                Manager_button1WasClicked = false;
            }
            else if (sell_button1WasClicked == true)
            {
                ResetForm();
                panel_sellManagement.BringToFront();

                button_SellManagement.BackColor = select_color;

                button_UserManagement.BackColor = unSelected_color;
                button_TeaPlant.BackColor = unSelected_color;
                button_Inventory.BackColor = unSelected_color;

                SellManagement_Bar.BackColor = bar_selected;

                UserManagement_Bar.BackColor = bar_unselected;
                TeaPlant_Bar.BackColor = bar_unselected;
                InventoryManagement_Bar.BackColor = bar_unselected;

                sell_button1WasClicked = false;                
            }
            else if (inventory_button1WasClicked == true)
            {
                panel_InventoryManagement.BringToFront();

                button_Inventory.BackColor = select_color;

                button_UserManagement.BackColor = unSelected_color;
                button_SellManagement.BackColor = unSelected_color;
                button_TeaPlant.BackColor = unSelected_color;

                InventoryManagement_Bar.BackColor = bar_selected;

                UserManagement_Bar.BackColor = bar_unselected;
                TeaPlant_Bar.BackColor = bar_unselected;
                SellManagement_Bar.BackColor = bar_unselected;

                inventory_button1WasClicked = false;
            }
            else if (Plant_button1WasClicked == true)
            {
                panel_TeaPlantManagement.BringToFront();

                button_TeaPlant.BackColor = select_color;

                button_SellManagement.BackColor = unSelected_color;
                button_UserManagement.BackColor = unSelected_color;
                button_Inventory.BackColor = unSelected_color;

                TeaPlant_Bar.BackColor = bar_selected;

                UserManagement_Bar.BackColor = bar_unselected;
                InventoryManagement_Bar.BackColor = bar_unselected;
                SellManagement_Bar.BackColor = bar_unselected;

                Plant_button1WasClicked = false;
            }
            else
            {
                MessageBox.Show("else"); 
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //ofd.Filter = "PNG File(*.png)|*.png";
            //ofd.Filter = "JPG File(*.jpgg)|*.jpg";
            //ofd.Filter = "Image File(*.png;*.jpg)|*.png;*.png";
            ofd.Filter = "Image File (All files) *.* | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox_Manager.Image = new Bitmap(ofd.FileName);
            }
            ofd.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Admin_Table values (@ID,@FirstName,@LastName,@Address,@Email,@PhoneNo,@Photo,@UserName,@Password)";
            SqlCommand cmd = new SqlCommand(query, con);            

            cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(textBox_ID.Text));
            cmd.Parameters.AddWithValue("@FirstName", textBox_FName.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox_LName.Text);
            cmd.Parameters.AddWithValue("@Address", textBox_Address.Text);
            cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox_PhoneNo.Text);            
            cmd.Parameters.AddWithValue("@UserName", textBox_UserName.Text);
            cmd.Parameters.AddWithValue("@Password", textBox_Password.Text);
            cmd.Parameters.AddWithValue("@Photo", SavePhoto());            

            con.Open();            
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted Successfully","Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ManagerInfoAdded();
                ResetForm();                
            }
            else 
            {
                MessageBox.Show("Data Inserted Failed", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ManagerInfoAdded()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Manager_Info values (@Username ,@Password)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Username", textBox_UserName.Text);
            cmd.Parameters.AddWithValue("@Password", textBox_Password.Text);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        void ResetForm()
        {
            textBox_FName.Clear();
            textBox_LName.Clear();
            textBox_Address.Clear();
            textBox_Email.Clear();
            textBox_PhoneNo.Clear();
            textBox_UserName.Clear();
            textBox_Password.Clear();
            textBox_ID.Clear();
            pictureBox_Manager.Image = Properties.Resources.download;
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox_Manager.Image.Save(ms,pictureBox_Manager.Image.RawFormat);
            return ms.GetBuffer();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Admin_Table l";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView_ManagerRecord.DataSource = data;

            //Image column
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView_ManagerRecord.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //autosize
            dataGridView_ManagerRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //Image Hight
            dataGridView_ManagerRecord.RowTemplate.Height = 50;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox_ID.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[0].Value.ToString();
            textBox_FName.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[1].Value.ToString();
            textBox_LName.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[2].Value.ToString();
            textBox_Address.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[3].Value.ToString();
            textBox_Email.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[4].Value.ToString();
            textBox_PhoneNo.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[5].Value.ToString();
            pictureBox_Manager.Image = GetPhoto((byte[])dataGridView_ManagerRecord.SelectedRows[0].Cells[6].Value);
            textBox_UserName.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[7].Value.ToString();
            textBox_Password.Text = dataGridView_ManagerRecord.SelectedRows[0].Cells[8].Value.ToString();    
        }

        private Image GetPhoto(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            return Image.FromStream(ms);
        }        

        private void button16_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Admin_Table set ID=@ID, [First Name]=@FirstName, [Last Name]=@LastName, Address=@Address, Email=@Email, [Phone No.]=@PhoneNo, Photo=@Photo, [User Name]=@UserName, Password=@Password where ID=@ID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(textBox_ID.Text));
            cmd.Parameters.AddWithValue("@FirstName", textBox_FName.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox_LName.Text);
            cmd.Parameters.AddWithValue("@Address", textBox_Address.Text);
            cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox_PhoneNo.Text);
            cmd.Parameters.AddWithValue("@UserName", textBox_UserName.Text);
            cmd.Parameters.AddWithValue("@Password", textBox_Password.Text);
            cmd.Parameters.AddWithValue("@Photo", SavePhoto());

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data updated Successfully","Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Data updated Failed","Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Delete_button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Admin_Table where ID=@ID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", textBox_ID.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a >= 0)
            {
                MessageBox.Show("Data deleted successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                managerInfoDelete();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Data not deleted", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void managerInfoDelete()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Manager_Info where Username=@Username";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Username", textBox_UserName.Text);
            cmd.Parameters.AddWithValue("@Password", textBox_Password.Text);

            con.Open();
            cmd.ExecuteNonQuery();           
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
