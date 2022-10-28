using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6_VP
{
    public partial class Form2 : Form


    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)     //show current record button
        {
            if (Form1.listView1.Items.Count == 0)
            {
                MessageBox.Show("No current data in list.\nData may saved in datbase");
            }
            else
            {
                dataGridView1.Visible = false;
                listView1.Visible = true;
                listView1.Items.Clear();
                foreach (ListViewItem item in Form1.listView1.Items)
                {
                    listView1.Items.Add((ListViewItem)item.Clone());
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)                         //search by last name button
        {
            dataGridView1.Visible = false;
            listView1.Visible = true;
            bool flag = true;
            listView1.Items.Clear();
           if (Form1.listView1.Items.Count == 0)
            {
                MessageBox.Show("No record present with last name: " + textBox1.Text);
            }

            foreach (ListViewItem item in Form1.listView1.Items)
            {
                string[] arr = item.Text.Split(' ');
               
                if (arr.Length >= 2)
                {
                    if (arr[1].ToUpper() == textBox1.Text.ToUpper())
                    {
                        listView1.Items.Add((ListViewItem)item.Clone());
                        if(Form1.listView1.Items.Count != 0)
                             flag = false;
                    }
            }
                if (flag)
                {
                    MessageBox.Show("No record present with last name: " + textBox1.Text);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)                 //back icon
        {
            Form1 form1 = new Form1();
            form1.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)                       //show record from database
        {
            listView1.Visible = false;
            dataGridView1.Visible = true;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mootu & Patlu\source\repos\Lab6_VP\Lab6_VP\Entry.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from record", con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;
           
            MessageBox.Show("Data loaded from database successfully");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            listView1.Visible = true;
        }
    }
}
