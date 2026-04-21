using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerpustakaanApi.Models;

namespace PerpustakaanApi.Controllers
{
    [ApiController]
    public class BookController : Controller
    {
        private string _constr;

        public BookController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
        }

        // GET semua buku - bebas akses
        [HttpGet("api/books")]
        public ActionResult<object> GetAllBooks()
        {
            BookContext context = new BookContext(_constr);
            List<Book> list = context.ListBook();
            return Ok(new { status = "success", data = list, meta = new { total = list.Count } });
        }

        // GET buku by id - bebas akses
        [HttpGet("api/books/{id}")]
        public ActionResult<object> GetBookById(int id)
        {
            BookContext context = new BookContext(_constr);
            Book book = context.GetBookById(id);
            if (book == null)
                return NotFound(new { status = "error", message = "Buku tidak ditemukan" });
            return Ok(new { status = "success", data = book });
        }

        // POST tambah buku - butuh JWT
        [HttpPost("api/books"), Authorize]
        public ActionResult<object> AddBook([FromBody] Book book)
        {
            if (string.IsNullOrEmpty(book.judul) || string.IsNullOrEmpty(book.penulis))
                return BadRequest(new { status = "error", message = "Judul dan penulis wajib diisi" });

            BookContext context = new BookContext(_constr);
            bool success = context.AddBook(book);
            if (!success)
                return StatusCode(500, new { status = "error", message = "Gagal menambahkan buku" });
            return StatusCode(201, new { status = "success", message = "Buku berhasil ditambahkan" });
        }

        // PUT update buku - butuh JWT
        [HttpPut("api/books/{id}"), Authorize]
        public ActionResult<object> UpdateBook(int id, [FromBody] Book book)
        {
            BookContext context = new BookContext(_constr);
            bool success = context.UpdateBook(id, book);
            if (!success)
                return NotFound(new { status = "error", message = "Buku tidak ditemukan" });
            return Ok(new { status = "success", message = "Buku berhasil diupdate" });
        }

        // DELETE buku - butuh JWT
        [HttpDelete("api/books/{id}"), Authorize]
        public ActionResult<object> DeleteBook(int id)
        {
            BookContext context = new BookContext(_constr);
            bool success = context.DeleteBook(id);
            if (!success)
                return NotFound(new { status = "error", message = "Buku tidak ditemukan" });
            return Ok(new { status = "success", message = "Buku berhasil dihapus" });
        }
    }
}