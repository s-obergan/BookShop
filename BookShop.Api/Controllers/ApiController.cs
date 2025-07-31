using BookShop.Data.DTOs;
using BookShop.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookShop.Api.Controllers
{
    /// <summary>
    /// Book shop API controller
    /// </summary>
    [ApiController]
    [Route("api/v1/")]
    public class ApiController : ControllerBase
    {
        //we could split ApiController into smaller parts like BooksController and OrdersController if necessary

        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="orderService"></param>
        public ApiController(IBookService bookService, IOrderService orderService)
        {
            _bookService = bookService;
            _orderService = orderService;
        }

        /// <summary>
        /// Get all available books
        /// </summary>
        /// <returns>A list of books</returns>
        [HttpGet("books")]
        [Tags("Books")]
        public IActionResult GetBooks()
        {
            try
            {
                return Ok(_bookService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request./n {ex.Message}");
            }
        }

        /// <summary>
        /// Get book by ID
        /// </summary>
        /// <param name="id">Book database ID</param>
        /// <returns>Book</returns>
        [HttpGet("book/{id}")]
        [Tags("Books")]
        public IActionResult GetBookById(int id)
        {
            return Ok(_bookService.GetById(id));
        }

        /// <summary>
        /// Adds a new book to database
        /// </summary>
        /// <param name="bookDto">New book object</param>
        /// <returns>Newly added book ID in database</returns>
        [HttpPost("books")]
        [Tags("Books")]
        public IActionResult AddBook([FromBody] BookDto bookDto)
        {
            try
            {
                int bookId =_bookService.Add(bookDto);
                return CreatedAtAction(nameof(AddBook), new { id = bookId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request./n {ex.Message}");
            }
        }

        /// <summary>
        /// Get books list filtered by title and/or date
        /// </summary>
        /// <param name="title">Book title</param>
        /// <param name="date">Book publication date (format yyyy-MM-dd)</param>
        /// <returns></returns>
        [HttpGet("books/search")]
        [Tags("Books")]
        public IActionResult SearchBooks([FromQuery] string? title, [FromQuery] string? date)
        {
            DateOnly? parsedDate = null;
            if (!String.IsNullOrWhiteSpace(date) && DateOnly.TryParse(date, out var dateValue))
            {
                parsedDate = dateValue;
            }

            var result = _bookService.Filter(title, parsedDate);
            return Ok(result);
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        /// <param name="id">Order database ID</param>
        /// <returns>Order</returns>
        [HttpGet("order/{id}")]
        [Tags("Orders")]
        public IActionResult GetOrder(int id)
        {
            return Ok(_orderService.GetById(id));
        }
        /// <summary>
        /// Adds a new order to database.
        /// </summary>
        /// <param name="orderDto">Order details</param>
        /// <returns>Order database ID</returns>
        [HttpPost("orders")]
        [Tags("Orders")]
        public IActionResult AddOrder([FromBody]OrderDto orderDto)
        {
            try
            {
                int orderId = _orderService.Add(orderDto);
                return CreatedAtAction(nameof(AddOrder), new { id = orderId});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request./n {ex.Message}");
            }
        }

        /// <summary>
        /// Get orders filtered by ID and/or date
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="date">Order date</param>
        /// <returns>Orders list</returns>
        [HttpGet("orders/search")]
        [Tags("Orders")]
        public IActionResult SearchOrders([FromQuery] int id, [FromQuery] string? date)
        {
            DateOnly? parsedDate = null;
            if (!String.IsNullOrWhiteSpace(date) && DateOnly.TryParse(date, out var dateValue))
            {
                parsedDate = dateValue;
            }

            var result = _orderService.Filter(id, parsedDate);
            return Ok(result);
        }
    }
}
