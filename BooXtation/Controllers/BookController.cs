using AutoMapper;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using BookXtation.DAL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using BooXtation.Helpers;
using Microsoft.IdentityModel.Tokens;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.Generic;
using BooXtation.BLL.Repositories.Specifications;
using BooXtation.BLL.Repositories.Repository;

namespace BooXtation.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;


        public BookController(IBookRepository repository, IMapper mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Books = await _repository.GetAllTack24();
            return View(Books);
        }

        //GET: Book/Details/id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {

            if (!id.HasValue)
                return BadRequest();

            var movieDetail = await _repository.GetBookByIdAsync(id.Value);
            if (movieDetail == null) return NotFound("Not Found");

            return View(movieDetail);
        }

        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create()
        {
            var BookDropdownsData =await  _repository.GetBookDropdownsValues();

            ViewBag.Author = new SelectList(BookDropdownsData.Authors, "Author_ID", "Name");
            ViewBag.Category = new SelectList(BookDropdownsData.Categories, "Category_ID", "Name");
            ViewBag.Publisher = new SelectList(BookDropdownsData.Publishers, "Publisher_ID", "Name");

            return View();
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookVM)
        {
            if (bookVM.Cover_Image_File == null)
                return NoContent();
            if (ModelState.IsValid)
            {
                
                    bookVM.Cover_Image = await DocumentSettings.UploadFileAsync(bookVM.Cover_Image_File, "images");
                var mappedBook = _mapper.Map<BookViewModel, Book>(bookVM);

                try
                {
                    await _repository.AddAsync(mappedBook);

                }
                catch
                {
                    // If ModelState is not valid, redirect to the Error view
                    ViewBag.ErrorMessage = "There was an error saving the book. Please try again.";
                    return View("Error");
                }

                return RedirectToAction("ViewBooks","admin");
            }

            // If ModelState is not valid, redirect to the Error view
            ViewBag.ErrorMessage = "The model is not valid. Please check the input values and try again.";
            return View("Error");
        }



        [Authorize(Roles = "Admin,Editor")]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var BookDetails = await _repository.GetBookByIdAsync(id);
            if (BookDetails == null) return View("NotFound");

            //Book to BookViewModel
            var mappedBook = _mapper.Map<Book, BookViewModel>(BookDetails);
            var BookDropdownsData = await _repository.GetBookDropdownsValues();

            ViewBag.Author = new SelectList(BookDropdownsData.Authors, "Author_ID", "Name");
            ViewBag.Category = new SelectList(BookDropdownsData.Categories, "Category_ID", "Name");
            ViewBag.Publisher = new SelectList(BookDropdownsData.Publishers, "Publisher_ID", "Name");

            return View(mappedBook);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id ,BookViewModel bookVM)
        {
            bookVM.Book_ID = id;
            if (ModelState.IsValid)
            {
                if (bookVM.Cover_Image_File != null)
                    bookVM.Cover_Image = await DocumentSettings.UploadFileAsync(bookVM.Cover_Image_File, "images");
                
                var mappedBook = _mapper.Map<BookViewModel, Book>(bookVM);
                
                try
                {
                    // Call Update
                    await _repository.UpdateAsync(mappedBook);
                    return RedirectToAction("ViewBooks", "admin");
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                    ViewBag.ErrorMessage = "There was an error updating the book. Please try again.";
                    return View("BookError");
                }
            }


            // If ModelState is not valid, redirect to the Error view
            ViewBag.ErrorMessage = "The model is not valid. Please check the input values and try again.";
            return View("BookError");

        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var BookDetails = await _repository.GetBookByIdAsync(id);
            if (BookDetails == null) return View("NotFound");
            return View(BookDetails);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Delete(Book book)
        {
            var BookDetails = await _repository.GetBookByIdAsync(book.Book_ID);
            if (BookDetails == null) return View("NotFound");

            try
            {
                await _repository.DeleteAsync(BookDetails);
                return RedirectToAction("ViewBooks", "Admin");
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                ViewBag.ErrorMessage = "There was an error While Delete the book. Please try again.";
                return View("BookError");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> FilterBooks(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var Books = await _repository.FilterBooks(searchString);
                if (!Books.IsNullOrEmpty())
                {
                    return View("Index", Books); 
                }
                else
                {
                    ViewBag.ErrorMessage = "There is No Books Match";
                    return View("BookError");
                }
                
            }
            else
                return View("Index");

        }

        [HttpGet("shop")]
        public async Task<ActionResult<PaginationData<Book>>> GetAllBookWithSpecs([FromQuery] BookSpecParams bookSpecParams)
        {
            var BookDropdownsData = await _repository.GetBookDropdownsValues();

            ViewBag.Author = new SelectList(BookDropdownsData.Authors, "Author_ID", "Name");
            ViewBag.Category = new SelectList(BookDropdownsData.Categories, "Category_ID", "Name");
            ViewBag.Publisher = new SelectList(BookDropdownsData.Publishers, "Publisher_ID", "Name");
            ViewBag.MinPrice = await _repository.GetMinPriceAsync();
            ViewBag.MaxPrice = await _repository.GetMaxPriceAsync();


            var spec = new BooktWithSpecifications(bookSpecParams);
            var books = await _repository.GetALLWithSpecSync(spec);
            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<Book>>(books.ToList());

            var countSpec = new BookWithFilteractionForCountSpec(bookSpecParams);
            var count = await _repository.GetCountWithSpecAsync(countSpec);

            var result = new PaginationData<Book>
            {
                Data = data,
                PageIndex = bookSpecParams.PageIndex,
                PageSize = bookSpecParams.PageSize,
                Count = count
            };

            return View("Shop", result); // Return View instead of Ok for Razor View
        }


    }
}
