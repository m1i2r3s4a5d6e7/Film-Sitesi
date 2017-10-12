using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSitesi.Models
{
    [Table("tblFilm")]
    public class Film
    {
        [Key]
        public int FilmID { get; set; }
        public string FilmAdi { get; set; }
        public string Ozet { get; set; }
        public string Yonetmen { get; set; }
        public string YoutubeURL { get; set; }
        public string Resim { get; set; }
    }
}
