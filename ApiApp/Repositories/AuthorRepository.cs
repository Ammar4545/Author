using ApiApp.Models;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using ApiApp.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMapper mapper;
        private readonly LibraryDbContext context;
        //private readonly ErrorHandeler error;

        public AuthorRepository(IMapper mapper, LibraryDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
            //this.error = error;

        }

        //public PagedResponse<AuthorReponseDTO> GetAll(IUrlHelper Url,string filterByName, PagingDTO paging)
        //{
        //    var QueryAuthorValue = context.Authors.AsQueryable();

        //    QueryAuthorValue = QueryAuthorValue.Where(m => m.IsDeleted == false);

        //    if (!string.IsNullOrWhiteSpace(filterByName))
        //    {                                            //
        //        QueryAuthorValue = QueryAuthorValue.Where(m => m.Name.Equals(filterByName));
        //    }

            

        //    var ResponseDTO = QueryAuthorValue.Select(m => mapper.Map<AuthorReponseDTO>(m));

        //    var pagedResponse = new PagedResponse<AuthorReponseDTO>(ResponseDTO, paging);

        //    if (pagedResponse.Paging.HasNextPage)
        //    {
        //        pagedResponse.Paging.NextPageUrl = Url.Link("GetAllUsers", new
        //        {
        //            filterByName,
        //            paging.RowCount,
        //            PageNumber = paging.PageNo + 1
        //        });
        //    }

        //    if (pagedResponse.Paging.HasPreviousPage)
        //    {
        //        pagedResponse.Paging.PreviousPageUrl = Url.Link("GetAllUsers", new
        //        {
        //            filterByName,
        //            paging.RowCount,
        //            PageNumber = paging.PageNo - 1
        //        });
        //    }

        //    return pagedResponse;
        //}

        public AuthorReponseDTO GetById(int id,out string ErrorCode)
        {
            ErrorCode = "";
            Author SelsctedAuthor = context.Authors.Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefault();
            if (SelsctedAuthor == null)
            {
                ErrorCode = "Auth001";
                return null;
            }
            return mapper.Map<AuthorReponseDTO>(SelsctedAuthor);
        }

        public Author AddAuthor(AuthorAddRequest author, out string ErrorCode)
        {
            ErrorCode = "";

            if (string.IsNullOrWhiteSpace(ErrorCode))
            {
                ErrorCode = "Auth001";
                return null;
            }

          
            
            var curentAuthor = mapper.Map<Author>(author);
            
            context.Authors.Add(curentAuthor);
            Save();
            return curentAuthor;
        }


        public Author UpdateAuthor(int Id, AuthorUpdateRequestDTO newAuthor/*, out string ErrorCode*/)
        {
            //ErrorCode = "";
            //if (string.IsNullOrWhiteSpace(newAuthor.Name))
            //{
            //    ErrorCode = "Auth003";               
            //    return ;
            //}

            var CurentAuthor = context.Authors.Where(c => c.Id == Id).SingleOrDefault();

            //if (CurentAuthor == null)
            //{
            //    ErrorCode = "Auth001";
            //    return ;
            //}

            // here i will update the old object so the syntax will be 
            mapper.Map(newAuthor, CurentAuthor);
            Save();
            return CurentAuthor;
        }




        public Author UpdatePartial(int Id, JsonPatchDocument AuthotPatch, out string ErrorCode)
        {
            ErrorCode = "";
            var CurentAuthor = context.Authors.Where(c => c.Id == Id).SingleOrDefault();
            if (CurentAuthor == null)
            {

                ErrorCode = "Auth001";
                return null ;
            }

            AuthotPatch.ApplyTo(CurentAuthor);
            return CurentAuthor;

            //if (TryValidateModel(CurentAuthor))
            //{
            //    return ValidationProblem();
            //}
        }

        public void DeleteAuthor(int Id, out string ErrorCode)
        {
            ErrorCode = "";
            var CurentAuthor = context.Authors.Where(c => c.Id == Id).SingleOrDefault();
            if (CurentAuthor == null)
            {
                ErrorCode = "Auth001";
                return ;
            }
            ////now we should check if the author have books
            //if (BookController.BookList.Any(m => m.AuthorId == Id))
            //{
            //    return BadRequest("this auhtor has dependances");
            //}
            CurentAuthor.IsDeleted = true;
            //Authors.Remove(CurentAuthor);
            Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public IQueryable<Author> GetAll()
        {
            return context.Authors;
        }

      
    }


}

