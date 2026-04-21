using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerpustakaanApi.Models;

namespace PerpustakaanApi.Controllers
{
    [ApiController]
    public class BorrowingController : Controller
    {
        private string _constr;

        public BorrowingController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
        }

        // GET semua peminjaman - butuh JWT
        [HttpGet("api/borrowings"), Authorize]
        public ActionResult<object> GetAllBorrowings()
        {
            BorrowingContext context = new BorrowingContext(_constr);
            List<Borrowing> list = context.ListBorrowing();
            return Ok(new { status = "success", data = list, meta = new { total = list.Count } });
        }

        // GET peminjaman by id - butuh JWT
        [HttpGet("api/borrowings/{id}"), Authorize]
        public ActionResult<object> GetBorrowingById(int id)
        {
            BorrowingContext context = new BorrowingContext(_constr);
            Borrowing borrowing = context.GetBorrowingById(id);
            if (borrowing == null)
                return NotFound(new { status = "error", message = "Data peminjaman tidak ditemukan" });
            return Ok(new { status = "success", data = borrowing });
        }

        // POST pinjam buku - butuh JWT
        [HttpPost("api/borrowings"), Authorize]
        public ActionResult<object> AddBorrowing([FromBody] Borrowing borrowing)
        {
            if (borrowing.user_id == 0 || borrowing.book_id == 0)
                return BadRequest(new { status = "error", message = "user_id dan book_id wajib diisi" });

            BorrowingContext context = new BorrowingContext(_constr);
            bool success = context.AddBorrowing(borrowing);
            if (!success)
                return StatusCode(500, new { status = "error", message = "Gagal meminjam buku" });
            return StatusCode(201, new { status = "success", message = "Buku berhasil dipinjam" });
        }

        // PUT kembalikan buku - butuh JWT
        [HttpPut("api/borrowings/{id}/return"), Authorize]
        public ActionResult<object> ReturnBook(int id)
        {
            BorrowingContext context = new BorrowingContext(_constr);
            bool success = context.ReturnBook(id);
            if (!success)
                return NotFound(new { status = "error", message = "Data peminjaman tidak ditemukan" });
            return Ok(new { status = "success", message = "Buku berhasil dikembalikan" });
        }

        // DELETE riwayat peminjaman - butuh JWT
        [HttpDelete("api/borrowings/{id}"), Authorize]
        public ActionResult<object> DeleteBorrowing(int id)
        {
            BorrowingContext context = new BorrowingContext(_constr);
            bool success = context.DeleteBorrowing(id);
            if (!success)
                return NotFound(new { status = "error", message = "Data peminjaman tidak ditemukan" });
            return Ok(new { status = "success", message = "Riwayat peminjaman berhasil dihapus" });
        }
    }
}