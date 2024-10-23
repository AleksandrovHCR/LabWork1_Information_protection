using LabWork1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabWork1.Forms
{
    public partial class AllUsers : Form
    {

        private Main Main; 
        public AllUsers(Main Main)
        {
            InitializeComponent();
            this.Main = Main;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void AllUsers_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach(User usr in Main.Users)
            
            {
                dataGridView1.Rows.Add();
                dataGridView1[0,i].Value=usr.Get_Username();
               // dataGridView1[1, i].Value = usr.Get_Password();
                dataGridView1[1, i].Value = usr.Get_IsAdmin() ? "Administrator" : "Ordinary";
                dataGridView1[2, i].Value = usr.Get_Blocked() ? "Sealed" : "Active";
                dataGridView1[3, i].Value = ">="+usr.Get_limitation();
                
                i++;
            }
        }
    }
}
