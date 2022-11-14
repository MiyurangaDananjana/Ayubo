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
    public partial class Package : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
        SqlDataAdapter adapt;
        int ID = 0;
        public Package()
        {
            InitializeComponent();
            DisplayData();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            panel1.BackColor = Color.FromArgb(180, Color.RosyBrown);
            lodtable();
            ClearData();


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
        private void ClearData()
        {
            txtpname.Text = "";
            txtdc.Text = "";
            txtwc.Text = "";
            txtekc.Text = "";
            txtonc.Text = "";
            txtpc.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            

        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lodtable()
        {
            try
            {

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from New_Package";
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
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtpname.Text != "" && txtwc.Text != "")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string pname = txtpname.Text;
                string dc = txtdc.Text;
                string wc = txtwc.Text;
                string ekc = txtekc.Text;
                string onc = txtonc.Text;
                string pc = txtpc.Text;
                string km = textBox1.Text;
                string hour = textBox2.Text;
                string chargehour = textBox3.Text;

                cmd.CommandText = "insert into New_Package (p_name,driver_c,waiting_c,extra_km_c,over_n_c,parking_c,maximumkm,maximumhour,extra_hour_c) values ('" + pname+ "', '" + dc+ "', '" + wc + "', '" + ekc + "', '" + onc + "' , '" + pc + "', '" + km + "', '" + hour + "', '" + chargehour + "')";
                
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record has been inserted!");

                ClearData();
                lodtable();




            }
            else
            {

                
                MessageBox.Show("Please Provide Details!");
            }
        

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Package_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtpname.Text != "" && txtdc.Text != "" && txtwc.Text != "" && txtekc.Text != "" && txtonc.Text != "" && txtpc.Text != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                int id = Convert.ToInt32(ID);
                string pname = txtpname.Text;
                string dc = txtdc.Text;
                string wc = txtwc.Text;
                string ekc = txtekc.Text;
                string onc = txtonc.Text;
                string pc = txtpc.Text;
                string km = textBox1.Text;
                string hour = textBox2.Text;
                string chargehour = textBox3.Text;

                cmd.CommandText = "update  New_Package  set  p_name= '" + pname + "',    driver_c ='" + dc + "',waiting_c='" + wc + "',extra_km_c='" + ekc + "',over_n_c='" + onc + "',parking_c='" + pc + "',maximumkm='" + km + "',maximumhour='" + hour + "',extra_hour_c='" + chargehour + "'  where ID = '" + id + "' ";
                
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("Update Custamer");

                ClearData();

                txtSearch.Clear();
                lodtable();



               
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!(txtdelete.Text == string.Empty))
            {


                string query = "Delete from New_Package where ID = '" + this.txtdelete.Text + "'";
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
                    ClearData();

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

                cmd.CommandText = "select * from New_Package where  p_name like  '%" + name + "%' ";

                

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

        private void cellpackage(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtdelete.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtpname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtdc.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtwc.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtekc.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtonc.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtpc.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();



            }
        }
    }
}
