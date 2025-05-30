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
using System.Security.Cryptography;

namespace ayseakulup
{
    public partial class KullaniciForm : Form
    {
        public KullaniciForm()
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

        private void KullaniciForm_Load(object sender, EventArgs e)
        {
            EtkinlikleriYukle();
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
    }

}