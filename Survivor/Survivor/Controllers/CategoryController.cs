using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Data.Migrations;
using Survivor.Dtos;
using Survivor.Model.Entities;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CategoryController(SurvivorDbContext context)
        {
            _context = context;
        }


        [HttpGet("/api/categories")]
        public async Task<IActionResult> Get()                  //bütün kategorileri listele
        {
            var categories = await _context.Categories              
                           // .Where(c => !c.IsDeleted)
                           .Select(c => new CategoryDto()           // CategoryDto daki yapıya uygun yeni liste oluştur
                           {
                               Id = c.Id,
                               ModifiedDate = c.ModifiedDate,
                               CreatedDate = c.CreatedDate,
                               Name = c.Name,
                           })
                            .ToListAsync();                         // oluşturulanları listele
            return Ok(categories);
        }

        [HttpGet("/api/categori/{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {

            var categories = await _context.Categories
                            .Where(c => c.Id == id)                      // id ye göre kategoriyi getir
                            .Select(c => new CategoryDto()              // CategoryDto daki yapıya uygun yeni liste oluştur
                            {
                                Id = c.Id,
                                ModifiedDate = c.ModifiedDate,
                                CreatedDate = c.CreatedDate,
                                Name = c.Name,
                            })
                            .ToListAsync();
            if (categories == null)                                   // kategori null ise notfound döndür
            {
                return NotFound();
            }
            return Ok(categories);                                      // oluşturulan kategoriyi getir
        }

        [HttpPost("/api/add-category")]
        public async Task<IActionResult> AddCompetitor(CategoryDto category)
        {
            // CategoryDto ya göre yeni category oluşturdk
            var newcategory = new Category
            {

                IsDeleted = category.IsDeleted,
                Name = category.Name,
                Id = category.Id,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate
            };

            _context.Categories.Add(newcategory);        // oluşturduğumuz newcategory yi _context.Categories e ekledik
            await _context.SaveChangesAsync();            //değişiklikleri kaydet
            return CreatedAtAction(nameof(GetbyId), new { id = newcategory.Id }, newcategory);
        }

        [HttpDelete("/api/category/delete/{id}")]     
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id); // id ye göre getir
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);     // _Context.categories den sil
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("/api/category/update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto) 
        {
            var updatedcategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);   // id ye göre kategori getir
            if (updatedcategory == null)
            { return NotFound(); }
            //categoryDto ya göre düzenleme yap
            updatedcategory.Name = categoryDto.Name;
            updatedcategory.IsDeleted = categoryDto.IsDeleted;
            updatedcategory.ModifiedDate = categoryDto.ModifiedDate;
            updatedcategory.CreatedDate = categoryDto.CreatedDate;
            updatedcategory.Id = categoryDto.Id;

            await _context.SaveChangesAsync();
            return Ok(updatedcategory);

        }

    }
}
    