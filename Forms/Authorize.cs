using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabWork1.Classes;

namespace LabWork1.Forms
{
    public partial class Authorize : Form
    {
        private Main Main;
        private int Wrong = 0;
        public Authorize(Main MW)
        {
            Main=MW;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //textBox2.PasswordChar = '*';
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User USER = null;
            string Username=textBox1.Text;
            foreach (User user in Main.Users)
            {
                if (user.Get_Username() == Username) { USER = user; break; }
            }
            if (USER != null)
            {
                if(USER.Get_Password()==textBox2.Text) {
                    if (USER.Get_Blocked())
                    {
                        MessageBox.Show("Access sealed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Main.Current_User = USER;
                        this.Close();
                    }
                }
                else
                {
                    if (Wrong < 3)
                    {
                        MessageBox.Show($"Invalid password! {3 - Wrong} attempts remanin! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); Wrong++;
                    }
                    else
                    {
                        MessageBox.Show($"Invalid password! Access denied. Application closing... ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main.Close();
                        this.Close();

                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid username!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Wrong = 0;
        }
    }
}
