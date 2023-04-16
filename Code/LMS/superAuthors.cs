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
namespace LMS
{
    public partial class superAuthors : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public superAuthors()
        {
            InitializeComponent();
        }

        public void clearData()
        {
            txtId.Items.Clear();
            txtName.Text = "";
        }

        public void Select()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tblAuthors", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void fillAuthorsId()
        {
            try
            {
                SqlCommand com = new SqlCommand("select id from tblAuthors", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtId.Items.Add(dr.GetValue(0).ToString());
                    }
                }

                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                con.Close();
            }

        }


        private void superAuthors_Load(object sender, EventArgs e)
        {
            Select();
            fillAuthorsId();
        }

        public void runCommand (string query, string msg)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();

                   if(result == 1)
                   {
                        MessageBox.Show(msg);
                        Select();

                   }
                    else
                    {
                    MessageBox.Show("Something went wrong!");
                    }
                con.Close();
            }
            catch(SqlException se)
            {
                con.Close();
                if(se.Number == 547)
                {
                    MessageBox.Show("Can't delete author, because author exist in books");

                }
                
            }
            catch (Exception er)
            {
                con.Close();
                MessageBox.Show(er.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runCommand("insert into tblAuthors values('"+txtName.Text+"')", "Author Added Successfully!");
            clearData();
            fillAuthorsId();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runCommand("update tblAuthors set authorName = '" + txtName.Text + "' where id="+txtId.Text+"","Author Updated Successfully!");
            clearData();
            fillAuthorsId();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            runCommand("delete from tblAuthors where id=" + txtId.Text + "", "Author Deleted Successfully!");
            clearData();
            fillAuthorsId();
        }

        private void txtId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("select authorName from tblAuthors where id=" + txtId.Text + "", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr.GetString(0);
                    }
                }

                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                con.Close();
            }
        }
    }
}
