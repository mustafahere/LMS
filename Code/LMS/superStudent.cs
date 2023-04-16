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
    public partial class superStudent : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public superStudent()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void Select()
        {
            SqlDataAdapter da = new SqlDataAdapter("select id,userName,email,createdAt,isActive, booksIssued, booksReturned from _students", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           
        }


        public void fillStudentId()
        {
            try
            {
                SqlCommand com = new SqlCommand("select id from tblUsers where userRole='student'", con);

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


        private void superStudent_Load(object sender, EventArgs e)
        {
            btn.Hide();
            Select();
            fillStudentId();
        }

        private void txtId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("select userName,isActive from tblUsers where id=" + txtId.Text + "", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr.GetString(0);
                        if(dr.GetInt32(1) == 1)
                        {
                            btn.Text = "Deactivate";
                        }
                        else
                        {
                            btn.Text = "Activate";
                        }

                        btn.Show();
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

        private void btn_Click(object sender, EventArgs e)
        {
            int status = 0;
            if(btn.Text == "Activate") {
                status = 1;
            }
            if (btn.Text == "Deactivate") {
                status = 0;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update tblUsers set isActive="+status+" where id="+txtId.Text+"", con);
                int result = cmd.ExecuteNonQuery();

                if (result == 1)
                {
                    MessageBox.Show("Account "+ btn.Text +"d Successfully");
                    Select();
                    
                }
                else
                {
                    MessageBox.Show("Something went wrong!");
                }

                
                con.Close();
                txtId.Items.Clear();
                txtName.Text = "";
                btn.Hide();
                fillStudentId();
            }
            catch (Exception er)
            {
                con.Close();
                MessageBox.Show(er.Message);
            }

        }
    }
}
