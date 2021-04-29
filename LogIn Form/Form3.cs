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
    public partial class Form_Manager : Form
    {
        String username = LogIn_Form.saveUsername;
        public static int Product;
        public static int Cost;
        public static int Sell;

        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form_Manager()
        {
            InitializeComponent();
            Manager_BindGridView();
            DataGridView_SellRecord();
            dataGridView2();
            dataGridView_TeaRecord();
        }
                
        Color select_color = Color.FromArgb(46, 49, 49);
        Color unSelected_color = Color.FromArgb(0, 0, 0);

        Color bar_selected = Color.FromArgb(52, 152, 219);
        Color bar_unselected = Color.FromArgb(49, 46, 46);

        private bool Manager_button1WasClicked = false;
        private bool sell_button1WasClicked = false;
        private bool inventory_button1WasClicked = false;
        private bool Plant_button1WasClicked = false;

        //====================================Menu Button Click=========================================
        private void button_SellManagement_Click(object sender, EventArgs e)
        {
            sell_button1WasClicked = true;
            Button_Click(sender, e);
        }

        private void button_StaffManagement_Click(object sender, EventArgs e)
        {
            Manager_button1WasClicked = true;
            Button_Click(sender, e);
        }

        private void button_Inventory_Click(object sender, EventArgs e)
        {
            inventory_button1WasClicked = true;
            Button_Click(sender, e);
        }

        private void button_TeaPlant_Click(object sender, EventArgs e)
        {
            Plant_button1WasClicked = true;
            Button_Click(sender, e);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (Manager_button1WasClicked == true)
            {
                Panel_UserManagement.BringToFront();

                button_StaffManagement.BackColor = select_color;

                button_SellManagement.BackColor = unSelected_color;
                button_TeaPlant.BackColor = unSelected_color;
                button_Inventory.BackColor = unSelected_color;

                StaffManagement_Bar.BackColor = bar_selected;

                TeaPlant_Bar.BackColor = bar_unselected;
                InventoryManagement_Bar.BackColor = bar_unselected;
                SellManagement_Bar.BackColor = bar_unselected;

                Manager_button1WasClicked = false;
            }
            else if (sell_button1WasClicked == true)
            {
                StaffPanel_ResetForm();
                panel_SellManagement.BringToFront();

                button_SellManagement.BackColor = select_color;

                button_StaffManagement.BackColor = unSelected_color;
                button_TeaPlant.BackColor = unSelected_color;
                button_Inventory.BackColor = unSelected_color;

                SellManagement_Bar.BackColor = bar_selected;

                StaffManagement_Bar.BackColor = bar_unselected;
                TeaPlant_Bar.BackColor = bar_unselected;
                InventoryManagement_Bar.BackColor = bar_unselected;

                sell_button1WasClicked = false;
            }
            else if (inventory_button1WasClicked == true)
            {
                panel_InventoryManagement.BringToFront();

                button_Inventory.BackColor = select_color;

                button_StaffManagement.BackColor = unSelected_color;
                button_SellManagement.BackColor = unSelected_color;
                button_TeaPlant.BackColor = unSelected_color;

                InventoryManagement_Bar.BackColor = bar_selected;

                StaffManagement_Bar.BackColor = bar_unselected;
                TeaPlant_Bar.BackColor = bar_unselected;
                SellManagement_Bar.BackColor = bar_unselected;

                inventory_button1WasClicked = false;
            }
            else if (Plant_button1WasClicked == true)
            {
                panel_TeaPlantManagement.BringToFront();

                button_TeaPlant.BackColor = select_color;

                button_SellManagement.BackColor = unSelected_color;
                button_StaffManagement.BackColor = unSelected_color;
                button_Inventory.BackColor = unSelected_color;

                TeaPlant_Bar.BackColor = bar_selected;

                StaffManagement_Bar.BackColor = bar_unselected;
                InventoryManagement_Bar.BackColor = bar_unselected;
                SellManagement_Bar.BackColor = bar_unselected;

                Plant_button1WasClicked = false;
            }
            else
            {
                MessageBox.Show("else");
            }
        }


        //=====================================Staff Management's Button================================
        private void button_CreateManager_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Staff_Record values (@ID,@FirstName,@LastName,@Address,@Email,@PhoneNo,@Photo,@UserName,@Salary,@M_username)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(textBox_ID.Text));
            cmd.Parameters.AddWithValue("@FirstName", textBox_FName.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox_LName.Text);
            cmd.Parameters.AddWithValue("@Address", textBox_Address.Text);
            cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox_PhoneNo.Text);
            cmd.Parameters.AddWithValue("@UserName", textBox_UserName.Text);
            cmd.Parameters.AddWithValue("@Salary", textBox_Password.Text);
            cmd.Parameters.AddWithValue("@Photo", SavePhoto());
            cmd.Parameters.AddWithValue("@M_username", LogIn_Form.saveUsername);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted Successfully");
                Manager_BindGridView();
                StaffPanel_ResetForm();
            }
            else
            {
                MessageBox.Show("Data Inserted Failed");
            }
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
        private void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "Image File (All files) *.* | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox_Manager.Image = new Bitmap(ofd.FileName);
            }
            ofd.ShowDialog();
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox_Manager.Image.Save(ms, pictureBox_Manager.Image.RawFormat);
            return ms.GetBuffer();
        }
        void Manager_BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            //string query = "select * from Staff_Record";
            string query = "select * from Staff_Record where M_username='" + username + "'";
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
        private void button_Update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Staff_Record set ID=@ID, [First Name]=@FirstName, [Last Name]=@LastName, Address=@Address, Email=@Email, [PhoneNo.]=@PhoneNo, Photo=@Photo, [UserName]=@UserName, Salary=@Salary where ID=@ID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(textBox_ID.Text));
            cmd.Parameters.AddWithValue("@FirstName", textBox_FName.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox_LName.Text);
            cmd.Parameters.AddWithValue("@Address", textBox_Address.Text);
            cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox_PhoneNo.Text);
            cmd.Parameters.AddWithValue("@UserName", textBox_UserName.Text);
            cmd.Parameters.AddWithValue("@Salary", textBox_Password.Text);
            cmd.Parameters.AddWithValue("@Photo", SavePhoto());

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data updated Successfully");
                Manager_BindGridView();
                StaffPanel_ResetForm();
            }
            else
            {
                MessageBox.Show("Data updated Failed");
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Staff_Record where ID=@ID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", textBox_ID.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a >= 0)
            {
                MessageBox.Show("Data deleted successfully");
                Manager_BindGridView();
                StaffPanel_ResetForm();
            }
            else
            {
                MessageBox.Show("Data not deleted");
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            StaffPanel_ResetForm();
        }
        void StaffPanel_ResetForm()
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




        //=====================================Sell Record====================================
        private void button_SellCreate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Sell_Record values (@Serial,@Buyer,@Address,@Email,@PhoneNo,@Note,@userName)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", Convert.ToInt16(textBox_S_Serial.Text));            
            cmd.Parameters.AddWithValue("@Buyer", textBox_S_Buyer.Text);
            cmd.Parameters.AddWithValue("@Address",textBox_S_Address.Text);
            cmd.Parameters.AddWithValue("@Email", textBox_S_Email.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox_S_Phone.Text);
            cmd.Parameters.AddWithValue("@Note", textBox_S_Note.Text);
            cmd.Parameters.AddWithValue("@userName", LogIn_Form.saveUsername);

            Sell = Convert.ToInt16(textBox_S_Address.Text);
            //Admin_Form admnfrm = new Admin_Form(textBox_S_Serial.ToString(), textBox_S_Buyer.Text, textBox_S_Address.Text, textBox_S_Email.Text, textBox_S_Phone.Text, textBox_S_Note.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted Successfully");
                DataGridView_SellRecord();
                SellManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data Inserted Failed");
            }
        }
        
        private void DataGridView_SellRecord()
        {
            
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Sell_Record where [User Name]='" + username + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void button_SellReset_Click(object sender, EventArgs e)
        {
            SellManagement_ResetText();
        }
        private void SellManagement_ResetText()
        {
            textBox_S_Serial.Clear();
            textBox_S_Buyer.Clear();
            textBox_S_Address.Clear();
            textBox_S_Email.Clear();
            textBox_S_Phone.Clear();
            textBox_S_Note.Clear();
        }
        private void dataGridView1_CellContentClick(object sender, MouseEventArgs e)
        {
            textBox_S_Serial.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox_S_Buyer.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox_S_Address.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox_S_Email.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox_S_Phone.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox_S_Note.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button_SellUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Sell_Record set Serial=@Serial,Buyer=@Buyer,Address=@Address, Email=@Email, [PhoneNo.]=@PhoneNo,Note=@Note where Serial=@Serial";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", Convert.ToInt16(textBox_S_Serial.Text));
            cmd.Parameters.AddWithValue("@Buyer", textBox_S_Buyer.Text);
            cmd.Parameters.AddWithValue("@Address", textBox_S_Address.Text);
            cmd.Parameters.AddWithValue("@Email", textBox_S_Email.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox_S_Phone.Text);
            cmd.Parameters.AddWithValue("@Note", textBox_S_Note.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data updated Successfully");
                DataGridView_SellRecord();
                SellManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data updated Failed");
            }
        }

        private void button_SellDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Sell_Record where Serial=@Serial";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", textBox_S_Serial.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a >= 0)
            {
                MessageBox.Show("Data deleted successfully");
                DataGridView_SellRecord();
                SellManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data not deleted");
            }
        }



        //===============================Inventory Record=================================
        private void button_CreateInventory_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Inventory_Record values (@Serial,@Date,@StaffSalary,@MaintananceCost,@TotalSell,@Note,@userName)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", Convert.ToInt16(textBox2.Text));
            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@StaffSalary", textBox6.Text);
            cmd.Parameters.AddWithValue("@MaintananceCost", textBox4.Text);
            cmd.Parameters.AddWithValue("@TotalSell", textBox5.Text);
            cmd.Parameters.AddWithValue("@Note", textBox1.Text);
            cmd.Parameters.AddWithValue("@userName", LogIn_Form.saveUsername);

            Cost = Convert.ToInt16(textBox6.Text) + Convert.ToInt16(textBox4.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted Successfully");
                dataGridView2();
                InventoryManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data Inserted Failed");
            }
        }

        private void InventoryManagement_ResetText()
        {
            textBox2.Clear();            
            textBox6.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Clear();
        }

        private void dataGridView2()
        {
            SqlConnection con = new SqlConnection(cs);
            
            string query = "select * from Inventory_Record where [User Name]='" + username + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView_Inventory.DataSource = data;
                        
            //autosize
            dataGridView_Inventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;            
        }

        private void button_UpdateInventory_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Inventory_Record set Serial=@Serial, Date=@Date, [Staff Salary]=@StaffSalary, [Maintanance Cost]=@MaintananceCost, TotalSell=@Totalsell, Note=@Note where Serial=@Serial";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", Convert.ToInt16(textBox2.Text));
            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@StaffSalary", textBox6.Text);
            cmd.Parameters.AddWithValue("@MaintananceCost", textBox4.Text);
            cmd.Parameters.AddWithValue("@TotalSell", textBox5.Text);
            cmd.Parameters.AddWithValue("@Note", textBox1.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data updated Successfully");
                dataGridView2();
                InventoryManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data updated Failed");
            }
        }

        private void dataGridView_Inventory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView_Inventory.SelectedRows[0].Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView_Inventory.SelectedRows[0].Cells[1].Value.ToString();
            textBox6.Text = dataGridView_Inventory.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView_Inventory.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView_Inventory.SelectedRows[0].Cells[4].Value.ToString();
            textBox1.Text = dataGridView_Inventory.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button_ResetInventory_Click(object sender, EventArgs e)
        {
            InventoryManagement_ResetText();
        }

        private void button_DeleteInventory_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Inventory_Record where Serial=@Serial";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", textBox2.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a >= 0)
            {
                MessageBox.Show("Data deleted successfully");
                dataGridView2();
                InventoryManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data not deleted");
            }
        }





        //===============================Tea Plant Record================================
        private void button_TeaAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into TeaPlant_Record values (@serial,@TeaType,@Quantity,@Cost,@Note,@userName)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@serial", Convert.ToInt16(textBox7.Text));
            cmd.Parameters.AddWithValue("@TeaType", textBox10.Text);
            cmd.Parameters.AddWithValue("@Quantity", textBox9.Text);            
            cmd.Parameters.AddWithValue("@Cost", textBox8.Text);
            cmd.Parameters.AddWithValue("@Note", textBox3.Text);
            cmd.Parameters.AddWithValue("@userName", LogIn_Form.saveUsername);

            Product = Convert.ToInt16(textBox9.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted Successfully");
                dataGridView_TeaRecord();
                TeaManagement_ResetText();
                PassToAnalysisTable();
            }
            else
            {
                MessageBox.Show("Data Inserted Failed");
            }
        }

        private void PassToAnalysisTable()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Analysis_Table values (@Name,@Product,@Cost,@Sell)";
            SqlCommand cmd = new SqlCommand(query, con);
                       
            cmd.Parameters.AddWithValue("@Name", LogIn_Form.saveUsername);
            cmd.Parameters.AddWithValue("@Product", Product);
            cmd.Parameters.AddWithValue("@Cost", Cost);
            cmd.Parameters.AddWithValue("@Sell", Sell);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        private void dataGridView_TeaRecord()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from TeaPlant_Record where [User Name]='" + username + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView3.DataSource = data;

            //autosize
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void TeaManagement_ResetText()
        {
            textBox10.Clear();
            textBox9.Clear();
            textBox8.Clear();
            textBox8.Clear();
            textBox3.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TeaManagement_ResetText();
        }

        private void button_TeaUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update TeaPlant_Record set Serial=@Serial, [Tea Type]=@TeaType, Quantity=@Quantity, Cost=@Cost, Note=@Note where Serial=@Serial";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", Convert.ToInt16(textBox7.Text));
            cmd.Parameters.AddWithValue("@TeaType", textBox10.Text);
            cmd.Parameters.AddWithValue("@Quantity", textBox9.Text);
            cmd.Parameters.AddWithValue("@Cost", textBox8.Text);
            cmd.Parameters.AddWithValue("@Note", textBox3.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data updated Successfully");
                dataGridView_TeaRecord();
                TeaManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data updated Failed");
            }
        }

        private void dataGridView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox7.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            textBox10.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            textBox9.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            textBox8.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            textBox3.Text = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button_teaDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from TeaPlant_Record where Serial=@Serial";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Serial", textBox7.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a >= 0)
            {
                MessageBox.Show("Data deleted successfully");
                dataGridView_TeaRecord();
                TeaManagement_ResetText();
            }
            else
            {
                MessageBox.Show("Data not deleted");
            }
        }
    }
}
