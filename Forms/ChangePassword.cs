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
    public partial class ChangePassword : Form
    {
        private Main Main;
        public ChangePassword(Main MW)
        {
            Main = MW;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==Main.Current_User.Get_Password())
            {
                if(textBox2.Text==textBox3.Text && textBox2.Text.Length>=Main.Current_User.Get_limitation())
                {
                    Main.Current_User.SetPassword(textBox2.Text);
                    MessageBox.Show("Password changed. Your account will be signed out.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Passwords is not equial or new password is too short...","Warning!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    
                }
            }
            else
            {
                MessageBox.Show("Invalid password.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
