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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Signup obj = new Signup();
            obj.Show();
        }

        public void GoToDashboard(string role, int id, string name)
        {
            if (role != "")
            {
                this.Hide();
                Global.UserId = id;
                Global.UserName = name;
                switch (role)
                {
                    case "super_admin":
                        SuperAdminDashboard obj = new SuperAdminDashboard();
                        obj.Show();
                        break;

                    case "admin":
                        AdminDashboard obj2 = new AdminDashboard();
                        obj2.Show();
                        break;

                    case "student":
                        StudentDashboard obj3 = new StudentDashboard();
                        obj3.Show();
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand query = new SqlCommand("Exec _Login '"+email.Text+"','"+password.Text+"'", con);
                SqlDataReader reader = query.ExecuteReader();
                string role="";
                int id = 0;
                string name = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        role = reader.GetString(0);
                        id = reader.GetInt32(1);
                        name = reader.GetString(2);
                        break;
                    }
                }
                GoToDashboard(role,id,name);

                con.Close();
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
    }
}
