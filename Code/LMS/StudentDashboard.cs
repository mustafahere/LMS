using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS
{
    public partial class StudentDashboard : Form
    {
        public StudentDashboard()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            this.Hide();
            obj.Show();
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            welcomeTxt.Text = "Welcome " + Global.UserName;
            studentPanel.Controls.Clear();
            student_Dash obj = new student_Dash();
            studentPanel.Controls.Add(obj);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            studentPanel.Controls.Clear();
            student_Books obj = new student_Books();
            studentPanel.Controls.Add(obj);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            studentPanel.Controls.Clear();
            student_issueBook obj = new student_issueBook();
            studentPanel.Controls.Add(obj);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            studentPanel.Controls.Clear();
            student_returnBook obj = new student_returnBook();
            studentPanel.Controls.Add(obj);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentPanel.Controls.Clear();
            student_Dash obj = new student_Dash();
            studentPanel.Controls.Add(obj);
        }
    }
}
