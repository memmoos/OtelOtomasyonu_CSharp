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
using OtelOtomasyonu.Model;

namespace OtelOtomasyonu
{
    public partial class Musteriler : Form
    {
        VeriKatman veriKatman = new VeriKatman();
        public Musteriler()
        {
            InitializeComponent();
            this.GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                DataSet dataSet = veriKatman.doldur();
                this.dgvCustomer.AutoGenerateColumns = false;
                this.dgvCustomer.DataSource = dataSet.Tables["Rezervasyon"];
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtCName.Text != "" && txtPhone.Text != "" && txtAdd.Text != "" && txtNID.Text != "")
            {
                try
                {
                    veriKatman.musteriGuncelle(txtCName.Text, txtPhone.Text, txtAdd.Text, txtNID.Text, this.dgvCustomer.CurrentRow.Cells["CNID"].Value.ToString());
                    MessageBox.Show("Güncelleme Başarılı");
                    this.GridDoldur();
                    this.Temizle();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hata Güncellemede Hata Oluştu: " + exc.Message);
                }
            }
            else
            {
                MessageBox.Show("Kutular Boş Geçilemez");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Temizle();
        }

        private void dgvCustomer_DoubleClick(object sender, EventArgs e)
        {
            this.txtCName.Text = this.dgvCustomer.CurrentRow.Cells["CName"].Value.ToString();
            this.txtPhone.Text = this.dgvCustomer.CurrentRow.Cells["CPhone"].Value.ToString();
            this.txtAdd.Text = this.dgvCustomer.CurrentRow.Cells["CAdd"].Value.ToString();
            this.txtNID.Text = this.dgvCustomer.CurrentRow.Cells["CNID"].Value.ToString();
        }

        private void Temizle()
        {
            this.txtCName.Text = "";
            this.txtPhone.Text = "";
            this.txtAdd.Text = "";
            this.txtNID.Text = "";
        }

        private void CustomerDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GirisMenusus md = new GirisMenusus();
            md.Visible = true;
            this.Visible = false;
        }
    }
}
