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
    public partial class Form5 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");

        SqlDataAdapter adapt;
        
        public Form5()
        {
            InitializeComponent();
            DisplayData();
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            lodtable();
        }
        private void lodtable()
        {
            try
            {

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Vehicle_Type";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox5.Text != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Vehicle_Type(vehicle_type,vehicle_price)VALUES('" + textBox1.Text + "','" + textBox5.Text + "')";

                cmd.ExecuteNonQuery();
                MessageBox.Show("Save New Vehicle Type And Price");
                textBox1.Clear();
                textBox5.Clear();
                con.Close();
                lodtable();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Vehicle_Type ", con);

            adapt.Fill(dt);
            dataGridView2.DataSource = dt;

            con.Close();
        }
        private void ClearData()
        {
            textBox1.Text = "";
            textBox5.Text = "";
            

        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to Update?", "Update Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    int id = Convert.ToInt32(txtdelete.Text);
                    string type = textBox1.Text;
                    string price = textBox5.Text;

                    cmd.CommandText = "update Vehicle_Type  set vehicle_type= '" + type + "', vehicle_price='" + price+ "' where id = '" + id + "' ";
                    
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lodtable();
                    
                    MessageBox.Show("Record has been Updated!");
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!(txtdelete.Text == string.Empty))
            {


                string query = "Delete from Vehicle_Type where ID = '" + this.txtdelete.Text + "'";
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
                    lodtable();
                    txtdelete.Clear();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string name = txtSearch.Text;
                cmd.CommandText = "select * from Vehicle_Type where  vehicle_type like  '%" + name + "%' ";                
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView2.DataSource = dt;
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

        private void celltype(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                txtdelete.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox5.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtdelete.Clear();
            textBox1.Clear();
            textBox5.Clear();
        }
    }
}
