using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranThanhDat_Exercises2.Data;
using TranThanhDat_Exercises2.Data.Entities;
using TranThanhDat_Exercises2.Models;

namespace TranThanhDat_Exercises2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMapper _mapper;
        private MyDbContext _context;

        public ProductController(IMapper mapper, MyDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(String id)
        {
            var produuct = _context.Products.SingleOrDefault(x=>x.Id.ToString() == id);
            if (produuct == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<ProductViewModel>(produuct);


            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            var listProductVM = _mapper.Map<List<Product>, List<ProductViewModel>>(products);
            return Ok(listProductVM);
        }

        [HttpGet("keySearch")]
        public IActionResult Search(string keySearch, double? from, double? to, string? sortBy)
        {
            var products = _context.Products.Where(x=> x.Name.Contains(keySearch));

            if (from.HasValue)
            {
                products = products.Where(x=>x.Price >= from);
            }
            if (to.HasValue)
            {
                products = products.Where(x => x.Price <= to);
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        products = products.OrderByDescending(x => x.Name);
                        break;
                    case "name_asc":
                        products = products.OrderBy(x => x.Name);
                        break;
                    case "price_desc":
                        products = products.OrderByDescending(x => x.Price);
                        break;
                    case "price_asc":
                        products = products.OrderBy(x => x.Price);
                        break;
                }

            }

            var model = _mapper.Map<List<Product>, List<ProductViewModel>>(products.ToList());
            return Ok(model);
        }



        [HttpPost]
        public IActionResult Add(ProductModel productModel)
        {
            try
            {
                var product = _mapper.Map<Product>(productModel);

                product.Id = Guid.NewGuid();
                _context.Products.Add(product);
                _context.SaveChanges();


                var model = _mapper.Map<ProductModel>(product);
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.ToString() == id);
            if (product == null)
            {
                return NotFound();

            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(String id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id.ToString())
            {
                return BadRequest();
            }

            var product = _context.Products.FirstOrDefault(x => x.Id.ToString() == id);
            if(product == null)
            {
                return NotFound();
            }

            _mapper.Map<ProductViewModel, Product>(productViewModel, product);
            _context.SaveChanges();
            return NoContent();
        }



        
       
    }
}
