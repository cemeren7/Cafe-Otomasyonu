using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe_Otomasyonu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //string kullanıcıadı = "cafeadmin";
        string şifre = "4380";
        bool kontrol; // kullanıcı giriş yaptımı  kontrol edecegimiz degişken
        public int perId;

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kontrol) // Giriş kontrolü yapılır
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox2.Text==şifre && textBox1.Text.Trim() !=null && textBox1.Text.Trim() !="" 
               && Convert.ToInt32(textBox1.Text)<=10 && textBox2.Text.Trim() !=null && textBox2.Text.Trim() !="")
            {
                perId = Convert.ToInt32(textBox1.Text);
                kontrol = true;
                this.Close();               
            }
            else
            {
                MessageBox.Show("Girdiginiz Kullanıcı Adı veya Şifre Alanlarını Eksiksiz Ve Tam Giriniz Lütfen Tekrar Deneyin","Hata"
                ,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textBox1.Clear();
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "Yönetici Girişi";
        }
    }
}
