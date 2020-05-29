using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BackEndContext _context;

        public PostsController(BackEndContext context)
        {
            _context = context;
        }

        // GET: api/Posts/Postalcodes
        // Return string-List of PostalCodes
        [HttpGet("postalcodes")]
        public async Task<ActionResult<List<string>>> GetPostalCodes()
        {
            try
            {
                var posts = await _context.Post.Select(post => post.PostalCode).ToListAsync();

                if (null == posts) return NotFound();

                return posts;
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}