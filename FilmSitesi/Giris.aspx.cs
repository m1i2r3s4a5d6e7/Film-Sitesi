using FilmSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmSitesi
{
    public partial class Giris : System.Web.UI.Page
    {
        public string sonuc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string gelenkadi = Request.Form["kadi"];
                string gelensifre = Request.Form["sifre"];

                FilmContext ctx = new FilmContext();
                Kullanici k = (from x in ctx.Kullanicilar where x.KullaniciAdi == gelenkadi && x.Sifre == gelensifre select x).FirstOrDefault();

                if (k == null)
                {
                    //Şartlara uyan kişi yok
                    sonuc = "Giriş Başarısız. Tekrar Dene";
                }
                else
                {
                    //Giriş yaptı
                    Session["kadi"] = k.KullaniciAdi;
                    var id = ctx.Kullanicilar.Where(x => x.KullaniciAdi == k.KullaniciAdi).FirstOrDefault().KullaniciID;
                    Session["KID"] = id;

                    //4-Beni hatırla butonu
                    var hatirla = Request.Form["hatirla"];
                    //checkbox old. dan seçildiyse "on" gelir, diğer türlü "off" gelir.
                    if (hatirla == "on")
                    {
                        HttpCookie cerez = new HttpCookie("bizimcerez2");
                        cerez.Value = k.KullaniciAdi + "---" + id;
                        cerez.Expires = DateTime.Today.AddDays(5); //bu hatırlama 5 gün sonra bitecek.
                        Response.SetCookie(cerez); // cerezi kullanıcıya gönderdik.
                    }
                    else
                    {
                        HttpCookie cerez = new HttpCookie("bizimcerez2"); //aynı isimde bir cookie oluşturuyoruz.
                        cerez.Expires = DateTime.Today.AddDays(-1); //Silme işlemi yok. Tarih geçsin dedik.
                        Response.SetCookie(cerez);
                    }    

                    //3-anasafya yönlendir.
                    Response.Redirect("/");
                }
            }

        }
    }
}