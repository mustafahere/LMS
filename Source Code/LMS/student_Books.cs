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
    public partial class student_Books : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3O4IV31\\MUSTAFA;Initial Catalog=LMS;Integrated Security=true;");

        public student_Books()
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


        private void student_Books_Load(object sender, EventArgs e)
        {
            Select();
        }
    }
}
