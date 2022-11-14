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
    public partial class Rent_History : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");

       // SqlDataAdapter adapt;
        public Rent_History()
        {
            InitializeComponent();
            //DisplayData();
            lodtable();
            //serch();
        }
        private void serch()
        {
            try
            {
                int count = 0;
                con.Open();


                SqlCommand cmd = con.CreateCommand();
               cmd.CommandType = CommandType.Text;
                string name = txtSearch.Text;
                cmd.CommandText = "select * from Rent where  c_name LIKE '%"+ name +"%' ";

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
        private void lodtable()
        {
            try
            {

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Rent";
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
        //private void DisplayData()
        //{
        //    con.Open();
        //    DataTable dt = new DataTable();
        //    adapt = new SqlDataAdapter("select * from Rent", con);

        //    adapt.Fill(dt);
        //    dataGridView1.DataSource = dt;

        //    con.Close();
        //}
        private void Rent_History_Load(object sender, EventArgs e)
        {

        }

        private void cellclick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;

                txtdelete.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                


            }
        }

        private void btndlt_Click(object sender, EventArgs e)
        {
            if (!(txtdelete.Text == string.Empty))
            {


                string query = "Delete from Rent where ID = '" + this.txtdelete.Text + "'";
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

        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int count = 0;
            //    con.Open();


            //    SqlCommand cmd = con.CreateCommand();
            //    cmd.CommandType = CommandType.Text;
            //    string name = txtSearch.Text;
            //    cmd.CommandText = "select * from Rent where  c_name like  '%" + name + "%' ";

            //    cmd.ExecuteNonQuery();
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(dt);
            //    count = Convert.ToInt32(dt.Rows.Count.ToString());
            //    dataGridView1.DataSource = dt;
            //    txtSearch.Clear();
            //    con.Close();
            //    if (count == 0)
            //    {
            //        MessageBox.Show("Record not found!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error" + ex);

            //}
            serch();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void enter(object sender, KeyEventArgs e)
        {
           //
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}
