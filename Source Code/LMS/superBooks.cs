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
    public partial class superBooks : UserControl
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public superBooks()
        {
            InitializeComponent();
        }

        public void Select()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from _books", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void fillAuthorsId()
        {
            try {
                SqlCommand com = new SqlCommand("select id from tblAuthors", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        authorId.Items.Add(dr.GetValue(0).ToString());
                    }
                }

                con.Close();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
                con.Close();
            }

        }

        public void fillBooksId()
        {
            try
            {
                SqlCommand com = new SqlCommand("select id from tblBooks", con);

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

        private void superBooks_Load(object sender, EventArgs e)
        {
            Select();
            fillAuthorsId();
            fillBooksId();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void authorId_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public void clearData()
        {
            txtId.Items.Clear();
            txtQuantity.Text = "";
            txtDescription.Text = "";
            txtTitle.Text = "";
            txtType.Text = "";
            authorId.Items.Clear();
            authorName.Text = "";
        }
        private void authorId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("select authorName from tblAuthors where id="+authorId.Text+"", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        authorName.Text = dr.GetString(0);
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

        private void button1_Click(object sender, EventArgs e)
        {
            runCommand("insert into tblBooks values('"+txtTitle.Text+"',"+authorId.Text+",'"+txtType.Text+"','"+txtDescription.Text+"','"+txtQuantity.Text+"',CURRENT_TIMESTAMP)", "Book Inserted Successfully!");
            clearData();
            fillBooksId();
            fillAuthorsId();
        }


        public void runCommand(string query, string msg)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();

                if (result == 1)
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
            catch (Exception er)
            {
                con.Close();
                MessageBox.Show(er.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runCommand("update tblBooks set bookTitle='"+txtTitle.Text+"',authorId='"+authorId.Text+"',bookType='"+txtType.Text+"',description='"+txtDescription.Text+"',quantity='"+txtQuantity.Text+"' where id = " + txtId.Text+"", "Book Updated Successfully!");
            clearData();
            fillBooksId();
            fillAuthorsId();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            runCommand("delete from tblBooks where id="+txtId.Text+"", "Book Deleted Successfully!");
            clearData();
            fillBooksId();
            fillAuthorsId();

        }

        private void txtId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("select * from tblBooks where id=" + txtId.Text + "", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();

                string aId = "";
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtTitle.Text = dr.GetString(1);
                        txtType.Text = dr.GetString(3);
                        txtDescription.Text = dr.GetString(4);
                        txtQuantity.Text = dr.GetInt32(5).ToString();
                        aId = dr.GetInt32(2).ToString();
                    }
                }

                con.Close();
                authorId.Text = aId;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                con.Close();
            }
        }
    }
}
