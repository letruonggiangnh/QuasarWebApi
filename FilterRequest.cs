namespace QuasarWebApi
{
    public class    FilterRequest
    {
        public string TenTinh {  get; set; }
        public string LoaiTram { get; set; }
        public string KhuVuc { get; set; }
        public string PhanCapTram { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
