using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabWork1.Classes;

namespace LabWork1.Classes
{
    public class FileToUse
    {
        private string Filename;
        private List<User> Users;
        public FileToUse(string Filename, List<User> users = null)
        {
            this.Filename = Filename; Users = users;
        }
        public void Save() { 
            StreamWriter streamWriter = new StreamWriter(this.Filename);
            foreach (User user in Users)
            {
                string Line=user.Get_Username()+" "+user.Get_Password()+" "+user.Get_IsAdmin().ToString()+" "+user.Get_Blocked().ToString()+" "+user.Get_limitation();
                streamWriter.WriteLine(Line);
            }
            streamWriter.Close();
        }
        public List<User> GetUsers() { return Users; }

        public void Replace()
        {
           File.Delete(Filename);
            Save();
        }
        
        public void Check_file_existance()
        {
            if(!File.Exists(Filename)) {
                StreamWriter streamWriter = new StreamWriter(this.Filename);
                User newUser=new User("admin","",true);
                string Line = newUser.Get_Username() + " " + newUser.Get_Password() + " " + newUser.Get_IsAdmin().ToString() + " " + newUser.Get_Blocked().ToString()+" 0";
                streamWriter.WriteLine(Line);
                streamWriter.Close();
                Users.Add(newUser);
            }
            else
            {
                StreamReader streamReader = new StreamReader(this.Filename);
                while(!streamReader.EndOfStream)
                {
                    string[] Converter = streamReader.ReadLine().Split(' ');
                    User newUser=new User(Converter[0], Converter[1], Convert.ToBoolean(Converter[2]), Convert.ToBoolean(Converter[3]));
                    newUser.Set_limitation(Convert.ToInt32(Converter[4]));
                    Users.Add(newUser);
                }
                streamReader.Close();
            }
        }
    }
}
