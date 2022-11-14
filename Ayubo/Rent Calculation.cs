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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
        string driver;
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(130, Color.Aqua);
            vehicleno();
            clear();
            Custome();
        }
        private void clear()
        {
            comboBox2.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            txtprice.Clear();
            txttdays.Clear();
            txttotal.Clear();
            textBox1.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(150, Color.Aqua);
        }
        public void vehicleno()
        {
            
            string sql = "select * from New_Vehicle";
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
        public void Custome()
        {

            string sql = "select * from Customer";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string cname = myreader.GetString(1);
                    comboBox2.Items.Add(cname);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select * from New_Vehicle where Vehicle_No='" + comboBox1.Text + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    string id = myreader.GetInt32(0).ToString();
                    string price = myreader.GetString(4);
                    txtprice.Text = price;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////// get total Day
            DateTime Rent_Date = dateTimePicker1.Value.Date;
            DateTime Return_Date = dateTimePicker2.Value.Date;

            int Dateiff = ((TimeSpan)(Return_Date - Rent_Date)).Days;
            txttdays.Text = Dateiff.ToString();

            if (txtprice.Text != "" && txttdays.Text != "" && comboBox2.Text != "")
            {
                
                

                if (checkBox1.Checked == true)
                {
                    
                    int price, day, Ans, driverCost;
                    price = int.Parse(txtprice.Text);
                    day = int.Parse(txttdays.Text);
                    driverCost = int.Parse(textBox1.Text);

                    Ans = ((price+ driverCost) *day);


                    txttotal.Text = Ans.ToString();

                   // driver = "With Driver";
                    driver = textBox1.Text;


                }
                //if (checkBox1.Checked == false)
                else
                {
                    
                    int price, day, Ans;
                    price = int.Parse(txtprice.Text);
                    day = int.Parse(txttdays.Text);
                    Ans = price * day;
                    txttotal.Text = Ans.ToString();
                    driver = "Not Driver";
                }

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string cname = comboBox2.Text;
                string vehicle_no = comboBox1.Text;
                //withdriver
                int days = Convert.ToInt32(txttdays.Text);
                int charge = Convert.ToInt32(txttotal.Text);




                cmd.CommandText = "insert into Rent (c_name,vehicle_no,with_driver,total_days,total_charge) values ('" + cname + "', '" + vehicle_no + "', '" + driver+ "', '" + days + "' , '" + charge + "'  )";
                label9.Text = "Data Insert Successfull!";
                cmd.ExecuteNonQuery();
                con.Close();
                
                











            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            label9.Text = "Clear Textbox ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rent_History frm2 = new Rent_History();
            frm2.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
    
}
