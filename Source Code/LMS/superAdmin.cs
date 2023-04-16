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
    public partial class superAdmin : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public superAdmin()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Select()
        {
            SqlDataAdapter da = new SqlDataAdapter("select id,userName,email,createdAt,isActive from _admins", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void fillAdminId()
        {
            try
            {
                SqlCommand com = new SqlCommand("select id from tblUsers where userRole='admin'", con);

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


        public void ClearData()
        {
            txtId.Items.Clear();
            txtName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            btn.Hide();
            fillAdminId();
        }

        private void superAdmin_Load(object sender, EventArgs e)
        {
            btn.Hide();
             Select();
            fillAdminId();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand query = new SqlCommand("insert into tblUsers values ('" + txtName.Text + "','" + txtEmail.Text + "','" + txtPassword.Text + "',CURRENT_TIMESTAMP ,1,'admin')", con);
                int result = query.ExecuteNonQuery();
                if (result == 2)
                {
                    MessageBox.Show("Admin registerd successfully!");
                   
                }
                con.Close();
                Select();
                ClearData();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.Message);
                con.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
                con.Close();
            }
        }

        private void txtId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtId.SelectedItem != null) {
                btnAdd.Hide();
            }
            else
            {
                btnAdd.Show();
            }
            try
            {

                SqlCommand com = new SqlCommand("select * from tblUsers where id=" + txtId.Text + "", con);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr.GetString(1);
                        txtEmail.Text = dr.GetString(2);
                        txtPassword.Text = dr.GetString(3);
                        if (dr.GetInt32(5) == 1)
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
            if (btn.Text == "Activate")
            {
                status = 1;
            }
            if (btn.Text == "Deactivate")
            {
                status = 0;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update tblUsers set isActive=" + status + " where id=" + txtId.Text + "", con);
                int result = cmd.ExecuteNonQuery();

                if (result == 1)
                {
                    MessageBox.Show("Account " + btn.Text + "d Successfully");
                }
                else
                {
                    MessageBox.Show("Something went wrong!");
                }


                con.Close();
                Select();
                ClearData();
                btnAdd.Show();
            }
            catch (Exception er)
            {
                con.Close();
                MessageBox.Show(er.Message);
            }
        }
    }
}
