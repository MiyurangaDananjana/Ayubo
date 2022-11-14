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
    public partial class Day_tour : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
        public Day_tour()
        {
            InitializeComponent();
            vehicletype();
            packagetype();

        }
        public void clear()
        {
            comboBox2.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox1.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox2.Clear();
            txtkms.Clear();
            txthours.Clear();
            txtprice.Clear();
            textBox3.Clear();
            txttotal.Clear();
        }
        public void vehicletype()
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            



            
        }
        public void packagetype()
        {
            
            string sql = "select * from New_Package";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string packgename = myreader.GetString(1);
                    comboBox2.Items.Add(packgename);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }
        private void Day_tour_Load(object sender, EventArgs e)
        {
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string sql = "select * from New_Vehicle where Vehicle_No='" + comboBox1.Text+"' ";
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select * from New_Package where p_name='" + comboBox2.Text + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    string id = myreader.GetInt32(0).ToString();
                    //string extra_km_c = myreader.GetString(4);
                    //textBox5.Text = extra_km_c ;
                    //string waiting_c = myreader.GetString(3);
                    //textBox6.Text = waiting_c;
                    string km = myreader.GetString(7);
                    txtkms.Text = km;
                    string hour = myreader.GetString(8);
                    txthours.Text = hour;
                    string chour = myreader.GetString(9);
                    textBox10.Text = chour;
                    string exkm = myreader.GetString(5);
                    textBox13.Text = exkm;
                    string packge = myreader.GetString(2);
                    textBox3.Text = packge;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text !="") 
            {
                DateTime startTime = dateTimePicker1.Value;
                DateTime endTime = dateTimePicker2.Value;

                TimeSpan duration = new TimeSpan(endTime.Ticks - startTime.Ticks);

                textBox8.Text = duration.ToString(@"hh");



                int mhour, nohour, ans;
                mhour = int.Parse(txthours.Text);
                nohour = int.Parse(textBox8.Text);


                ans = (nohour - mhour);


                textBox9.Text = ans.ToString();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }

















        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""&& textBox2.Text != "")
            {
                



                int startkm, endkm, ans;
                startkm = int.Parse(textBox1.Text);
                endkm = int.Parse(textBox2.Text);



                ans = (endkm - startkm);

                textBox11.Text = ans.ToString();

                int mkm, nokm, kmAns;
                mkm = int.Parse(txtkms.Text);
                nokm = int.Parse(textBox11.Text);


                kmAns = (nokm - mkm);


                textBox12.Text = kmAns.ToString();


            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (comboBox2.Text != "" && textBox9.Text != "" && textBox12.Text != "")
            {
                int ehc, noeh, Hourans, ekmc, noekm, kmans, reat, total;
                ehc = int.Parse(textBox10.Text);
                noeh = int.Parse(textBox9.Text);
                ekmc = int.Parse(textBox13.Text);
                noekm = int.Parse(textBox12.Text);
                reat = int.Parse(textBox3.Text);
                Hourans = (ehc * noeh);
                kmans = (ekmc * noekm);
                total = reat + Hourans + kmans;
                txttotal.Text = total.ToString();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
                
















        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
