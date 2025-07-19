using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Personal_Finance_Tracker___Enterprise_Edition.Enumerations;
using Personal_Finance_Tracker___Enterprise_Edition.Models;
using System.Security.Claims;
using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Personal_Finance_Tracker___Enterprise_Edition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TransactionController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var transactions = await _context.Transactions
                .Where(t => !t.IsDeleted)
                .ToListAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await _context.Transactions
                .Where(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();
            return Ok(transaction);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            int result = await _context.SaveChangesAsync();
            return result>0? Ok("Transaction successfully created") :BadRequest("Error creating transaction");
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> Post(int id, [FromBody] Transaction transaction)
        {

            _context.Transactions.Update(transaction);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? Ok("Transaction successfully updated") : BadRequest("Error updating transaction");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRolesEnum.Admin))]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions
                .Where(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();
            if(transaction!=null)
                transaction.IsDeleted=true;
            int result = await _context.SaveChangesAsync();
            return result > 0 ? Ok("Transaction successfully removed") : BadRequest("Error removing transaction");
        }
    }
}
