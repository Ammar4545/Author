using ApiApp.hepler;
using ApiApp.Models;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using ApiApp.Repositories.Interfaces;
using ApiApp.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{

    [Route("api/Author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper mapper;

        //private readonly LibraryDbContext context;
        private readonly ErrorHandeler  error;
        private readonly IAuthorService AuthorSer;

        public AuthorController(IMapper mapper,ErrorHandeler error, IAuthorService AuthorRepo)
        {
            this.mapper = mapper;
            //this.context = context;
            this.error = error;
            this.AuthorSer = AuthorRepo;

        }


        [HttpGet(Name ="GetAllUsers")]
        public IActionResult GetAll([FromQuery]string filterByName,[FromQuery]PagingDTO paging  )
        {

            var Result = AuthorSer.GetAll(Url, filterByName, paging);
            return Ok(Result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            string ErrorCode = "";
            var result = AuthorSer.GetById(id,out ErrorCode);
            return Ok(result);



            //string ErrorCode = "";
            //var result = AuthorSer.GetById(id, out ErrorCode);

            //if (!string.IsNullOrWhiteSpace(ErrorCode))
            //{
            //    error.LoadError(ErrorCode);
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}
            //return Ok(result);

        }

        [HttpPost]
        // if u comment this ( [ApiController] ) u must Right [FromBody] as data sended empty
        public IActionResult AddAuthor(AuthorAddRequest author)
        {
           // string ErrorCode = "";
            var result = AuthorSer.AddAuthor(author);

            #region

            //if (context.Authors.Any(m => m.Name.Equals(author.Name)))
            //{
            //    error.LoadError("Auth002");
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}

            ////if the name hava spaces or empty
            //if (string.IsNullOrWhiteSpace(author.Name))
            //{
            //    return BadRequest(new { ErrorMessage = "Invalid Name" });
            //}
            ////var curAuthor = Authors.Where(b => b.Id == author.Id & & b.IsDeleted==false).SingleOrDefault();
            ////if (curAuthor != null)
            ////{
            ////    return Conflict(new { ErrorMessage = "Duplication in id author id" });
            ////}
            ////this is the sended model (Domain model) => i should make it 'response model'

            ////this regin will be mapped with auto mapper
            //#region
            ////Author curentAuthor = new Author()
            ////{

            ////    Name = author.Name,
            ////    Location = author.Location,
            ////    IsDeleted = false,
            ////    Books = new List<Book>(),
            ////    Id = Authors.Max(m => m.Id) + 1

            ////};
            //#endregion

            ////mapping
            ////this mapping is from creating new obj so the syntax will be like this
            // var curentAuthor =mapper.Map<Author>(author);
            ////curentAuthor.Id = context.Authors.Max(m => m.Id) + 1;
            //context.Authors.Add(curentAuthor);
            //context.SaveChanges();
            #endregion
            //if (!string.IsNullOrEmpty(ErrorCode))
            //{
            //    error.LoadError(ErrorCode);
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, MapAuthorToResponse(result));
        }




        [HttpPut("{Id}")]
        // here u shoud update all object
        // [ApiController] is insted of ([FromBody] Author newAuthor)

        public IActionResult UpdateAuthor(int Id, AuthorUpdateRequestDTO newAuthor)
        {
            #region 
            //if (string.IsNullOrWhiteSpace(newAuthor.Name))
            //{
            //    error.LoadError("Auth003");
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}

            //var CurentAuthor = context.Authors.Where(c => c.Id ==Id).SingleOrDefault();

            //if (CurentAuthor == null)
            //{
            //    error.LoadError("Auth001");
            //    ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            //    return ValidationProblem();
            //}
            //// here i will update the old object so the syntax will be 
            //mapper.Map(newAuthor, CurentAuthor);
            //context.SaveChanges();
            #endregion
            string ErrorCode = "";
            AuthorSer.UpdateAuthor(Id, newAuthor/*, out ErrorCode*/);
            if (!string.IsNullOrEmpty(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            return NoContent();
        }

        [HttpPatch("{Id}")]
        //here u will send the id with url
        //then frombody u will send array of json 
        public IActionResult UpdateAuthorPartial(int Id, JsonPatchDocument AuthotPatch)
        {
            string ErrorCode = "";
            var CurentAuthor = AuthorRepo.UpdatePartial(Id, AuthotPatch, out ErrorCode);


            AuthotPatch.ApplyTo(CurentAuthor);

            if (TryValidateModel(CurentAuthor))
            {
                return ValidationProblem();
            }
            AuthorRepo.Save();
            return NoContent();
        }

        //[HttpDelete("{Id}")]
        //public IActionResult DeleteAuthor(int Id)
        //{
        //    string ErrorCode = "";
        //    AuthorRepo.DeleteAuthor(Id, out ErrorCode);
        //    if (!string.IsNullOrEmpty(ErrorCode))
        //    {
        //        error.LoadError(ErrorCode);
        //        ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
        //        return ValidationProblem();
        //    }

        //    return NoContent();
        //}
        private AuthorReponseDTO MapAuthorToResponse(Author a)
        {
            return new AuthorReponseDTO()
            {
                Id = a.Id,
                Name = a.Name,
                Location = a.Location,
                BookCount = (a.Books == null) ? 0 : a.Books.Count()
            };
        }
    }

}
