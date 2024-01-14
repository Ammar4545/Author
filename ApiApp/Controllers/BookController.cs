using ApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    //this controller is related to author controller
    //so u should change the route
    //          /related/its param/this controller
    [Route("api/Author/{Id}/Book")]
    
    public class BookController : ControllerBase
    {
        public static List<Book> BookList = new List<Book>() {
        new Book(){ BookId=1,BookName="Computer Information",PaperCount=100,PublishYear=2000,AuthorId=1},
        new Book(){ BookId=2,BookName="Database Introduction",PaperCount=150,PublishYear=2020,AuthorId=1},
        new Book(){ BookId=3,BookName="Data Structured",PaperCount=120,PublishYear=2005,AuthorId=1},
        new Book(){ BookId=4,BookName="Operating Systems",PaperCount=50,PublishYear=2008,AuthorId=3},
        new Book(){ BookId=5,BookName="Asp Net Core 3 API",PaperCount=70,PublishYear=2010,AuthorId=3},
        };

        [HttpGet]
        public IActionResult AllAuthorBook(int Id)
        {
            var bookOfAuthor = BookList.Where(m => m.AuthorId == Id);
            return Ok(bookOfAuthor);
        }

        [HttpGet("{BookId}")]
        public IActionResult GetBookById(int Id,int BookId)
        {
            var curBook = BookList.Where(m => m.AuthorId == Id && m.BookId == BookId).SingleOrDefault();
            if (curBook==null)
            {
                return NotFound();
            }
            return Ok(curBook);
        }

        [HttpPost]
        public IActionResult AddBook(int Id,Book NewBook)
        {
            //be sure that realated table author is exist

            //if(!AuthorController.Authors.Any(m => m.Id == Id))
            //{
            //    return NotFound("Author Is not found");
            //}

            //be sure that authorid in author == authorid sended with book obj
            if ( Id != NewBook.AuthorId)
            {
                return BadRequest("Invalid author Id");
            }
            //be sure that the book is not exists
            if (BookList.Any(m => m.AuthorId == Id && m.BookId == NewBook.BookId))
            {
                return Conflict("Book Is exists");
            }
            BookList.Add(NewBook);
            return CreatedAtAction(nameof(GetBookById), new { Id, BookId = NewBook.BookId }, NewBook);
        }

        [HttpDelete("{BookId}")]
        public IActionResult DeleteBook(int Id,int BookId)
        {
                //if (!AuthorController.Authors.Any(m => m.Id == Id))
                //{
                //    return NotFound("Author Is not found");
                //}

            var curBook = BookList.Where(m => m.AuthorId == Id && m.BookId == BookId).SingleOrDefault();
            //be sure that the book is not exists
            if (curBook==null)
            {
                return NotFound("Book is not found");

            }
            BookList.Remove(curBook);
            return NoContent();
        }
    }
}
