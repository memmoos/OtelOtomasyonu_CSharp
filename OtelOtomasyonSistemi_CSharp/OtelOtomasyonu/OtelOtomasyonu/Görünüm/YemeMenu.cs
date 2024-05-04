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
    public partial class YemeMenu : Form
    {
        VeriKatman veriKatmanı = new VeriKatman();

        public YemeMenu()
        {
            InitializeComponent();
            this.GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                DataSet dataSet = veriKatmanı.doldur();
                this.dgvFood.AutoGenerateColumns = false;
                this.dgvFood.DataSource = dataSet.Tables["YemekPaketi"];
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtFName.Text != "" && txtFCost.Text != "")
            {
                try
                {
                    if (txtFId.Text != "")
                    {
                        //Güncelle
                        veriKatmanı.yemekGuncelle(txtFName.Text, txtFCost.Text, txtFId.Text);
                        MessageBox.Show("Güncelleme Başarılı");
                    }
                    else
                    {
                        //Ekleme
                        veriKatmanı.yemekEkle(txtFName.Text, txtFCost.Text);
                        MessageBox.Show("Veri Ekleme Başarılı");
                    }

                    this.GridDoldur();
                    this.Temizle();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hata: " + exc.Message);
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                veriKatmanı.yemekSil(this.dgvFood.CurrentRow.Cells["FId"].Value.ToString());
                this.GridDoldur();
                this.Temizle();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void dgvFood_DoubleClick(object sender, EventArgs e)
        {
            this.txtFId.Text = this.dgvFood.CurrentRow.Cells["FId"].Value.ToString();
            this.txtFName.Text = this.dgvFood.CurrentRow.Cells["FName"].Value.ToString();
            this.txtFCost.Text = this.dgvFood.CurrentRow.Cells["FPackageCost"].Value.ToString();
        }

        private void Temizle()
        {
            this.txtFName.Text = "";
            this.txtFCost.Text = "";
        }

        private void FoodMenu_FormClosed(object sender, FormClosedEventArgs e)
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
