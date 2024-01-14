using ApiApp.Models;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Services.Interfaces
{
   public interface IAuthorService 
    {
        PagedResponse<AuthorReponseDTO> GetAll(IUrlHelper Url, string filterByName, PagingDTO paging);
        AuthorReponseDTO GetById(int id, out string ErrorCode);
        Author AddAuthor(AuthorAddRequest author);

        Author UpdateAuthor(int Id, AuthorUpdateRequestDTO newAuthor/*, out string ErrorCode*/);
    }
}
