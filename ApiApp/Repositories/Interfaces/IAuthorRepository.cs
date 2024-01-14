using ApiApp.Models;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        IQueryable<Author> GetAll();
        AuthorReponseDTO GetById(int id,out string ErrorCode);
        Author AddAuthor(AuthorAddRequest author, out string ErrorCode);
        //void
        Author UpdateAuthor(int Id, AuthorUpdateRequestDTO newAuthor, out string ErrorCode);
        Author UpdatePartial(int Id, JsonPatchDocument AuthotPatch, out string ErrorCode);
        void DeleteAuthor(int Id, out string ErrorCode);
        int Save();
    }
}
