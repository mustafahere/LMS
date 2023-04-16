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
    public partial class superDash : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public superDash()
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

        private void superDash_Load(object sender, EventArgs e)
        {
            stdCount.Text = count("select dbo._countUsers('student')");
            adminCount.Text = count("select dbo._countUsers('admin')");
            booksCount.Text = count("select dbo._countBooks()");
            booksIssuedCount.Text = count("select dbo._countBooksIssued()");
        }
    }
}
