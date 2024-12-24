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


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _context.Categories
                           // .Where(c => !c.IsDeleted)
                           .Select(c => new CategoryDto()
                           {
                               Id = c.Id,
                               ModifiedDate = c.ModifiedDate,
                               CreatedDate = c.CreatedDate,
                               Name = c.Name,
                           })
                            .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("/api/categori/{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {

            var categories = await _context.Categories
                            .Where(c => c.Id == id)
                            .Select(c => new CategoryDto()
                            {
                                Id = c.Id,
                                ModifiedDate = c.ModifiedDate,
                                CreatedDate = c.CreatedDate,
                                Name = c.Name,
                            })
                            .ToListAsync();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpPost("/api/create-category")]
        public async Task<IActionResult> AddCompetitor(CategoryDto category)
        {
            // Create a new Competitor object and map the data from the DTO
            var newcategory = new Category
            {

                IsDeleted = category.IsDeleted,
                Name = category.Name,
                Id = category.Id,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate
            };


            // Add the new competitor to the context
            _context.Categories.Add(newcategory);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created competitor with a status of 201 (Created)
            return CreatedAtAction(nameof(GetbyId), new { id = newcategory.Id }, newcategory);
        }

        [HttpDelete("{id:min(1)}")]      // çalısıyor.
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var updatedcategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (updatedcategory == null)
            { return NotFound(); }

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
    