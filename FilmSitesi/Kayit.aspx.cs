﻿using FilmSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmSitesi
{
    public partial class Kayit : System.Web.UI.Page
    {
        public string sonuc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                Kullanici k = new Kullanici();
                k.KullaniciAdi = Request.Form["kadi"];
                k.Email = Request.Form["email"];
                k.Sifre = Request.Form["sifre"];

                if (k.Sifre != Request.Form["sifretekrar"])
                {
                    sonuc = "Şifreler eşleşmiyor.";
                }

                //Kullanıcı daha önce kaydolduysa
                FilmContext ctx = new FilmContext();
                var sayi = ctx.Kullanicilar.Where(x => x.KullaniciAdi == k.KullaniciAdi).Count();

                if(sayi > 0)
                {
                    sonuc = "Bu kullanıcı adı daha önce kullanılmış";
                }

                //Hiç problem yoksa:
                if(string.IsNullOrEmpty(sonuc))
                {
                    //1-kullanıcı kaydesilsin.
                    ctx.Kullanicilar.Add(k);
                    ctx.SaveChanges();
                    //2-kullanıcı oturum açsın.
                    Session["kadi"] = k.KullaniciAdi;
                    var id = ctx.Kullanicilar.Where(x => x.KullaniciAdi == k.KullaniciAdi).FirstOrDefault().KullaniciID;
                    Session["KID"] = id;
                    //3-anasafya yönlendir.
                    Response.Redirect("/");
                }




            }

        }
    }
}