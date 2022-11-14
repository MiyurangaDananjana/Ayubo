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

namespace Ayubo
{
    public partial class customer : Form
    {
        
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
        string gender;
        
        SqlDataAdapter adapt;

        public customer()
        {
            InitializeComponent();
            DisplayData();
            panel1.BackColor = Color.FromArgb(180, Color.RosyBrown);
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            lodtable();

        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from New_Package", con);

            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }
        private void customer_Load(object sender, EventArgs e)
        {

        }
        private void lodtable()
        {
            try
            {

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Customer";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "" && txtname.Text != "" && txtphone.Text != "" && txtaddress.Text != "")
            {
                if (radioButton1.Checked == true)
                {
                    gender = "Male";
                }
                else
                    gender = "Female";




                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string ID = txtid.Text;
                string name = txtname.Text;
                string phone = txtphone.Text;
                string address = txtaddress.Text;
                string email = txtemail.Text;

                cmd.CommandText = "insert into Customer (ID_NO,c_name,c_tp,c_address,c_email,gender) values ('" + ID + "', '" + name + "', '" + phone + "', '" + address + "', '" + email + "', '" + gender + "' )";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record has been inserted!");


                lodtable();
                


                txtid.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtemail.Clear();
                txtaddress.Clear();
                
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
        
            }
        }

        
        private void ClearData()
        {
            txtname.Text = "";
            txtid.Text = "";
            txtphone.Text = "";
            txtemail.Text = "";
            txtaddress.Text = "";
            radioButton1.Text = "";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!(txtdelete.Text == string.Empty))
            {


                string query = "Delete from Customer where ID = '" + this.txtdelete.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader myreader;
                try
                {
                    con.Open();
                    myreader = cmd.ExecuteReader();
                    MessageBox.Show("successfully data Deleted", "user information");
                    while (myreader.Read())
                    {
                    }
                    con.Close();
                    
                    txtdelete.Clear();
                    lodtable();
                }
                catch (Exception)
                {
                    MessageBox.Show("Record Deleted Successfully!");
                }
            }
            else
            {
                MessageBox.Show("enter ID which you want to delete", "User information");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "" && txtname.Text != "" && txtphone.Text != "" && txtaddress.Text != "" && txtemail.Text != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                int id = Convert.ToInt32(txtdelete.Text);
                string cid = txtid.Text;
                string name = txtname.Text;
                string phone = txtphone.Text;
                string address = txtaddress.Text;
                string email = txtemail.Text;

                cmd.CommandText = "update  Customer  set ID_NO= '" + cid + "',c_name='" + name + "',c_tp='" + phone + "',c_address='" + address + "',c_email='" + email + "' where ID = '" + id + "' ";
                
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("Update Custamer");

                txtid.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtemail.Clear();
                txtaddress.Clear();

                txtSearch.Clear();
                lodtable();
                

                
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                int count = 0;
                con.Open();
                

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string name = txtSearch.Text;            
                cmd.CommandText = "select * from customer where  ID_NO like  '%" + name + "%' ";
                cmd.CommandText = "select * from customer where  c_name like  '%" + name + "%' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                txtSearch.Clear();
                con.Close();
                if (count == 0)
                {
                    MessageBox.Show("Record not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            txtid.Clear();
            txtname.Clear();
            txtphone.Clear();
            txtemail.Clear();
            txtaddress.Clear();
            txtdelete.Clear();
            txtSearch.Clear();
            MessageBox.Show(" Successfully Clear !");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cellcustomer(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                
                txtdelete.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtphone.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtaddress.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtemail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();



            }
        }
    }
}
    