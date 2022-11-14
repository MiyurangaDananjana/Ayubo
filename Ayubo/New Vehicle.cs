using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ayubo
{
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
        
        SqlDataAdapter adapt;
        
        public Form3()
        {

            InitializeComponent();
            DisplayData();
            lodtable();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            vehicletype();

        }

        public void vehicletype()
        {
            ///SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
            string sql = "select * from Vehicle_Type";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string sname = myreader.GetString(1);
                    comboBox1.Items.Add(sname);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }
        private void Form3_Load(object sender, EventArgs e)
        {
            
            

        }

        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
            string sql = "select * from Vehicle_Type where vehicle_type='" + comboBox1.Text + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    string id = myreader.GetInt32(0).ToString();
                    string price = myreader.GetString(2);
                    txtprice.Text = price;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox3.Text != "" && textBox2.Text != "")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string Vehicle_No = textBox3.Text;
                string Vehicle_Name = textBox2.Text;
                string Vehicle_Type = comboBox1.Text;
                string Vehicle_Price = txtprice.Text;

                cmd.CommandText = "insert into New_Vehicle (Vehicle_No,Vehicle_Name,Vehicle_Type,Vehicle_Price) values ('" + textBox3.Text + "', '" + textBox2.Text + "', '" + comboBox1.Text + "', '" + txtprice.Text + "'  )";
                label1.Text = "Data Insert Successfull!";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record has been inserted!");
                lodtable();

                textBox2.Clear();
                textBox3.Clear();
                
            }
            else
            {
                
                label1.Text = "Please Provide Details!";
            }

        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from New_Vehicle", con);
            
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            
            con.Close();
        }
        

        private void lodtable()
        {
            try
            {

                con.Open();
                

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from New_Vehicle";
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


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (!(txtdelete.Text == string.Empty))
            {


                string query = "Delete from New_Vehicle where ID = '" + this.txtdelete.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader myreader;
                label1.Text = "Data Delete Successfull!";
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
                catch (Exception )
                {
                    MessageBox.Show("Record Deleted Successfully!");
                }
            }
            else
            {
                
                label1.Text = "enter ID which you want to delete";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox2.Text != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                int id = Convert.ToInt32(txtdelete.Text);
                string nomber = textBox3.Text;
                string name = textBox2.Text;
                string type = comboBox1.Text;
                string price = txtprice.Text;

                cmd.CommandText = "update  New_Vehicle  set Vehicle_No= '" + nomber + "',Vehicle_Name='" + name + "',Vehicle_Type='" + type + "',Vehicle_Price='" + price + "' where id = '" + id + "' ";
                label1.Text = "Data Update Successfull!";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Update Vehicle");
                lodtable();



                
            }
            else
            {
                
                label1.Text = "Please Select Record to Update";

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string name = txtSearch.Text;

                cmd.CommandText = "select * from customer where  ID_NO like  '%" + name + "%' ";

                cmd.CommandText = "select * from New_Vehicle where  Vehicle_No like  '%" + name + "%' ";
                label1.Text = "Search Vehicle Nombare !";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            txtSearch.Clear();
            txtdelete.Clear();
            comboBox1.Text = "";
            txtprice.Clear();
            //label1.Text = " TextBox Data Clear Successfull!";
        }

        private void cellvehicle(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                
                txtdelete.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtprice.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();


            }
        }
    }
}
