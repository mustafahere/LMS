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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            this.Hide();
            obj.Show();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            superDash obj = new superDash();
            mainPanel.Controls.Add(obj);
            welcomeTxt.Text = "Welcome "+Global.UserName; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            superStudent obj = new superStudent();
            mainPanel.Controls.Add(obj);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            superBooks obj = new superBooks();
            mainPanel.Controls.Add(obj);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            superDash obj = new superDash();
            mainPanel.Controls.Add(obj);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
