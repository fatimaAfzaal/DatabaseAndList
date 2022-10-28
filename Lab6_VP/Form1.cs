using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab6_VP
{
    public partial class Form1 : Form
    {
        public static ListView listView1 = new ListView();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)                     //save data
        {
            //check if all fields are selected or not

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false) || textBox6.Text == "")
            {
                MessageBox.Show("Please fill out missing requirement");
            }

            else
            {
                bool i = true;
                foreach (ListViewItem item in Form1.listView1.Items)
                {
                    if(item.SubItems[4].Text== textBox5.Text)              //no duplicate data
                    {
                        MessageBox.Show("Unable to enter this data!!!\nPlease check warehouse no. again");
                        i = false;
                    }
                }
                if (i)                    //if data is correct
                {
                    ListViewItem list = new ListViewItem(textBox1.Text + " " + textBox2.Text);

                    String str = "Female";
                    if (radioButton1.Checked == true)
                    {
                        str = "Male";
                    }

                    list.SubItems.Add(textBox3.Text);
                    list.SubItems.Add(str);
                    list.SubItems.Add(textBox4.Text);
                    list.SubItems.Add(textBox5.Text);
                    list.SubItems.Add(textBox6.Text);

                    listView1.Items.Add(list);

                    MessageBox.Show("Record saved successfully");


                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
            }
        }

            private void button2_Click(object sender, EventArgs e)              //show button
            {
                Form2 form2 = new Form2();
                form2.Show();
                Hide();
            }

            private void button4_Click(object sender, EventArgs e)            //update button
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please first load the data for update");
                }

                else
                {
                    foreach (ListViewItem item in Form1.listView1.Items)
                    {
                        String str = "Female";
                        if (radioButton1.Checked == true)
                        {
                            str = "Male";
                        }
                        if (item.SubItems[4].Text == textBox7.Text)
                        {
                            item.SubItems[0].Text = textBox1.Text + " " + textBox2.Text;
                            item.SubItems[1].Text = textBox3.Text;
                            item.SubItems[2].Text = str;
                            item.SubItems[3].Text = textBox4.Text;
                            item.SubItems[4].Text = textBox5.Text;
                            item.SubItems[5].Text = textBox6.Text;

                            MessageBox.Show("Record update successfully");
                        }

                    }
                }
        }

        private void button5_Click(object sender, EventArgs e)            //load button
        {
            bool flag = true;
            if (textBox7.Text == "")
            {
                MessageBox.Show("Please enter ware-house no. to load data");
            }
            else
            {

                foreach (ListViewItem item in Form1.listView1.Items)
                {
                    if (item.SubItems[4].Text == textBox7.Text)
                    {
                        String[] arr = item.SubItems[0].Text.Split(' ');

                        textBox1.Text = arr[0];
                        textBox2.Text = arr[1];
                        if (item.SubItems[2].Text == "Female")
                        {
                            radioButton2.Checked = true;
                        }
                        else
                        {
                            radioButton1.Checked = true;
                        }
                        textBox3.Text = item.SubItems[1].Text;
                        textBox4.Text = item.SubItems[3].Text;
                        textBox5.Text = item.SubItems[4].Text;
                        textBox6.Text = item.SubItems[5].Text;
                       // MessageBox.Show("Record load successfully");
                        flag = false;
                    }

                }
                if (flag)
                {
                    MessageBox.Show("No record present with this ware house number");
                }
            }
           
        }

        private void button6_Click(object sender, EventArgs e)         //Reset button
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)           //Add to database button
        {

            foreach (ListViewItem item in Form1.listView1.Items)
            {
                try
                {
                    String FullName = item.SubItems[0].Text;
                    String ContactNo = item.SubItems[1].Text;
                    String Gender = item.SubItems[2].Text;
                    String Address = item.SubItems[3].Text;
                    String WarehouseNo = item.SubItems[4].Text;
                    String WarehouseName = item.SubItems[5].Text;


                    String c = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mootu & Patlu\source\repos\Lab6_VP\Lab6_VP\Entry.mdf;Integrated Security=True";
                    SqlConnection con = new SqlConnection(c);

                    String query = "INSERT INTO record(FullName,ContactNo,Gender,Address,WarehouseNo,WarehouseName) VALUES ('" + FullName + "' , '" + ContactNo + "' , '" + Gender + "' , '" + Address + "' , '" + WarehouseNo + "' , '" + WarehouseName + "')";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Data added to database successfully");
                        
                        foreach (ListViewItem items in Form1.listView1.Items)
                        {
                            item.Remove();
                        }


                    }
                    else if (i == 0)
                    {
                        MessageBox.Show("Sorry! Data not inserted insertion");
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Please recheck your data\nWe are unable to add it to database");
                }
                
            }

        }
    }
}
