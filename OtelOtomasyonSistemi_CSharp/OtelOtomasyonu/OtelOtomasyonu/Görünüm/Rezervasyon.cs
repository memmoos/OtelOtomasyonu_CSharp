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
    public partial class Rezervasyon : Form
    {
        VeriKatman veriKatman = new VeriKatman();
        public Rezervasyon()
        {
            InitializeComponent();
            this.GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                DataSet dataSet = veriKatman.doldur();
                this.dgvRoom.AutoGenerateColumns = false;
                this.dgvRoom.DataSource = dataSet.Tables["Odalar"];

                this.dgvFood.AutoGenerateColumns = false;
                this.dgvFood.DataSource = dataSet.Tables["YemekPaketi"];

                this.dgvService.AutoGenerateColumns = false;
                this.dgvService.DataSource = dataSet.Tables["ExtraPaket"];

                DataTable rezervasyonTablosu = dataSet.Tables["Rezervasyon"];
                DataRow sonSatir = rezervasyonTablosu.Rows[rezervasyonTablosu.Rows.Count - 1];
                txtBId.Text = sonSatir.ItemArray[0].ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet dataSet = veriKatman.doldur();
            this.dgvRoom.AutoGenerateColumns = false;

            DataTable odaTablosu = dataSet.Tables["Odalar"];


            string kategori = txtAutoSearch.Text;


            DataRow[] filtrelenmişSatırlar = odaTablosu.Select("Category LIKE '%" + kategori + "%'");

            dgvRoom.DataSource = filtrelenmişSatırlar.Length > 0 ? filtrelenmişSatırlar.CopyToDataTable() : null;
        }

        private void Reservation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Fatura Ekle
                veriKatman.faturaEkle(txtRId.Text,dtpCheckIn.Text,dtpCheckOut.Text,txtFId.Text,txtSId.Text,txtCName.Text,txtAdd.Text,txtPhone.Text,txtNID.Text,txtTotal.Text,txtAdv.Text,txtRemaining.Text);
                MessageBox.Show("Fatura Ekleme Başarılı");
                this.GridDoldur();
                try
                {
                     //Oda Tablosu Güncelle
                    veriKatman.rezerOdaGuncelle(txtBId.Text,txtRId.Text);
                    MessageBox.Show("Oda Güncellendir");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hata: " + exc.Message);
                }
                this.Temizle();
                this.GridDoldur();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void btnTCalculate_Click(object sender, EventArgs e)
        {
            try
            {             
                string gun = this.txtDay.Text;

                var G = Convert.ToDouble(gun);
                DataSet dataSet = veriKatman.doldur();

                DataTable odaTablosu = dataSet.Tables["Odalar"];
                DataTable yemekTablosu = dataSet.Tables["YemekPaketi"];
                DataTable servisTablosu = dataSet.Tables["ExtraPaket"];

                DataRow[] odaSatır = odaTablosu.Select("RId = " + this.txtRId.Text);
                double odaMaliyet = Convert.ToDouble(odaSatır[0]["RoomCost"]);
                double toplam = odaMaliyet * G;


                DataRow[] yemekSatır = yemekTablosu.Select("FId = " + this.txtFId.Text);
                double yemekMaliyet = Convert.ToDouble(yemekSatır[0]["FPackageCost"]);
                toplam += yemekMaliyet * G;


                DataRow[] servisSatır = servisTablosu.Select("SId = " + this.txtSId.Text);
                double servisMaliyet = Convert.ToDouble(servisSatır[0]["SPackageCost"]);
                toplam += servisMaliyet;

                txtTotal.Text = toplam.ToString();

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }

        }

        private void btnRCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double kalan = 0;

                string toplam = this.txtTotal.Text;
                var T = Convert.ToDouble(toplam);

                string adv = this.txtAdv.Text;
                var A = Convert.ToDouble(adv);

                kalan = T - A;

                txtRemaining.Text = Convert.ToString(kalan);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void Temizle()
        {
            this.txtRId.Text = "";
            this.dtpCheckIn.Text = "";
            this.dtpCheckOut.Text = "";
            this.txtDay.Text = "";
            this.txtFId.Text = "";
            this.txtSId.Text = "";
            this.txtCName.Text = "";
            this.txtAdd.Text = "";
            this.txtPhone.Text = "";
            this.txtNID.Text = "";
            this.txtTotal.Text = "";
            this.txtAdv.Text = "";
            this.txtRemaining.Text = "";
            this.txtAutoSearch.Text = "";


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Temizle();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GirisMenusus md = new GirisMenusus();
            md.Visible = true;
            this.Visible = false;
        }
    }
}
