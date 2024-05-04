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
    public partial class ExtraServis : Form
    {
        VeriKatman veriKatman = new VeriKatman();
        public ExtraServis()
        {
            InitializeComponent();
            this.GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                DataSet dataSet = veriKatman.doldur();
                this.dgvService.AutoGenerateColumns = false;
                this.dgvService.DataSource = dataSet.Tables["ExtraPaket"];
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtSName.Text != "" && txtSCost.Text != "")
            {
                try
                {
                    if (txtSId.Text != "")
                    {
                        //Güncelleme
                        veriKatman.extraPaketGuncelle(txtSName.Text, txtSCost.Text, txtSId.Text);
                        MessageBox.Show("Güncelleme Başarılı");
                    }
                    else
                    {
                        //Ekleme
                        veriKatman.extraPaketEkle(txtSName.Text, txtSCost.Text);
                        MessageBox.Show("Ekleme Başarılı");
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
                veriKatman.extraPaketSil(this.dgvService.CurrentRow.Cells["SId"].Value.ToString());
                this.GridDoldur();
                this.Temizle();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void dgvService_DoubleClick(object sender, EventArgs e)
        {
            this.txtSId.Text = this.dgvService.CurrentRow.Cells["SId"].Value.ToString();
            this.txtSName.Text = this.dgvService.CurrentRow.Cells["SName"].Value.ToString();
            this.txtSCost.Text = this.dgvService.CurrentRow.Cells["SPackageCost"].Value.ToString();
        }

        private void Temizle()
        {
            this.txtSName.Text = "";
            this.txtSCost.Text = "";
            this.txtSId.Text = "";
        }

        private void ExtraService_FormClosed(object sender, FormClosedEventArgs e)
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
