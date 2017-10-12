using FilmSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmSitesi
{
    public partial class Detay : System.Web.UI.Page
    {
        public Film secilenFilm = new Film();
        public int oySayisi = 0;
        public double toplampuan = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Detay.aspx?ID=1, gelen adresden ID gelecek
            FilmContext ctx = new FilmContext();
            int gelenid = Convert.ToInt32(Request.QueryString["ID"]);
            //1. ihtimal liste: .ToList()
            //2. ihtimal tek satır: .FirstOrDefault()
            secilenFilm = ctx.Filmler.Where(x => x.FilmID == gelenid).FirstOrDefault();

            /**** OY İŞLEMLERİ ****/
            oySayisi = ctx.Oylar.Where(x => x.FilmID == gelenid).Count();
            if(oySayisi > 0) //verilen oy varsa toplam puan hesaplansın.
            {
                toplampuan = ctx.Oylar.Where(x => x.FilmID == gelenid).Sum(x => x.Puan); //o filme verilen puanların toplamı
            }

            toplampuan = toplampuan / oySayisi; //Puan ortalaması
        }
    }
}