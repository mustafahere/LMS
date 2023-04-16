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
    public partial class Signup : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");
        public Signup()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand query = new SqlCommand("insert into tblUsers values ('"+fullName.Text+"','"+email.Text+"','"+password.Text+ "',CURRENT_TIMESTAMP ,1,'student')", con);
                int result = query.ExecuteNonQuery();
                if (result == 2) {
                    MessageBox.Show("Account registerd successfully!");
                    Login obj = new Login();
                    this.Hide();
                    obj.Show();
                }
                con.Close();
            }
            catch(SqlException se)
            {
                MessageBox.Show(se.Message);
                con.Close();
            }
            catch(Exception error)
            {
                MessageBox.Show("Error"+error);
                con.Close();
            }
        }
    }
}
