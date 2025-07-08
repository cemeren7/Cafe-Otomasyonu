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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection veri = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Cafe1.accdb");
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        BindingSource bs = new BindingSource();
  

        string islem;
       
        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 cafefrm = new Form2();
            cafefrm.ShowDialog();

            textBox1.Text = cafefrm.perId.ToString();
        
            veri.Open();
            OleDbCommand sorgu = new OleDbCommand("select * from SSiparis order by Spno asc", veri);

            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Load(sorguoku);



            dataGridView1.DataSource = dt;
            veri.Close();

            veri.Open();
            OleDbCommand sorgu2 = new OleDbCommand("select * from Urunler", veri);
            OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
            dt2.Load(sorguoku2);


            dataGridView2.DataSource = dt2;
            veri.Close();

            bs.DataSource = dt;
            dataGridView1.DataSource = bs;

            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "UrunAdi";
            comboBox1.ValueMember = "UrunNo";

            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "UrunAdi";
            comboBox2.ValueMember = "UrunNo";


            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;

            btn_kaydet.Enabled = false;
            btn_İptal.Enabled = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.Rows[0].Selected = true;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            

            btn_hazır.Enabled = false;
            btn_teslim.Enabled = false;

         


        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (islem == "yeni")
            {
                veri.Open();
                OleDbCommand sorgu = 
               new OleDbCommand("Insert Into Sıparıs(Pno,MasaNo,Starıh,UrunNo,Mıktar,BırımFıyat,Durum) Values('" + textBox1.Text + "','" + textBox2.Text + "','"  
               + dateTimePicker1.Value.ToShortDateString() + "','" + comboBox1.SelectedValue + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')", veri);

                OleDbDataReader sorguoku = sorgu.ExecuteReader();
                dt.Load(sorguoku);
                dt.Clear();
                dataGridView1.DataSource = dt;
                veri.Close();


                veri.Open();
                OleDbCommand sorgu2 = new OleDbCommand("select * from SSiparis order by Spno desc", veri);

                OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
                dt.Load(sorguoku2);

                dataGridView1.DataSource = dt;
                veri.Close();

            }
            if (islem == "duzenle")
            {
                veri.Open();
                OleDbCommand sorgu = new OleDbCommand("update Sıparıs set Pno='" + textBox1.Text + "', MasaNo='" + textBox2.Text + "', Starıh='" +
                dateTimePicker1.Value.ToShortDateString() + "', UrunNo='" + comboBox1.SelectedValue + "', Mıktar='" + textBox5.Text + "', BırımFıyat='" + textBox6.Text + "', Durum='" + textBox7.Text + "' where Spno=" + textBox3.Text, veri);
                OleDbDataReader sorguoku = sorgu.ExecuteReader();
                dt.Load(sorguoku);
                dt.Clear();
                dataGridView1.DataSource = dt;
                veri.Close();

                veri.Open();
                OleDbCommand sorgu2 = new OleDbCommand("select * from SSiparis order by Spno asc", veri);

                OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
                dt.Load(sorguoku2);

                dataGridView1.DataSource = dt;
                veri.Close();
            }

                veri.Open();
                OleDbDataAdapter stok = new OleDbDataAdapter("Update Urunler Set StokMiktarı= StokMiktarı - " + textBox5.Text + " where UrunAdi='" + comboBox1.Text + "'", veri);
                stok.Fill(dt2);
                dt2.Clear();
                dataGridView2.DataSource = dt2;
                veri.Close();



                veri.Open();
                OleDbDataAdapter stok2 = new OleDbDataAdapter("select * from Urunler", veri);
                stok2.Fill(dt2);
                dataGridView2.DataSource = dt2;
                veri.Close();
                

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.ShowDialog();
        }

        private void btn_hazır_Click(object sender, EventArgs e)
        {
            veri.Open();

            OleDbCommand sorgu = new OleDbCommand("update Sıparıs set Durum='Hazır' where Spno=" + txt_sno.Text, veri);

            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Load(sorguoku);

            dataGridView1.DataSource = dt;
            veri.Close();

            veri.Open();
            OleDbCommand sorgu2 = new OleDbCommand("select * from Sıparıs order by Spno asc", veri);

            OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
            dt.Load(sorguoku2);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[8].Visible = false;
            veri.Close();


        }

        private void txt_sno_TextChanged(object sender, EventArgs e)
        {
            btn_hazır.Enabled = true;
            btn_teslim.Enabled = true;

            if (txt_sno.Text == "")
            {
                btn_hazır.Enabled = false;
                btn_teslim.Enabled = false;

            }

            veri.Open();
            OleDbCommand sorgu = new OleDbCommand("select * from SSiparis where Spno=" + "0" + txt_sno.Text, veri);

            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Clear();
            dt.Load(sorguoku);

            dataGridView1.DataSource = dt;

            veri.Close();

        }

        private void btn_teslim_Click(object sender, EventArgs e)
        {
            veri.Open();
            OleDbCommand sorgu = new OleDbCommand("update Sıparıs set Durum='Teslim Edildi' where Spno=" + txt_sno.Text, veri);

            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Load(sorguoku);
            
            dataGridView1.DataSource = dt;
            veri.Close();

            veri.Open();
            OleDbCommand sorgu2 = new OleDbCommand("select * from Sıparıs order by Spno asc", veri);

            OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
            dt.Load(sorguoku2);
          
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[8].Visible = false;
            veri.Close();
        }


        private void btn_yeni_Click(object sender, EventArgs e)
        {
            //textBox1.Enabled = true;
            textBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;


            btn_yeni.Enabled = false;
            btn_duzenle.Enabled = false;

            btn_kaydet.Enabled = true;
            btn_İptal.Enabled = true;

            //textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.Text = "";
            textBox5.Text = "0";
            textBox6.Text = "0";




            islem = "yeni";
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            //textBox1.Enabled = true;
            textBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;


            btn_yeni.Enabled = false;
            btn_duzenle.Enabled = false;

            btn_kaydet.Enabled = true;
            btn_İptal.Enabled = true;

            islem = "duzenle";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                //textBox1.Text = dataGridView1.SelectedRows[0].Cells["Pno"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["MasaNo"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells["Starıh"].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells["UrunAdi"].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells["Mıktar"].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells["BırımFıyat"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["Spno"].Value.ToString();

            }
            catch
            {


            }

        }

        private void btn_İptal_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox5.Text = "0";
            textBox6.Text = "0";

            btn_İptal.Enabled = false;
            btn_kaydet.Enabled = false;
            btn_duzenle.Enabled = true;
            btn_yeni.Enabled = true;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
         
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            DialogResult cevap; // Burada Kullanıcının Hangi Tuşa Basıldıgını Kontrol etmek İçin Kullanılır

            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz", "Emin Misin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (cevap == DialogResult.Yes)
            {
                veri.Open();
                OleDbCommand sorgu = new OleDbCommand("Delete * from Sıparıs where Spno=" + dataGridView1.SelectedRows[0].Cells["Spno"].Value.ToString(), veri);

                OleDbDataReader sorguoku = sorgu.ExecuteReader();
                dt.Load(sorguoku);
                dt.Clear();
                dataGridView1.DataSource = dt;
                veri.Close();

                veri.Open();
                OleDbCommand sorgu2 = new OleDbCommand("select * from Sıparıs order by Spno asc", veri);

                OleDbDataReader sorguoku2 = sorgu2.ExecuteReader();
                dt.Load(sorguoku2);

                dataGridView1.DataSource = dt;
                veri.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sınır;
            int sınırsayı;

            sınır = "0" + textBox1.Text;
            sınırsayı = Convert.ToInt32(sınır);

            if (sınırsayı > 10)
            {
                MessageBox.Show("Personel sayısını doğru giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Clear();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.ShowDialog();
            
        }

        private void btn_ilk_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void btn_geri_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void btn_ileri_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void btn_son_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
               lbl_tutar.Text=(Convert.ToDecimal(textBox5.Text) * (Convert.ToDecimal(textBox6.Text))).ToString();
            }
            catch
            {

            }
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
               lbl_tutar.Text =(Convert.ToDecimal(textBox5.Text) * (Convert.ToDecimal(textBox6.Text))).ToString();
            }
            catch
            {

            }
           
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // eger klavyeden basılan tuş backspace tuşu ise ve imleç text içindeki degerin en sagında ise bu degerin silinmesini e.hadled ile engeller
            try
            {
                if (e.KeyChar == (char)Keys.Back && textBox5.SelectionStart == 1)
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
           
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
              if (e.KeyChar == (char)Keys.Back && textBox5.SelectionStart == 0)
              {
                e.Handled = true;
              }
            }
            catch
            {

            }
            
        }

        private void btn_stokekle_Click(object sender, EventArgs e)
        {
            veri.Open();
            OleDbDataAdapter stok = new OleDbDataAdapter("Update Urunler Set StokMiktarı= StokMiktarı + " + txt_urunadet.Text + " where UrunAdi='" + comboBox2.Text + "'", veri);
            stok.Fill(dt2);
            dt2.Clear();
            dataGridView2.DataSource = dt2;
            veri.Close();



            veri.Open();
            OleDbDataAdapter stok2 = new OleDbDataAdapter("select * from Urunler", veri);
            stok2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            veri.Close();
        }

        private void btn_rapor_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.ShowDialog();
        }

        private void btn_tumtablo_Click(object sender, EventArgs e)
        {
            txt_sno.Clear();
            veri.Open();
            OleDbCommand sorgu = new OleDbCommand("select * from SSiparis", veri);

            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Load(sorguoku);
            dataGridView1.DataSource = dt;
            veri.Close();           
        }
    }
}
