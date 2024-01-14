using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.hepler
{//this calss is to complete the whole data about paginf
    public class PagingDetails
    {
        public int TotalRow { get; set; }
        public int TotalPage { get; set; }
        public int CurPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }

    }
}
