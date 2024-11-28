using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using APIInterview.Models;
using APIInterview.DTO.Forum;

namespace APIInterview.Controllers.Forum
{
    [Route("api/[controller]")]
    [EnableCors("ALL")] // ±Ò¥Î CORS
    [ApiController]
    public class FTMembersController : ControllerBase
    {
        private readonly ReviewContext _context;

        public FTMembersController(ReviewContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DTMember>> GetTMember(int id)
        {
            var tMember = await _context.TMembers.FindAsync(id);

            if (tMember == null)
            {
                return NotFound();
            }

            return DTMember.FromEntity(tMember);
        }


    }
}
