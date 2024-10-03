using System;
using System.Collections.Generic;
using System.Linq;
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

        public User() { }
        public User(string username, string password, bool isadmin,bool issealed) { Username = username; Password = password; IsAdmin = isadmin;IsBlocked = issealed; }
        public User(string username, string password, bool isadmin) { Username = username; Password = password; IsAdmin = isadmin; }
        public void SetPassword(string password) {  Password = password; }
        public void Set_limitation(int Len) {  PasswordLimitation = Len; }
        public int Get_limitation() => PasswordLimitation;
        public string Get_Username() => Username;
        public void Lock() {  IsBlocked = true; }
        public void Unlock() {  IsBlocked = false; }
        public string Get_Password() => Password;
        public bool Get_IsAdmin() => IsAdmin;
        public bool Get_Blocked() => IsBlocked;
    }
}
