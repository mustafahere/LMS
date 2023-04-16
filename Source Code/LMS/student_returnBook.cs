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
    public partial class student_returnBook : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public student_returnBook()
        {
            InitializeComponent();
        }

        public void Select()
        {
            SqlDataAdapter da = new SqlDataAdapter("select bookTitle,authorName,returnedAt,fine from _returned where studentId = " + Global.UserId + "", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        public void fillBooksId()
        {
            try
            {
                SqlCommand com = new SqlCommand("select bookId from tblIssued where studentId = 2 and returned = 0", con);

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


        private void student_returnBook_Load(object sender, EventArgs e)
        {
            fillBooksId();
            Select();
        }

        private void txtId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("select bookTitle,bookType,description from tblBooks where id=" + txtId.Text + "", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtTitle.Text = dr.GetString(0);
                        txtType.Text = dr.GetString(1);
                        txtDescription.Text = dr.GetString(2);

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
        public void ClearData()
        {
            txtDescription.Text = "";
            txtTitle.Text = "";
            txtType.Text = "";
            txtId.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("exec _returnBook "+Global.UserId+","+txtId.Text+"", con);

                con.Open();

                int result = com.ExecuteNonQuery();
                if (result == 4)
                {
                    MessageBox.Show("Book returned successfully!");
                    ClearData();
                }
                con.Close();
                Select();
                fillBooksId();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                con.Close();
                ClearData();
                fillBooksId();

            }
        }
    }
}
