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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Cafe_Otomasyonu
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        OleDbConnection veri = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Cafe1.accdb");
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        BindingSource bs = new BindingSource();
        public string batarih;
        public string bitarih;

        private void Form5_Load(object sender, EventArgs e)
        {
          

            veri.Open();
            OleDbCommand sorgu = new OleDbCommand("Select * From SSiparis", veri);
            OleDbDataReader sorguoku = sorgu.ExecuteReader();

            dt.Load(sorguoku);
            dataGridView1.DataSource = dt;
            veri.Close();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            batarih = dateTimePicker1.Value.ToShortDateString();
            bitarih = dateTimePicker2.Value.ToShortDateString();

            veri.Open();
            
            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM SSiparis WHERE Starıh between ? and ?", veri);
            sorgu.Parameters.AddWithValue("?", batarih);
            sorgu.Parameters.AddWithValue("?", bitarih);
            OleDbDataReader sorguoku = sorgu.ExecuteReader();
            dt.Clear();
            dt.Load(sorguoku);            
            dataGridView1.DataSource = dt;
            veri.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
               
                
                using (OleDbConnection veri = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Cafe1.accdb"))
                {
                    veri.Open();
                    string sorgu = "SELECT * FROM SSiparis WHERE Starıh BETWEEN ? AND ?";
                    using (OleDbCommand command = new OleDbCommand(sorgu, veri))
                    {
                        command.Parameters.AddWithValue("?", bitarih);
                        command.Parameters.AddWithValue("?", batarih);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Belirtilen tarih aralığında kayıt bulunamadı.");
                                return;
                            }

                           
                            string dosyaYolu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),txt_pdfisim.Text+".pdf");

                            using (Document document = new Document())
                            {
                                PdfWriter.GetInstance(document, new FileStream(dosyaYolu, FileMode.Create));
                                document.Open();

                                
                                document.Add(new Paragraph($"Filtrelenmiş Sipariş Listesi ({bitarih} - {batarih})\n\n"));

                                
                                PdfPTable tablo = new PdfPTable(reader.FieldCount);
                                for (int i = 0; i < reader.FieldCount; i++)
                                    tablo.AddCell(new Phrase(reader.GetName(i)));

                               
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                        tablo.AddCell(reader[i].ToString());
                                }

                                document.Add(tablo);
                            }

                            MessageBox.Show($"PDF başarıyla oluşturuldu! Belgeler klasörüne '{txt_pdfisim.Text}.pdf' olarak kaydedildi.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

        }
    }
}
