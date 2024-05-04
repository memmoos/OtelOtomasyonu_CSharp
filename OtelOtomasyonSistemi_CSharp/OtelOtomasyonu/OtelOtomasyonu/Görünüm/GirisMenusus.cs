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

namespace OtelOtomasyonu
{
    public partial class GirisMenusus : Form
    {
        public GirisMenusus()
        {
            InitializeComponent();
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            OdaEkle ar = new OdaEkle();
            ar.Visible = true;
            this.Visible = false;
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            Rezervasyon rs = new Rezervasyon();
            rs.Visible = true;
            this.Visible = false;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Musteriler cd = new Musteriler();
            cd.Visible = true;
            this.Visible = false;
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            YemeMenu fm = new YemeMenu();
            fm.Visible = true;
            this.Visible = false;
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            ExtraServis es = new ExtraServis();
            es.Visible = true;
            this.Visible = false;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            Faturalar bh = new Faturalar();
            bh.Visible = true;
            this.Visible = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Giris lg = new Giris();
            lg.Visible = true;
            this.Visible = false;
        }

        private void ManagerDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
