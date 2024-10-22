using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using LabWork1.Classes;
using LabWork1.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Policy;


namespace LabWork1
{
    public partial class Main : Form
    {
        public List<User> Users = new List<User>();
        public string Hash="H0w_much_isop1um_cost_f01_p9op1e?";
        public bool Good=false;
        public User Current_User = null;
        public Main()
        {
            
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            Passkey passkey = new Passkey(this);
            passkey.Show();
            //FileToUse file = new FileToUse("DB.txt", Users);

            //file.Check_file_existance(Hash);

            //Users=file.GetUsers();
            this.Enabled = false;
            сменитьПарольToolStripMenuItem.Enabled = false;
            добавитьПользователяToolStripMenuItem.Enabled = false;
            вывестиВсехПользователейToolStripMenuItem.Enabled=false;
            настроитьПользователяToolStripMenuItem.Enabled = false;
            
        }
        public void INIT()
        { 
            this.Enabled = true;
            FileToUse file = new FileToUse("DB.txt", Users);
            Good = true;
            file.Check_file_existance(Hash);
           
            Users = file.GetUsers();
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Good)
                {
                    FileToUse fileToUse = new FileToUse("DB.txt", Users);
                    fileToUse.Encrypt_task(Hash);
                }
              // fileToUse.Decrypt_task(Hash);
            //    fileToUse.Decrypt(Hash);
               
            }
            catch { }
        }

      
        //private static void EncryptFile(string path, byte[] key)//Текст Аннунаков с Нибиру
        //{
        //    string tmpPath = Path.GetTempFileName();
        //    using (FileStream fsSrc = File.OpenRead(path))
        //    using (Aes aes = Aes.Create())
        //    using (FileStream fsDst = File.Create(tmpPath))
        //    {
        //        aes.Key = key;
        //        foreach (byte b in aes.IV)
        //        fsDst.WriteByte(b);
        //        using (CryptoStream cs = new CryptoStream(fsDst, aes.CreateEncryptor(), CryptoStreamMode.Write, true))
        //        {
        //            fsSrc.CopyTo(cs);
        //        }
        //    }
        //    File.Delete(path);
        //    File.Move(tmpPath, path);
        //}
        //private static void DecryptFile(string path, byte[] key)//Переводчик с древнеарийского
        //{
        //    string tmpPath = Path.GetTempFileName();
        //    using (FileStream fsSrc = File.OpenRead(path))
        //    {
        //        byte[] iv = new byte[16];

        //        fsSrc.Read(iv,0,0);
        //        using (Aes aes = Aes.Create())
        //        {
        //            aes.Key = key;
        //            aes.IV = iv;
        //            using (CryptoStream cs = new CryptoStream(fsSrc, aes.CreateDecryptor(), CryptoStreamMode.Read, true))
        //            using (FileStream fsDst = File.Create(tmpPath))
        //            {
        //                cs.CopyTo(fsDst);
        //            }
        //        }
        //    }
        //    File.Delete(path);
        //    File.Move(tmpPath, path);
        //}
    }
}
