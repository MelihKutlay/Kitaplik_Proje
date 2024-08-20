using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace Kitaplık_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=MELIH\\MELIH;Initial Catalog=KitaplikProje;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("update tbl_kitaplar set kitapad=@p1,yazar=@p2,tur=@p3,sayfa=@p4,durum=@p5 where kitapid=@p6", baglanti);
            guncelle.Parameters.AddWithValue("@p1", txtad.Text);
            guncelle.Parameters.AddWithValue("@p2", txtyazar.Text);
            guncelle.Parameters.AddWithValue("@p3", cmbtur.Text);
            guncelle.Parameters.AddWithValue("@p4", txtsayfa.Text);
            guncelle.Parameters.AddWithValue("@p5", lbldurum.Text);
            guncelle.Parameters.AddWithValue("@p6", txtid.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KAYIT GÜNCELLENMİŞTİR", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult kayıtsil = MessageBox.Show("KAYDI SİLMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ ? ", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (kayıtsil == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand sil = new SqlCommand("delete from tbl_kitaplar where kitapid=@p1", baglanti);
                sil.Parameters.AddWithValue("@p1", txtid.Text);
                sil.ExecuteNonQuery();
                baglanti.Close();
                listele();
                MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE SİLİNMİŞTİR", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("KAYIT SİLME İŞLEMİ İPTAL EDİLDİ", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into tbl_kitaplar (kitapad,yazar,tur,sayfa,durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            ekle.Parameters.AddWithValue("@p1", txtad.Text);
            ekle.Parameters.AddWithValue("@p2", txtyazar.Text);
            ekle.Parameters.AddWithValue("@p3", cmbtur.Text);
            ekle.Parameters.AddWithValue("@p4", txtsayfa.Text);
            ekle.Parameters.AddWithValue("@p5", lbldurum.Text);
            ekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KİTAP EKLENMİŞTİR", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lbldurum.Text = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lbldurum.Text = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbtur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {

                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand bul = new SqlCommand("select * from tbl_kitaplar where kitapad=@p1 or  kitapad like '%" + textBox1.Text + "%'", baglanti);
            bul.Parameters.AddWithValue("@p1", textBox1.Text);
            DataTable dt2 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(bul);
            da.Fill(dt2);
            dataGridView1.DataSource = dt2;
            baglanti.Close();
        }
    }
}