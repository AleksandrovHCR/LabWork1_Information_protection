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
    public partial class AddUser : Form
    {
        private Main Main;
        public AddUser(Main main)
        {
            InitializeComponent();
            Main = main;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((radioButton1.Checked || radioButton2.Checked) && textBox1.Text!=null)
            {
                if (radioButton1.Checked)
                {
                    User user = new User(Convert.ToString(textBox1.Text),"", true);
                    user.Set_limitation(0);
                    bool isExists = false;
                    foreach(User user1 in Main.Users)
                    {
                        if(user.Get_Username()==user1.Get_Username())
                            isExists = true;
                    }
                    if (!isExists)
                    {
                        user.GeneratePasskey(Main.Users);
                        Main.Users.Add(user);
                    }
                    else
                        MessageBox.Show("That username is used! Use other username.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    User user = new User(Convert.ToString(textBox1.Text),"", false);
                    user.Set_limitation(0);
                    bool isExists = false;
                    foreach (User user1 in Main.Users)
                    {
                        if (user.Get_Username() == user1.Get_Username())
                            isExists = true;
                    }
                    if (!isExists)
                    {
                        user.GeneratePasskey(Main.Users);
                        Main.Users.Add(user);
                    }
                    else
                        MessageBox.Show("That username is used! Use other username.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                this.Close();
            }
        }
    }
}
