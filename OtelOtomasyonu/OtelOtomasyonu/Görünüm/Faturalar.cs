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
    public partial class Faturalar : Form
    {

        VeriKatman veriKatman = new VeriKatman();
        public Faturalar()
        {
            InitializeComponent();
            this.GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                DataSet dataSet = veriKatman.doldur();
                this.dgvBooking.AutoGenerateColumns = false;
                this.dgvBooking.DataSource = dataSet.Tables["Rezervasyon"];
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void BillHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dgvBooking_DoubleClick(object sender, EventArgs e)
        {
            this.txtBId.Text = this.dgvBooking.CurrentRow.Cells["BId"].Value.ToString();
            this.txtRId.Text = this.dgvBooking.CurrentRow.Cells["RId"].Value.ToString();
            this.txtCName.Text = this.dgvBooking.CurrentRow.Cells["CName"].Value.ToString();
            this.txtPhone.Text = this.dgvBooking.CurrentRow.Cells["CPhone"].Value.ToString();
            this.dtpCheckIn.Text = this.dgvBooking.CurrentRow.Cells["CheckIn"].Value.ToString();
            this.dtpCheckOut.Text = this.dgvBooking.CurrentRow.Cells["CheckOut"].Value.ToString();
            this.txtAdvance.Text = this.dgvBooking.CurrentRow.Cells["Advance"].Value.ToString();
            this.txtRemaining.Text = this.dgvBooking.CurrentRow.Cells["Remaining"].Value.ToString();
            this.txtTotal.Text = this.dgvBooking.CurrentRow.Cells["Total"].Value.ToString();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double paraUstu = 0;
                string kalan = this.txtRemaining.Text;
                double K = Convert.ToDouble(kalan);
                string odenen = this.txtNPaid.Text;
                double O = Convert.ToDouble(odenen);
                paraUstu = O - K;
                txtReturn.Text = Convert.ToString(paraUstu);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int x = 100, y = 100; 

   
                int width = 80, height = 50;


                var header = new Font("Calibri", 21, FontStyle.Bold);
                int hdy = (int)header.GetHeight(e.Graphics);

                var fnt = new Font("Times new Roman", 14, FontStyle.Bold);
                int dy = (int)fnt.GetHeight(e.Graphics);


                string[] texts = {
                    "Hotel Dream",
                    "Fatura No: " + txtBId.Text,
                    "Oda No: " + txtRId.Text,
                    "Müşteri Adı: " + txtCName.Text,
                    "Telefon No: " + txtPhone.Text,
                    "Giriş Tarihi: " + dtpCheckIn.Text,
                    "Çıkış Tarihi: " + dtpCheckOut.Text,
                    "Toplam: " + txtTotal.Text,
                    "Ön Ödeme: " + txtAdvance.Text,
                    "Şu An Ödenen Miktar: " + txtNPaid.Text,
                    "İade Edilen Miktar: " + txtReturn.Text
                };

                foreach (string text in texts)
                {
                    e.Graphics.DrawString(text, fnt, Brushes.Black, new PointF(x, y));
                    y += dy;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Yazdırma sırasında bir hata oluştu: " + exc.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Oda Tablosunu Güncelle
                veriKatman.cıkısOdaGuncelle(txtRId.Text);
                MessageBox.Show("Oda Güncellendi");
                //Fatura tablosunu güncelle
                veriKatman.cıkısFaturaGuncelle(txtTotal.Text, txtBId.Text);
                MessageBox.Show("Fatura Güncellendi");

                this.Temizle();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void Temizle()
        {
            this.txtBId.Text = "";
            this.txtRId.Text = "";
            this.dtpCheckIn.Text = "";
            this.txtCName.Text = "";
            this.txtPhone.Text = "";
            this.txtTotal.Text = "";
            this.txtAdvance.Text = "";
            this.txtRemaining.Text = "";
            this.txtNPaid.Text = "";
            this.txtReturn.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GirisMenusus md = new GirisMenusus();
            md.Visible = true;
            this.Visible = false;
        }
    }
}
