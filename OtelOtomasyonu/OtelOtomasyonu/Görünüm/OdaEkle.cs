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
using System.Security;
using OtelOtomasyonu.Model;

namespace OtelOtomasyonu
{
    public partial class OdaEkle : Form
    {
        VeriKatman veriKatman = new VeriKatman();
        public OdaEkle()
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
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }
        private void AddRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dgvRoom_DoubleClick(object sender, EventArgs e)
        {
            this.txtRId.Text = this.dgvRoom.CurrentRow.Cells["RId"].Value.ToString();
            this.txtCategory.Text = this.dgvRoom.CurrentRow.Cells["Category"].Value.ToString();
            this.txtIsBooked.Text = this.dgvRoom.CurrentRow.Cells["IsBooked"].Value.ToString();
            this.txtRCost.Text = this.dgvRoom.CurrentRow.Cells["RoomCost"].Value.ToString();
        }

        private void Temizle()
        {
            this.txtRId.Text = "";
            this.txtCategory.Text = "";
            this.txtIsBooked.Text = "";
            this.txtRCost.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {  
                if (txtRId.Text != "")
                {
                    //Güncelle
                    veriKatman.odaGüncelle(txtRId.Text,txtCategory.Text,txtIsBooked.Text,txtRCost.Text);
                    MessageBox.Show("Güncelleme Başarılı");
                }
                else
                {
                    //Ekleme
                    veriKatman.odaEkle(txtCategory.Text,txtIsBooked.Text, txtRCost.Text);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Temizle();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = this.dgvRoom.CurrentRow.Cells["RId"].Value.ToString();
                veriKatman.odaSil(id);
                this.GridDoldur();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hata: " + exc.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GirisMenusus md = new GirisMenusus();
            md.Visible = true;
            this.Visible = false;
        }
    }
}
