using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Data.Odbc;

namespace OtelOtomasyonu.Model
{
    internal class VeriKatman:VeritabaniYoneticisi
    {
        public DataSet giris (string kullanıcıId,string sifre)
        {
            using (SqlConnection baglan =BaglantiAl())
            {
                baglan.Open ();
                using(SqlCommand cmd = KomutAl("select * from Login where UId = @ıd and Password = @sifre", baglan))
                {
                    cmd.Parameters.AddWithValue("@ıd", kullanıcıId.ToString());
                    cmd.Parameters.AddWithValue("@sifre",sifre.ToString());
                    using(SqlDataAdapter adapter = VeriAdaptoruAl(cmd))
                    {
                        DataSet dataSet = VeriKumesiAl(adapter);
                        return dataSet;
                    }
                }
            }
        }

        public DataSet doldur()
        {
            DataSet tumVeriTabanı = new DataSet();
            using(SqlConnection baglan = BaglantiAl())
            {
                baglan.Open();
                string sorgu1 = "select * from Room";
                string sorgu2 = "select * from Booking";
                string sorgu3 = "select * from FoodPackage";
                string sorgu4 = "select * from ServicePackage";
                using (SqlCommand cmd1 = KomutAl(sorgu1, baglan))
                {
                    using(SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1))
                    {
                        adapter1.Fill(tumVeriTabanı, "Odalar");
                    }
                }
                using (SqlCommand cmd2 = KomutAl(sorgu2, baglan))
                {
                    using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                    {
                        adapter2.Fill(tumVeriTabanı, "Rezervasyon");
                    }
                }
                using(SqlCommand cmd3 = KomutAl(sorgu3,baglan))
                {
                    using(SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3))
                    {
                        adapter3.Fill(tumVeriTabanı, "YemekPaketi");
                    }
                }
                using (SqlCommand cmd4 = KomutAl(sorgu4, baglan))
                {
                    using (SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4))
                    {
                        adapter4.Fill(tumVeriTabanı, "ExtraPaket");
                        return tumVeriTabanı;
                    }
                }
            }
        }

        public void odaGüncelle(string Id,string kategory,string rezervasyon ,string ucret)
        {
            using(SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update Room set Category = @kategory , IsBooked = @rezervasyon , RoomCost = @ucret where RId = @ıd;";
                bagla.Open();
                using(SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@ıd", Id);
                    cmd.Parameters.AddWithValue("@kategory", kategory);
                    cmd.Parameters.AddWithValue("@rezervasyon", rezervasyon);
                    cmd.Parameters.AddWithValue("@ucret", ucret); 
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public void odaEkle(string kategory, string rezervasyon, string ucret)
        {
            using(SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "insert into Room(Category,IsBooked,RoomCost,BId) values(@kategory,@rezervasyon,@ucret,NULL)";
                bagla.Open();
                using(SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@kategory", kategory);
                    cmd.Parameters.AddWithValue("@rezervasyon", rezervasyon);
                    cmd.Parameters.AddWithValue("@ucret", ucret);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void odaSil(string id)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "delete from Room where RId = @id";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void musteriGuncelle(string isim, string numara, string addres,string kimlik,string kimlikA)
        {
            using(SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update Booking set CName = @isim,CPhone = @numara,CAdd = @adres,CNID = @kimlik where CNID = @kimlikA";
                bagla.Open();
                using(SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@isim", isim);
                    cmd.Parameters.AddWithValue("@numara", numara);
                    cmd.Parameters.AddWithValue("@adres", addres);
                    cmd.Parameters.AddWithValue("@kimlik", kimlik);
                    cmd.Parameters.AddWithValue("@kimlikA", kimlikA);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void yemekGuncelle(string paketİsmi,string maliyet,string yemekId)
        {
            using(SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update FoodPackage set FName = @isim, FPackageCost = @maliyet where FId = @ıd";
                bagla.Open();
                using(SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@isim", paketİsmi);
                    cmd.Parameters.AddWithValue("@maliyet", maliyet);
                    cmd.Parameters.AddWithValue("@ıd", yemekId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void yemekEkle(string paketİsmi,string maliyet)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "insert into FoodPackage values(@isim,@maliyet);";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@isim", paketİsmi);
                    cmd.Parameters.AddWithValue("@maliyet", maliyet);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void yemekSil(string id)
        {
            using(SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "delete from FoodPackage where FId = @ıd";
                bagla.Open();
                using(SqlCommand cmd = KomutAl(sorgu,bagla))
                {
                    cmd.Parameters.AddWithValue("@ıd",id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void extraPaketGuncelle(string isim,string maliyet,string id)
        {
            using(SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update ServicePackage set SName = @isim, SPackageCost = @maliyet where SId = @id";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@isim", isim);
                    cmd.Parameters.AddWithValue("@maliyet", maliyet);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();  
                }
            }
        }
        public void extraPaketEkle(string isim, string maliyet)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "insert into ServicePackage values(@isim,@maliyet)";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@isim", isim);
                    cmd.Parameters.AddWithValue("@maliyet", maliyet);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void extraPaketSil(string id)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "delete from ServicePackage where SId = @id";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void faturaEkle(string odaNumar,string girisGun,string cikisGun,string yemekPaket,string servisPaket,string musterİsim,string addres,string numara,string kimlik,string toplam,string ilerleme,string kalan)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "insert into Booking values(@odaNo,@giris,@cıkıs,@yemekNo,@servisNo,@musteri,@adres,@numara,@kimlik,@toplam,@iler,@kalan)";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@odaNo", odaNumar);
                    cmd.Parameters.AddWithValue("@giris", girisGun);
                    cmd.Parameters.AddWithValue("@cıkıs", cikisGun);
                    cmd.Parameters.AddWithValue("@yemekNo", yemekPaket);
                    cmd.Parameters.AddWithValue("@servisNo", servisPaket);
                    cmd.Parameters.AddWithValue("@musteri", musterİsim);
                    cmd.Parameters.AddWithValue("@adres", addres);
                    cmd.Parameters.AddWithValue("@numara", numara);
                    cmd.Parameters.AddWithValue("@kimlik", kimlik);
                    cmd.Parameters.AddWithValue("@toplam", toplam);
                    cmd.Parameters.AddWithValue("@iler", ilerleme);
                    cmd.Parameters.AddWithValue("@kalan", kalan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void rezerOdaGuncelle(string faturaNo,string odaNo)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update Room set IsBooked = 'Yes' , BId = @faturaNo where RId = @odaNO";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@faturaNo", faturaNo);
                    cmd.Parameters.AddWithValue("@odaNO", odaNo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void cıkısOdaGuncelle(string odaNo)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update Room set IsBooked = 'No' where RId = @odaNO";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@odaNO", odaNo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void cıkısFaturaGuncelle(string odenen,string faturaNo)
        {
            using (SqlConnection bagla = BaglantiAl())
            {
                string sorgu = "update Booking SET Advance = @odenen, Remaining = " + 0 + " WHERE BId = @faturaNO";
                bagla.Open();
                using (SqlCommand cmd = KomutAl(sorgu, bagla))
                {
                    cmd.Parameters.AddWithValue("@odenen", odenen);
                    cmd.Parameters.AddWithValue("@faturaNO", faturaNo);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
