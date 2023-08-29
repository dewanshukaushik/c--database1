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

namespace database1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123;");
            // con.Open();
            // MessageBox.Show("connection is ready");
            statecomb.SelectedIndex = 0;
            fetchalldata();



        }

        private void btnexecute_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123;");
            con.Open();


            // MessageBox.Show("connection is ready");
            string str = txtquery.Text;//"create table dewanshu_std(roll_no varchar(20),name varchar(50),father varchar(50), state varchar(50), gender varchar(1))";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("query Executed");
            //drop table dewanshu_std;
            //select * from dewanshu_std
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (checkexistance() == false)
            {
                if (txtrollno.Text == "" || txtname.Text == "" || txtfather.Text=="")
                        {
                    MessageBox.Show("please fill all the entries");

                }
                else
                {

                
                SqlConnection con = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123;");
                con.Open();
                string gender = "M";
                if (rbmale.Checked == true)
                    gender = "M";
                else
                    gender = "F";

                string str = " Insert into dewanshu_std(roll_no,name,father,state,gender) values ('" + txtrollno.Text + "', '" + txtname.Text + "', '" + txtfather.Text + "', '" + statecomb.SelectedItem.ToString() + "', '" + gender + "')";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data saved");
                fetchalldata();
                    txtrollno.Text =  "" ;
                    txtname.Text = "";
                    txtfather.Text = "";
                }
            }
            else
                MessageBox.Show("sorry, student with same roll number already exists.");

        
        }

        private void statecomb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123;");
            conn.Open();
            string str=txtquery.Text;
            SqlDataAdapter ada = new SqlDataAdapter(str, conn);
            DataTable dttab = new DataTable();
            ada.Fill(dttab);    
            conn.Close();
            dataGridView1.DataSource = dttab;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool checkexistance()
        {
            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123;");
            conn.Open();
            string str = "Select * from dewanshu_std where roll_no='"+txtrollno+"'";
            SqlDataAdapter ada = new SqlDataAdapter(str, conn);
            DataTable dttab = new DataTable();
            ada.Fill(dttab);
            conn.Close();
            if (dttab.Rows.Count >0 )
                return true;
            else return false;      
        }
        private void fetchalldata()
        {
            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123;");
            conn.Open();
            string str = "Select * from dewanshu_std order by name";
            SqlDataAdapter ada = new SqlDataAdapter(str, conn);
            DataTable dttab = new DataTable();
            ada.Fill(dttab);
            conn.Close();
            listView1.Items.Clear();
            if (dttab.Rows.Count > 0)
            {
                for (int i = 0; i < dttab.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dttab.Rows[i].ItemArray[0].ToString());
                    li.SubItems.Add(dttab.Rows[i].ItemArray[1].ToString());//name
                    li.SubItems.Add(dttab.Rows[i].ItemArray[2].ToString());//father
                    li.SubItems.Add(dttab.Rows[i].ItemArray[4].ToString());//state
                    li.SubItems.Add(dttab.Rows[i].ItemArray[3].ToString());//gender
                    listView1.Items.Add(li);

                }
            }

        }

        private void refresh_Click(object sender, EventArgs e)
        {

            fetchalldata();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            txtrollno.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtname.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtfather.Text = listView1.SelectedItems[0].SubItems[2].Text;
            if (listView1.SelectedItems[0].SubItems[3].Text=="M")
                rbmale.Checked = true;
                else
                rbfemale.Checked = true;
            statecomb.SelectedItem= listView1.SelectedItems[0].SubItems[4].Text;

        }
    }
}
