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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        VeriKatman VeriKatman = new VeriKatman();
        private void btnLogin_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    DataSet ds = new DataSet();
            //    ds = VeriKatman.giris(txtUserid.Text, txtPassword.Text);
            //    if (ds.Tables[0].Rows.Count == 1)
            //    {
            //        MessageBox.Show("Giriş Başarılı");
            //        if (ds.Tables[0].Rows[0][3].ToString().Equals("Owner"))
            //        {

            //        }
            //        else
            //        {
            //            GirisMenusus md = new GirisMenusus();
            //            md.Visible = true;
            //        }
            //        this.Visible = false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Giriş Reddedildi");
            //        this.lblWrong.Visible = true;
            //        this.txtUserid.Text = "";
            //        this.txtPassword.Text = "";
            //    }
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show("Hata: " + exc.Message);
            //}
            GirisMenusus md = new GirisMenusus();
            md.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtUserid.Text = "";
            this.txtPassword.Text = "";
        }
    }
}
