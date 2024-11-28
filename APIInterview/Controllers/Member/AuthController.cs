using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIInterview.Models;
using Microsoft.AspNetCore.Cors;
using APIInterview.DTO.Forum;
using APIInterview.DTO.Member;
namespace APIInterview.Controllers.Member
{
    [Route("api/[controller]")]
    [EnableCors("ALL")] // 啟用 CORS
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ReviewContext _context;

        public AuthController(ReviewContext context)
        {
            _context = context;
        }

        // GET: api/Auth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TMember>>> GetTMembers()
        {
            return await _context.TMembers.ToListAsync();
        }

        // GET: api/Auth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TMember>> GetTMember(int id)
        {
            var tMember = await _context.TMembers.FindAsync(id);

            if (tMember == null)
            {
                return NotFound();
            }

            return tMember;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] UserDto userloginDTO)
        {
            try
            {
                if (userloginDTO == null || string.IsNullOrEmpty(userloginDTO.FEmail) || 
                    string.IsNullOrEmpty(userloginDTO.FPassword))
                {
                    return BadRequest(new { message = "請提供完整的登入資訊" });
                }

                var user = await _context.TMembers
                    .FirstOrDefaultAsync(u => u.FEmail == userloginDTO.FEmail);

                if (user == null)
                {
                    return NotFound(new { message = "查無此帳號，請先註冊" });
                }

                if (user.FPassword != userloginDTO.FPassword)
                {
                    return Unauthorized(new { message = "輸入密碼不正確" });
                }

                return Ok(new { 
                    success = true,
                    message = "登入成功", 
                    memberId = user.FMemberId, 
                    memberName = user.FName 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"登入錯誤: {ex.Message}");
                return StatusCode(500, new { 
                    success = false,
                    message = "伺服器發生錯誤", 
                    error = ex.Message 
                });
            }
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTMember(int id, TMember tMember)
        {
            if (id != tMember.FMemberId)
            {
                return BadRequest();
            }

            _context.Entry(tMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TMember>> PostTMember(TMember tMember)
        {
            _context.TMembers.Add(tMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTMember", new { id = tMember.FMemberId }, tMember);
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTMember(int id)
        {
            var tMember = await _context.TMembers.FindAsync(id);
            if (tMember == null)
            {
                return NotFound();
            }

            _context.TMembers.Remove(tMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TMemberExists(int id)
        {
            return _context.TMembers.Any(e => e.FMemberId == id);
        }


        

    }
}
