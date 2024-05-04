using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyonu.Model
{
    internal class VeritabaniYoneticisi
    {
        private string baglantiCumlesi = @"Data Source=DESKTOP-99F2M4E;Initial Catalog=Project_HMS;Integrated Security=True;";

        protected SqlConnection BaglantiAl()
        {
            return new SqlConnection(baglantiCumlesi);
        }

        protected SqlCommand KomutAl(string sorgu, SqlConnection baglanti)
        {
            return new SqlCommand(sorgu, baglanti);
        }

        protected SqlDataAdapter VeriAdaptoruAl(SqlCommand komut)
        {
            return new SqlDataAdapter(komut);
        }

        protected DataSet VeriKumesiAl(SqlDataAdapter adaptor)
        {
            DataSet veriKumesi = new DataSet();
            adaptor.Fill(veriKumesi);
            return veriKumesi;
        }

        protected DataTable VeriTablosuAl(SqlDataAdapter adaptor)
        {
            DataTable veriTablosu = new DataTable();
            adaptor.Fill(veriTablosu);
            return veriTablosu;
        }
    }
}
