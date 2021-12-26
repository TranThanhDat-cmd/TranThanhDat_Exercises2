using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranThanhDat_Exercises2.Data;
using TranThanhDat_Exercises2.Data.Entities;
using TranThanhDat_Exercises2.Models;

namespace TranThanhDat_Exercises2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            var model = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return Ok(model);
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> Add(string name)
        {
            try
            {
                var category = new Category()
                {
                    Name = name
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                var model = _mapper.Map<CategoryViewModel>(category);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return BadRequest();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryViewModel.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}
