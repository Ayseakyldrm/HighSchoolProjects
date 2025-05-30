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

namespace ayseakulup
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }


        void EtkinlikleriYukle()
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\Desktop\udemy\ayseakulup\ayseakulup\Kulup.mdf;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Etkiinlik", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



        private void AdminForm_Load(object sender, EventArgs e)
        {
            EtkinlikleriYukle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\Desktop\udemy\ayseakulup\ayseakulup\Kulup.mdf;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Etkiinlik (Etkinlik_adi, Tarih, Yer, Aciklama) VALUES (@a, @t, @y, @ac)", baglanti);
            komut.Parameters.AddWithValue("@a", txtAd.Text);
            komut.Parameters.AddWithValue("@t", DateTime.Parse(txtTarih.Text));
            komut.Parameters.AddWithValue("@y", txtYer.Text);
            komut.Parameters.AddWithValue("@ac", txtAciklama.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etkinlik eklendi.");
            EtkinlikleriYukle();


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\Desktop\udemy\ayseakulup\ayseakulup\Kulup.mdf;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("DELETE FROM Etkiinlik WHERE Id=@id", baglanti);
                komut.Parameters.AddWithValue("@id", id);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Etkinlik silindi.");
                EtkinlikleriYukle();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\Desktop\udemy\ayseakulup\ayseakulup\Kulup.mdf;Integrated Security=True");
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("UPDATE Etkiinlik SET Etkinlik_adi=@a, Tarih=@t, Yer=@y, Aciklama=@ac WHERE Id=@id", baglanti);
                    komut.Parameters.AddWithValue("@a", txtAd.Text);

                    DateTime tarih;
                    if (DateTime.TryParse(txtTarih.Text, out tarih))
                    {
                        komut.Parameters.AddWithValue("@t", tarih);
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir tarih girin (örn. 28.05.2025)");
                        baglanti.Close();
                        return;
                    }

                    komut.Parameters.AddWithValue("@y", txtYer.Text);
                    komut.Parameters.AddWithValue("@ac", txtAciklama.Text);
                    komut.Parameters.AddWithValue("@id", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Etkinlik güncellendi.");
                    EtkinlikleriYukle();
                }
            }
        }
        



        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            string aranan = "%" + txtArama.Text + "%";

            using (SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\PC\\Desktop\\udemy\\ayseakulup\\ayseakulup\\Kulup.mdf;Integrated Security=True"))
            {
                string sql = "SELECT * FROM Etkiinlik WHERE Etkinlik_adi LIKE @p OR Yer LIKE @p OR Aciklama LIKE @p ORDER BY Tarih";
                SqlDataAdapter arama = new SqlDataAdapter(sql, baglanti);
                arama.SelectCommand.Parameters.AddWithValue("@p", aranan);

                DataTable dt = new DataTable();
                arama.Fill(dt);

                dataGridView1.DataSource = dt;
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // başlık satırı değilse
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtAd.Text = row.Cells["Etkinlik_adi"].Value.ToString();
                txtTarih.Text = Convert.ToDateTime(row.Cells["Tarih"].Value).ToString("dd.MM.yyyy");
                txtYer.Text = row.Cells["Yer"].Value.ToString();
                txtAciklama.Text = row.Cells["Aciklama"].Value.ToString();
            }
        }

    }
}

