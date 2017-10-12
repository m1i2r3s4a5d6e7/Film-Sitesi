using FilmSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmSitesi
{
    public partial class OyIslem : System.Web.UI.Page
    {
        //Bu sayfanın ne yapacağı
        //1- ne işlem yapacak? oy ver / verdiği oyu al
        //2- hangi film (filmid)
        //3- kim (Session["KID"])

        //Birden fazla parametre gönderimi. Adres (link) de
        //OyIslem.aspx?FilmID=1&islem=oyver&puan=4

        public string mesaj = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            FilmContext ctx = new FilmContext();

            int FilmID = Convert.ToInt32(Request.QueryString["FilmID"]);
            string islem = Request.QueryString["islem"];
            int kim = (int)Session["KID"];
            int puanAl = Convert.ToInt32(Request.QueryString["puan"]);

            if (islem == "oyver")
            {
                //eğer üyelik şartı yoksa. Herkes oy verebilir.
                //Session["Oyverildi" + FilmID] ="evet";

                //kullanıcı giriş yaptıysa
                if (Session["kadi"] != null)
                {
                    var oyVerdiMi = ctx.Oylar.Where(x => x.FilmID == FilmID && x.KullaniciID == kim).FirstOrDefault();

                    if (oyVerdiMi == null)
                    {
                        //tblOy tablosuna bir satır verişi için.
                        Oy o1 = new Oy();
                        o1.FilmID = FilmID;
                        o1.KullaniciID = kim;
                        o1.Puan = puanAl;

                        ctx.Oylar.Add(o1);
                        ctx.SaveChanges();
                    }
                    else
                        mesaj += "daha önce oy vermişsiniz.";
                }
                else
                    mesaj += "giriş yapılmamış.";

            }
        }
    }
}