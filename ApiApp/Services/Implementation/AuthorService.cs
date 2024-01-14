using ApiApp.Models;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using ApiApp.Repositories.Interfaces;
using ApiApp.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Services.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper mapper;

        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            this.mapper = mapper;
        }

       

        public PagedResponse<AuthorReponseDTO> GetAll(IUrlHelper Url, string filterByName, PagingDTO paging)
        {
            var QueryAuthorValue = _authorRepository.GetAll();

            QueryAuthorValue = QueryAuthorValue.Where(m => m.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(filterByName))
            {                                            //
                QueryAuthorValue = QueryAuthorValue.Where(m => m.Name.Equals(filterByName));
            }



            var ResponseDTO = QueryAuthorValue.Select(m => mapper.Map<AuthorReponseDTO>(m));

            var pagedResponse = new PagedResponse<AuthorReponseDTO>(ResponseDTO, paging);

            if (pagedResponse.Paging.HasNextPage)
            {
                pagedResponse.Paging.NextPageUrl = Url.Link("GetAllUsers", new
                {
                    filterByName,
                    paging.RowCount,
                    PageNumber = paging.PageNo + 1
                });
            }

            if (pagedResponse.Paging.HasPreviousPage)
            {
                pagedResponse.Paging.PreviousPageUrl = Url.Link("GetAllUsers", new
                {
                    filterByName,
                    paging.RowCount,
                    PageNumber = paging.PageNo - 1
                });
            }

            return pagedResponse;
        }


        public AuthorReponseDTO GetById(int id, out string ErrorCode)
        {
            ErrorCode = "";
            var value = _authorRepository.GetById(id ,out ErrorCode);
            return value;

            #region
            //string ErrorCode = "";


            //if (!string.IsNullOrWhiteSpace(ErrorCode))
            //{
            //    error.LoadError(ErrorCode);
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}

            //string ErrorCode = "";
            //var result = AuthorSer.GetById(id, out ErrorCode);

            //if (!string.IsNullOrWhiteSpace(ErrorCode))
            //{
            //    error.LoadError(ErrorCode);
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}
            //return Ok(result);


            //ErrorCode = "";
            //var value = _authorRepository.GetById(id, out ErrorCode);

            // value=value.Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefault();

            //Author SelsctedAuthor = value.Authors.Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefault();

            //if (SelsctedAuthor == null)
            //{
            //    ErrorCode = "Auth001";
            //    return null;
            //}
            //return mapper.Map<AuthorReponseDTO>(SelsctedAuthor);
            #endregion
        }


        public Author AddAuthor(AuthorAddRequest author)
        {
            string ErrorCode = "";
            var value = _authorRepository.AddAuthor(author,out ErrorCode);

            return value;
        }


        public void UpdateAuthor(int Id, AuthorUpdateRequestDTO newAuthor, out string ErrorCode)
        {
            //var value = _authorRepository.UpdateAuthor(Id,newAuthor,out ErrorCode);

            ErrorCode = "";
            if (string.IsNullOrWhiteSpace(newAuthor.Name))
            {
                ErrorCode = "Auth003";
                return;
            }

           // var curent =_authorRepository.UpdateAuthor(Id,newAuthor/*,out ErrorCode*/);

            var CurentAuthor = _authorRepository.UpdateAuthor(Id, newAuthor, out ErrorCode);

           // var CurentAuthor = context.Authors.Where(c => c.Id == Id).SingleOrDefault();

            if (CurentAuthor == null)
            {
                ErrorCode = "Auth001";
                return;
            }
            // here i will update the old object so the syntax will be 
            mapper.Map(newAuthor, CurentAuthor);
            _authorRepository.Save();
        }

        public Author UpdateAuthor(int Id, AuthorUpdateRequestDTO newAuthor)
        {
            throw new NotImplementedException();
        }
    }
}
