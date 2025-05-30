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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cmbKullanici_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKullanici.SelectedItem.ToString() == "Yönetici")
            {
                label2.Visible = true;
                txtSifre.Visible = true;
            }
            else
            {
                label2.Visible = false;
                txtSifre.Visible = false;
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string secilen = cmbKullanici.SelectedItem?.ToString();

            if (secilen == "Yönetici")
            {
                if (txtSifre.Text == "1234") // şifreyi burada belirliyorsun
                {
                    AdminForm admin = new AdminForm();
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Şifre hatalı.");
                }
            }
            else if (secilen == "Kullanıcı")
            {
                KullaniciForm kullanici = new KullaniciForm();
                kullanici.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin.");
            }
        }
    }
}
