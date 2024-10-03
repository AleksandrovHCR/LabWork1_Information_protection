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
    public partial class UserSettings : Form
    {
        private User SetUser = null;
        private Main Main;
        public UserSettings(Main Main)
        {
            this.Main = Main;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponent();
            foreach(User usr in Main.Users)
            {
                comboBox1.Items.Add(usr.Get_Username());
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (User user in Main.Users)
            {
                if (user.Get_Username() == comboBox1.SelectedItem.ToString())
                {
                    SetUser = user; break;
                }
            }
            if(SetUser != null)
            {
                textBox1.Text = SetUser.Get_limitation().ToString();
                if (SetUser.Get_Blocked())
                {
                    radioButton2.Checked = true;
                }
                else
                    radioButton1.Checked = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int length = Int32.Parse(textBox1.Text);
                SetUser.Set_limitation(length);
            }
            catch { }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked && SetUser != null)
            {
                SetUser.Lock();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked && SetUser != null)
            {
                SetUser.Unlock();
            }
        }
    }
}
