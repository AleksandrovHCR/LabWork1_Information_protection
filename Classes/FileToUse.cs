using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LabWork1.Classes;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LabWork1.Classes
{
    public class FileToUse
    {
        private static readonly byte[] S = new byte[256] {
        41, 46, 67, 201, 162, 216, 124, 1, 61, 54, 84, 161, 236, 240, 6, 19,
        98, 167, 5, 243, 192, 199, 115, 140, 152, 147, 43, 217, 188, 76, 130, 202,
        30, 155, 87, 60, 253, 212, 224, 22, 103, 66, 111, 24, 138, 23, 229, 18,
        190, 78, 196, 214, 218, 158, 222, 73, 160, 251, 245, 142, 187, 47, 238, 122,
        169, 104, 121, 145, 21, 178, 7, 63, 148, 194, 16, 137, 11, 34, 95, 33,
        128, 127, 93, 154, 90, 144, 50, 39, 53, 62, 204, 231, 191, 247, 151, 3,
        255, 25, 48, 179, 72, 165, 181, 209, 215, 94, 146, 42, 172, 86, 170, 198,
        79, 184, 56, 210, 150, 164, 125, 182, 118, 252, 107, 226, 156, 116, 4, 241,
        69, 157, 112, 89, 100, 113, 135, 32, 134, 91, 207, 101, 230, 45, 168, 2,
        27, 96, 37, 173, 174, 176, 185, 246, 28, 70, 97, 105, 52, 64, 126, 15,
        85, 71, 163, 35, 221, 81, 175, 58, 195, 92, 249, 206, 186, 197, 234, 38,
        44, 83, 13, 110, 133, 40, 132, 9, 211, 223, 205, 244, 65, 129, 77, 82,
        106, 220, 55, 200, 108, 193, 171, 250, 36, 225, 123, 8, 12, 189, 177, 74,
        120, 136, 149, 139, 227, 99, 232, 109, 233, 203, 213, 254, 59, 0, 29, 57,
        242, 239, 183, 14, 102, 88, 208, 228, 166, 119, 114, 248, 235, 117, 75, 10,
        49, 68, 80, 180, 143, 237, 31, 26, 219, 153, 141, 51, 159, 17, 131, 20
    };

        public static byte[] ComputeHash(byte[] input)
        {
            int padding = 16 - (input.Length % 16);
            byte[] paddedInput = new byte[input.Length + padding];
            Array.Copy(input, 0, paddedInput, 0, input.Length);
            for (int i = input.Length; i < paddedInput.Length; i++)
            {
                paddedInput[i] = (byte)padding;
            }

            byte[] checksum = new byte[16];
            byte[] state = new byte[48];
            int L = 0;

            for (int i = 0; i < paddedInput.Length / 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    state[j + 16] = paddedInput[i * 16 + j];
                    state[j + 32] = (byte)(state[j + 16] ^ state[j]);
                }

                for (int j = 0; j < 18; j++)
                {
                    for (int k = 0; k < 48; k++)
                    {
                        L = state[k] ^= S[L];
                    }
                    L = (L + j) % 256;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                checksum[i] = (byte)(checksum[i] ^ state[i]);
            }

            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 48; j++)
                {
                    L = state[j] ^= S[L];
                }
                L = (L + i) % 256;
            }

            byte[] hash = new byte[16];
            Array.Copy(state, 0, hash, 0, 16);
            return hash;
        }


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
                string Line=user.Get_Username()+" "+user.Get_Password_1()+" "+user.Get_IsAdmin().ToString()+" "+user.Get_Blocked().ToString()+" "+user.Get_limitation()+" "+user.PassKey;
                streamWriter.WriteLine(Line);
            }
            streamWriter.Close();
        }
        public List<User> GetUsers() { return Users; }


        //public void Encrypt()//Working
        //{
        //    StreamReader streamReader = new StreamReader(Filename);
        //    string clearText = streamReader.ReadToEnd();
        //    string EncryptionKey = "abc123";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    StreamWriter streamWriter = new StreamWriter("Aryantext.txt");
        //    streamWriter.WriteLine(clearText);
        //    streamWriter.Close();
        //}
        //public void Decrypt()
        //{
        //    if (File.Exists("Aryantext.txt"))
        //    {
        //        StreamReader streamReader = new StreamReader("Aryantext.txt");
        //        string cipherText = streamReader.ReadToEnd();
        //        streamReader.Close();
        //        string EncryptionKey = "abc123";
        //        cipherText = cipherText.Replace(" ", "+");
        //        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //        using (Aes encryptor = Aes.Create())
        //        {
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //                {
        //                    cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                    cs.Close();
        //                }
        //                cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //            }
        //        }
        //        StreamWriter streamWriter = new StreamWriter("DBM.txt");
        //        streamWriter.WriteLine(cipherText);
        //        streamWriter.Close();
        //    }
        //}//Working


        public void Encrypt_task(string Hash)
        {
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            StreamReader streamReader = new StreamReader(Filename);
            string clearText = streamReader.ReadToEnd();
            streamReader.Close();
            byte[] data = UTF8Encoding.UTF8.GetBytes(clearText);

            // byte[] data = UTF8Encoding.UTF8.GetBytes(textBox1.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                // byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                byte[] keys = ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    textBox.Text = Convert.ToBase64String(result, 0, result.Length);
                }
            }
            StreamWriter SW = new StreamWriter(Filename);
            SW.WriteLine(textBox.Text);
            SW.Close();


            //TextBox textBox = new TextBox();
            //StreamReader streamReader = new StreamReader(Filename);
            //string clearText = streamReader.ReadToEnd();
            //streamReader.Close();
            //byte[] Data = Encoding.UTF8.GetBytes(clearText);
            //using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            //{
            //    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
            //    using(TripleDESCryptoServiceProvider TD = new TripleDESCryptoServiceProvider() { Key=keys,Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7})
            //    {
            //        ICryptoTransform transform = TD.CreateEncryptor();
            //        int t = Data.Length;
            //        byte[] results=transform.TransformFinalBlock(Data,0, Data.Length);
            //        textBox.Text= Convert.ToBase64String(results, 0, results.Length);
            //    }
            //}

            ////textBox.Text = clearText;
            //StreamWriter streamWriter = new StreamWriter("Aryantext.txt");
            //streamWriter.WriteLine(textBox.Text);
            //streamWriter.Close();
        }
        public void Decrypt_task(string Hash)
        {
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            StreamReader streamReader = new StreamReader("DB.txt");
            textBox.Text= streamReader.ReadToEnd();
            streamReader.Close();
            try
            {
                byte[] data = Convert.FromBase64String(textBox.Text);
                //using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                //{
                //byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                byte[] keys = ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    textBox.Text = UTF8Encoding.UTF8.GetString(result);
                }
                //}
                StreamWriter SW = new StreamWriter(Filename);
                SW.WriteLine(textBox.Text);
                SW.Close();
            }
            catch { }
            //StreamReader streamReader = new StreamReader("Aryantext.txt");
            //string EncryptedText = streamReader.ReadToEnd();
            //streamReader.Close();
            //TextBox textBox = new TextBox();
            //textBox.Text = EncryptedText;
            //byte[] Data = UTF8Encoding.UTF8.GetBytes(textBox.Text);
            //using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            //{
            //    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
            //    using (TripleDESCryptoServiceProvider TD = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            //    {
            //        ICryptoTransform transform = TD.CreateDecryptor();
            //        int t = Data.Length;
            //        byte[] results = transform.TransformFinalBlock(Data, 0, t);
            //        EncryptedText = null;
            //        foreach (byte b in results)
            //            EncryptedText += (char)b;
            //    }
            //}
            //StreamWriter streamWriter = new StreamWriter("DBM.txt");
            //streamWriter.WriteLine(EncryptedText);
            //streamWriter.Close();
        }




        public void Replace()
        {
           File.Delete(Filename);
            Save();
        }
        
        public void Check_file_existance(string Hash)
        {
            if (!File.Exists(Filename))
            {
                StreamWriter streamWriter = new StreamWriter(this.Filename);
                User newUser = new User("admin", "", true, false, (char)1 + "228");
                newUser.SetPassword(null);
                string Line = newUser.Get_Username() + " " + newUser.PassKey + newUser.Get_Password() + " " + newUser.Get_IsAdmin().ToString() + " " + newUser.Get_Blocked().ToString() + " " + newUser.Get_limitation() + " " + newUser.PassKey;
                streamWriter.WriteLine(Line);
                streamWriter.Close();
                Users.Add(newUser);
            }
            else
            {
                try
                {
                    Decrypt_task(Hash);
                }
                finally { 
                StreamReader streamReader = new StreamReader(this.Filename);
                while (!streamReader.EndOfStream)
                {
                    string[] Converter = streamReader.ReadLine().Split(' ');
                    if (Converter.Length == 6)
                    {
                        User newUser = new User(Converter[0], Converter[1], Convert.ToBoolean(Converter[2]), Convert.ToBoolean(Converter[3]), Converter[5]);
                        newUser.Set_limitation(Convert.ToInt32(Converter[4]));
                        Users.Add(newUser);
                    }
                }
                streamReader.Close();
             }
            }
        }
    }
}
