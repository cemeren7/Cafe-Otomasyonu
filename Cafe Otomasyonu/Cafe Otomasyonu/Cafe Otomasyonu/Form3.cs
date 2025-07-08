using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Cafe_Otomasyonu
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        OleDbConnection veri = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Cafe1.accdb");
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        Random rnd = new Random();
        string[] resimler = new string[4];

        private void Form3_Load(object sender, EventArgs e)
        {
            int resim = rnd.Next(0, 3);
            int derece = rnd.Next(-4, 31);
            resimler[0] = "gunesli.png";
            resimler[1] = "bulutlu.png";
            resimler[2] = "yagmur.png";
            resimler[3] = "karlı.png";

            if (derece >= -4 && derece <=10)
            {
                veri.Open();
                OleDbCommand sorgu4 = new OleDbCommand("Select * from Urunler Where KategoriAd in('Sıcak İçecekler','Sıcak Kahveler')", veri);
                OleDbDataReader sorguoku4 = sorgu4.ExecuteReader();
                dt4.Load(sorguoku4);
                dataGridView4.DataSource = dt4;
                veri.Close();
            }
            if (derece > 10 && derece <=20)
            {
                veri.Open();
                OleDbCommand sorgu5 = new OleDbCommand("Select * from Urunler Where KategoriAd in('Meşrubatlar','Soğuk İçecekler','Sıcak İçecekler')", veri);
                OleDbDataReader sorguoku5 = sorgu5.ExecuteReader();
                dt4.Load(sorguoku5);
                dataGridView4.DataSource = dt4;
                veri.Close();
            }
            if (derece > 20 && derece <=30)
            {
                veri.Open();
                OleDbCommand sorgu6 = new OleDbCommand("Select * from Urunler Where KategoriAd in('Meşrubatlar','Soğuk İçecekler','Soğuk Kahveler')", veri);
                OleDbDataReader sorguoku6 = sorgu6.ExecuteReader();
                dt4.Load(sorguoku6);
                dataGridView4.DataSource = dt4;
                veri.Close();
            }

            if (derece > 2)
            {
                pictureBox1.ImageLocation = resimler[resim];
                label1.Text = derece.ToString() + "°C";
            }                    
            if (derece<2)
            {
                pictureBox1.ImageLocation = resimler[3];
                label1.Text = derece.ToString() + "°C";
            }

           



            veri.Open();
            OleDbCommand sorgu = new OleDbCommand("Select * From Urunler where KategoriAd in('Meşrubatlar','Soğuk İçecekler','Sıcak İçecekler')",veri);
            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Load(sorguoku);
            dataGridView1.DataSource = dt;
            veri.Close();

            veri.Open();
            OleDbCommand sorgu2 = new OleDbCommand("Select * From Urunler where KategoriAd in('Soğuk Kahveler','Sıcak Kahveler')", veri);
            OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
            dt2.Load(sorguoku2);
            dataGridView2.DataSource = dt2;
            veri.Close();

            veri.Open();
            OleDbCommand sorgu3 = new OleDbCommand("Select * From Urunler where KategoriAd in('Tatlılar')", veri);
            OleDbDataReader sorguoku3 = sorgu3.ExecuteReader();
            dt3.Load(sorguoku3);
            dataGridView3.DataSource = dt3;
            veri.Close();



        }
    }
}
