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
    public partial class student_Dash : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public student_Dash()
        {
            InitializeComponent();
        }

        public string count(string query)
        {
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            string count = "0";
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    count = dr[0].ToString();
                }
            }
            con.Close();
            return count;
        }

        private void student_Dash_Load(object sender, EventArgs e)
        {
            bookCount.Text = count("select dbo.studentDashboard("+Global.UserId+",'total_books')");
            fineCount.Text = count("select dbo.studentDashboard(" + Global.UserId + ",'total_fine')");
            issueCount.Text = count("select dbo.studentDashboard(" + Global.UserId + ",'total_issued')");
            returnCount.Text = count("select dbo.studentDashboard(" + Global.UserId + ",'total_returned')");
        }
    }
}
