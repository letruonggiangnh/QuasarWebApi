using System.Collections.Generic;

namespace QuasarWebApi
{
    public class ResponsePaging
    {
        public List<Tram> trams { get; set; }
        public int total { get; set; }
    }
}
