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
    public partial class Form4 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Cafe1.accdb");
        OleDbCommand Sorgu;
        OleDbDataReader SorguOku;
        string sorgu,işlem;

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        public Form4()
        {
            InitializeComponent();
        }

        private void Veri_Getir()
        {
            baglanti.Open();
            Sorgu = new OleDbCommand(sorgu, baglanti);
            SorguOku = Sorgu.ExecuteReader();
            dt.Clear();
            dt.Load(SorguOku);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            sorgu = "select * from Personeller";
            Veri_Getir();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            txt_pno.Enabled = false;
            textBox1.Enabled = false;
            txt_padres.Enabled = false;
            txt_pmail.Enabled = false;
            txt_tel.Enabled = false;

            btn_iptal.Enabled = false;
            btn_kaydet.Enabled = false;

            dataGridView1.ReadOnly = true;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            sorgu = "select * from Personeller where PersonelAd like '" + Filtre_yap.Text + "%'";
            Veri_Getir();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txt_pno.Text = dataGridView1.SelectedRows[0].Cells["PNo"].Value.ToString();
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["PersonelAd"].Value.ToString();
                txt_padres.Text = dataGridView1.SelectedRows[0].Cells["Adres"].Value.ToString();
                txt_pmail.Text = dataGridView1.SelectedRows[0].Cells["Mail"].Value.ToString();
                txt_tel.Text = dataGridView1.SelectedRows[0].Cells["Telefon"].Value.ToString();
            }
            catch
            {

            }
        }

        private void btn_yeni_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            txt_padres.Enabled = true;
            txt_pmail.Enabled = true;
            txt_pno.Enabled = true;
            txt_tel.Enabled = true;

            btn_kaydet.Enabled = true;
            btn_iptal.Enabled = true;

            btn_düzenle.Enabled = false;
            btn_yeni.Enabled = false;


            textBox1.Text = "";
            txt_padres.Text = "";
            txt_pmail.Text = "";
            txt_pno.Text = "";
            txt_tel.Text = "";

            işlem = "Yeni";
            
        }

        private void btn_iptal_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            txt_padres.Text = "";
            txt_pmail.Text = "";
            txt_pno.Text = "";
            txt_tel.Text = "";

            btn_kaydet.Enabled = false;
            btn_iptal.Enabled = false;

            btn_düzenle.Enabled = true;
            btn_yeni.Enabled = true;


        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (işlem=="Yeni")
            {
                sorgu = "insert into Personeller(PersonelAd,Adres,Mail,Telefon) values('" + textBox1.Text + "','" + txt_padres.Text + "','" + txt_pmail.Text + "','" + txt_tel.Text + "')";
                baglanti.Open();
                Sorgu = new OleDbCommand(sorgu, baglanti);
                Sorgu.ExecuteNonQuery();
                baglanti.Close();
                sorgu = "select * from Personeller";
                Veri_Getir();

            }
            if (işlem=="Düzenle")
            {
                baglanti.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("Update Personeller set PersonelAd='"+ textBox1.Text+"', Adres='"+txt_padres.Text+"', Mail='" +txt_pmail.Text+"', Telefon='"+txt_tel.Text+"' where Pno= "+txt_pno.Text, baglanti);
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();

                baglanti.Open();
                OleDbDataAdapter adp2 = new OleDbDataAdapter("select * from Personeller",baglanti);
                dt.Clear();
                adp2.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }


        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediginiz Eminmisiniz: ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (cevap == DialogResult.Yes)
            {
                baglanti.Open();
                sorgu = "delete * from Personeller where PersonelAd='" + dataGridView1.SelectedRows[0].Cells["PersonelAd"].Value.ToString() + "'";
                Sorgu = new OleDbCommand(sorgu, baglanti);
                Sorgu.ExecuteNonQuery();
                baglanti.Close();
            }

            sorgu = "select * from Personeller";
            Veri_Getir();
        }

        private void btn_düzenle_Click(object sender, EventArgs e)
        {

            textBox1.Enabled = true;
            txt_padres.Enabled = true;
            txt_pmail.Enabled = true;
            txt_pno.Enabled = false;
            txt_tel.Enabled = true;

            btn_yeni.Enabled = false;
            btn_düzenle.Enabled = false;

            btn_kaydet.Enabled = true;
            btn_iptal.Enabled = true;

            işlem = "Düzenle";
        }
    }
}
