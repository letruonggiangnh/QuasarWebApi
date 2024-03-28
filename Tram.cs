using System.ComponentModel.DataAnnotations;

namespace QuasarWebApi
{
    public class Tram
    {
        [Key]
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public string KhuVuc { get; set; }
        public string TenTinh { get; set; }
        public string LoaiTram { get; set; }
        public string PhanCapTram { get; set; }
        public double KinhDo { get; set; }
        public double ViDo { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu{ get; set; }
    }
}
