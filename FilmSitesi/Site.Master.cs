using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmSitesi
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Kullanıcı yapmış gözükmüyorsa ama cookie si varsa
            if(Session["kadi"] == null)
            {
                if(Request.Cookies["bizimcerez2"] != null)
                {
                    //Giriş yapsın ve sayfa yenilensin.
                    var icerikCookie = Request.Cookies["bizimcerez2"].Value;
                    string[] alanlar = icerikCookie.Split(new string[] { "---" },StringSplitOptions.None);
                    Session["kadi"] = alanlar[0];
                    Session["KID"] = alanlar[1];
                }
            }
        }
    }
}