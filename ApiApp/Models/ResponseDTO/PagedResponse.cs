using ApiApp.hepler;
using ApiApp.Models.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models.ResponseDTO
{
    //this class is for changing the response data returned to user 
    public class PagedResponse<T>
    {
        public PagedResponse(IQueryable<T> Query,PagingDTO CLientPaging)
        {
            Paging = new PagingDetails();
            Paging.TotalRow = Query.Count();
            Paging.TotalPage = (int)Math.Ceiling((double)Paging.TotalRow / CLientPaging.RowCount);
            Paging.CurPage = CLientPaging.PageNo;
            Paging.HasNextPage = Paging.CurPage < Paging.TotalPage;
            Paging.HasPreviousPage = Paging.CurPage > 1;

            Data = Query.Skip((CLientPaging.PageNo - 1) *
                CLientPaging.RowCount).Take(CLientPaging.RowCount).ToList();
        }
        public PagingDetails Paging { get; set; }
        public List<T> Data { get; set; }
    }
}
