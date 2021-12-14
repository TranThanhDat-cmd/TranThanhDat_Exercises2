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
        private MyDbContext _context;
        private IMapper _mapper;

        public CategoryController(MyDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();
            var model = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return Ok(model);
        }

        [HttpPost("{name}")]
        public IActionResult Add(string name)
        {
            try
            {
                var category = new Category()
                {
                    Name = name
                };
                _context.Categories.Add(category);
                _context.SaveChanges();

                var model = _mapper.Map<CategoryViewModel>(category);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return BadRequest();
            }

            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }

            category.Name = categoryViewModel.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }


    
    }
}
