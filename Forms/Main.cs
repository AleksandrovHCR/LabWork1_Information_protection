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
using LabWork1.Forms;

namespace LabWork1
{
    public partial class Main : Form
    {
        public List<User> Users = new List<User>();

        public User Current_User = null;
        public Main()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            FileToUse file = new FileToUse("DB.txt", Users);
            file.Check_file_existance();
            Users=file.GetUsers();
            сменитьПарольToolStripMenuItem.Enabled = false;
            добавитьПользователяToolStripMenuItem.Enabled = false;
            вывестиВсехПользователейToolStripMenuItem.Enabled=false;
            настроитьПользователяToolStripMenuItem.Enabled = false;
        }

        private void добавитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(this);
            addUser.ShowDialog();
            FileToUse file = new FileToUse("DB.txt", Users);
            file.Save();
        }
        private void If_passwordless()
        {
            if (Current_User.Get_Password() == "")
            {
                MessageBox.Show("Your account is lack of password! Change your password now.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ChangePassword CP = new ChangePassword(this);
                CP.ShowDialog();
                if (Current_User.Get_Password() == "")
                {
                    MessageBox.Show("You cannot use this application without password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    FileToUse file = new FileToUse("DB.txt", Users);
                    file.Replace();
                }
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            User temp=Current_User;
            Authorize authorize = new Authorize(this);
            authorize.ShowDialog();
            if (temp != Current_User)
            {
                if (Current_User.Get_IsAdmin())
                {
                    сменитьПарольToolStripMenuItem.Enabled = true;
                    добавитьПользователяToolStripMenuItem.Enabled = true;
                    вывестиВсехПользователейToolStripMenuItem.Enabled = true;
                    настроитьПользователяToolStripMenuItem.Enabled = true;
                    If_passwordless();
                    MessageBox.Show("Welcome, administrator!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }
                else
                {
                    сменитьПарольToolStripMenuItem.Enabled = true;
                    добавитьПользователяToolStripMenuItem.Enabled = false;
                    вывестиВсехПользователейToolStripMenuItem.Enabled = false;
                    настроитьПользователяToolStripMenuItem.Enabled = false;
                    If_passwordless();
                    MessageBox.Show("Welcome!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void сменитьПарольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword CP = new ChangePassword(this);
            CP.ShowDialog();
            FileToUse file = new FileToUse("DB.txt", Users);
            file.Replace();
        }

        private void вывестиВсехПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllUsers allUsers=new AllUsers(this);
            allUsers.ShowDialog();
        }

        private void настроитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserSettings US = new UserSettings(this);
            US.ShowDialog();
            FileToUse file = new FileToUse("DB.txt", Users);
            file.Replace();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}
