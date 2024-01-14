using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models.RequestDTO
{
    public class PagingDTO
    {
        private int rowCount = 10;

        public int RowCount { get => rowCount; set => rowCount = Math.Min(10, value); }
        public int PageNo { get; set; } = 1;
    }
}
