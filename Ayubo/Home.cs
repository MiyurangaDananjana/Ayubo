using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SqlClient;

namespace Ayubo
{
    public partial class Home : Form
    {
        //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NBIQEL;Initial Catalog=dbAyubo;Integrated Security=True");
        public Home()
        {
            InitializeComponent();
            label3.Text = "Database Connection Successfull!";


            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form1 frm2 = new Form1();
            frm2.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form3 frm2 = new Form3();
            frm2.ShowDialog();
        }

        private void loginFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login frm2 = new Login();
            frm2.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Form5 frm2 = new Form5();
            frm2.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            customer frm2 = new customer();
            frm2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Package frm2 = new Package();
            frm2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           Day_tour frm2 = new Day_tour();
            frm2.ShowDialog();
        }

        private void rentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rent_History frm2 = new Rent_History();
            frm2.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Long_tour frm2 = new Long_tour();
            frm2.ShowDialog();
        }
    }
}
