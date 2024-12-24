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
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CompetitorController(SurvivorDbContext context)
        {
            _context = context;
        }


        [HttpGet("/api/competitors")]
        public async Task<IActionResult> Get()
        {
            var competitors = await _context.Competitors
              .Where(c => !c.IsDeleted)
              .Select(c => new CompetitorDto()
              {
                  CategoryId = c.CategoryId,
                  CreatedDate = c.CreatedDate,
                  FirstName = c.FirstName,
                  LastName = c.LastName,
                  Id = c.Id,
                  ModifiedDate = c.ModifiedDate,
                  CategoryName = c.Category.Name
              })
               .ToListAsync();
            return Ok(competitors);
        }



        [HttpGet("/api/competitor/{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {

            var competitor = await _context.Competitors
                            .Where(c => c.Id == id)
                            .Select(c => new CompetitorDto()
                            {
                                Id= c.Id,
                                CategoryId = c.CategoryId,
                                CategoryName = c.Category.Name,
                                CreatedDate = c.CreatedDate,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                ModifiedDate = c.ModifiedDate
                            })
                            .ToListAsync();
            if (competitor == null)
            {
                return NotFound();
            }
            return Ok(competitor);
        }




        [HttpGet("/api/competitors/categories/{categoryId}")]
        public async Task<ActionResult<CompetitorDto>> GetCompetitorsByCategoryId(int categoryId)
        {
            var competitor = await _context.Competitors
                            .Where(c => c.CategoryId == categoryId)
                            .Select(c => new CompetitorDto()
                            {
                                CategoryId = c.CategoryId,
                                CreatedDate = c.CreatedDate,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Id = c.Id,
                                ModifiedDate = c.ModifiedDate,
                                CategoryName = c.Category.Name
                            })
                            .ToListAsync();

            if (competitor == null)
                return NotFound();

            return Ok(competitor);

        }

        [HttpPost("/api/competitors/add-competitor")]
        public async Task<IActionResult> AddCompetitor(CompetitorDto competitor)
        {
            // Create a new Competitor object and map the data from the DTO
            var newCompetitor = new Competitor
            {

                FirstName = competitor.FirstName,
                LastName = competitor.LastName,
                CategoryId = competitor.CategoryId,
                CreatedDate = competitor.CreatedDate, // If CreatedDate is null, use the current UTC time
                ModifiedDate = competitor.ModifiedDate   // If ModifiedDate is null, use the current UTC time
            };


            // Add the new competitor to the context
            _context.Competitors.Add(newCompetitor);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created competitor with a status of 201 (Created)
            return CreatedAtAction(nameof(GetbyId), new { id = newCompetitor.Id }, newCompetitor);
        }


        [HttpDelete("/api/competitor/delete/{id}")]      // çalısıyor.
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors.FirstOrDefaultAsync(c => c.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }

            _context.Competitors.Remove(competitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("/api/competitor/update/{id}")]
        public async Task<IActionResult> UpdateCompetitor(int id, CompetitorDto competitorDto)
        {
            var competitor = await _context.Competitors.FirstOrDefaultAsync(c => c.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }

            competitor.FirstName = competitorDto.FirstName;
            competitor.LastName = competitorDto.LastName;
            competitor.CategoryId = competitorDto.CategoryId;
            competitor.ModifiedDate = competitorDto.ModifiedDate;
            competitor.CreatedDate = competitorDto.CreatedDate;

            if (competitorDto.CategoryId > 2 || competitorDto.CategoryId < 1)
            {
                return BadRequest("Category Id Hatalı");
            }

            await _context.SaveChangesAsync();
            return Ok(competitor);
        }
    }
}

    

