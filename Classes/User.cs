using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace LabWork1.Classes
{
    public class User
    {
        private string Username;
        private string Password;
        private bool IsAdmin;
        private bool IsBlocked;
        private int PasswordLimitation;
        internal string PassKey; /// Добавочная фраза к паролю 

        public void GeneratePasskey(List<User> users)
        {
            Random random = new Random();
            bool IsClone = true;
            while (IsClone)
            {
                IsClone= false;
                string PC = null;
                int Len = random.Next(0, 10);
                for (int i = 0; i < Len; i++)
                {
                    char W = (char)random.Next(0, 255);
                    PC += W;
                }
                foreach (User user in users)
                {
                    if (user.PassKey == PC)
                        IsClone = true;
                }
                if (!IsClone)
                {
                    Random random1 = new Random();
                    int tmp=random1.Next(0, 10);
                    PassKey += tmp > 5 ? (char)1 : (char)2;
                    PassKey += PC;
                    if(tmp > 5)
                    {
                        Password = PassKey + Password;
                    }
                    else Password = Password + PassKey;
                    break;
                }
            }
        }
       

        public User() { }
        public User(string username, string password, bool isadmin, bool issealed, string PassKey)
        {
            Username = username; Password = password; IsAdmin = isadmin; IsBlocked = issealed; this.PassKey = PassKey;
        }
        public User(string username, string password, bool isadmin,bool issealed) { Username = username; Password = password; IsAdmin = isadmin;IsBlocked = issealed;        }
        public User(string username, string password, bool isadmin) { Username = username; Password = password; IsAdmin = isadmin; }
        public void SetPassword(string password) {
            if (PassKey[0]==1)
            Password = PassKey+password;
            else Password = password+PassKey;
        }
        public void Set_limitation(int Len) {  PasswordLimitation = Len; }
        public int Get_limitation() => PasswordLimitation;
        public string Get_Username() => Username;
        public void Lock() {  IsBlocked = true; }
        public void Unlock() {  IsBlocked = false; }
        public string Get_Password_1() => Password;
        public string Get_Password()
        {
            string DecryptedPassword = Password.Replace(PassKey, null);
            return DecryptedPassword;
        }
        public bool Get_IsAdmin() => IsAdmin;
        public bool Get_Blocked() => IsBlocked;
    }
}
