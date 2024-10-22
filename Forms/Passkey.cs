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
    public partial class Passkey : Form
    {
        Main Main;
        bool Good=false;
        public Passkey(Main main)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            Main = main;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == Main.Hash)
            {
                MessageBox.Show( "Правильно. Файл будет дешифрован!","Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                Main.INIT();
                this.Close();
            }
            else
            {
                MessageBox.Show( "Неправильно. Файл не будет дешифрован, а программа будет закрыта!","Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main.Close();
                //this.Close();
            }
            

        }

        private void Passkey_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Main.Good)
            {
                MessageBox.Show("Вы отказались от ввода КС. Файл не будет дешифрован, а программа будет закрыта!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main.Close();
            }
        }
    }
}
