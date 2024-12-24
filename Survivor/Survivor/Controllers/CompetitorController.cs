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
              .Select(c => new CompetitorDto()      //CompetitorDto daki propertyler ile bilgileri doldur
              {
                  CategoryId = c.CategoryId,
                  CreatedDate = c.CreatedDate,
                  FirstName = c.FirstName,
                  LastName = c.LastName,
                  Id = c.Id,
                  ModifiedDate = c.ModifiedDate,
                  CategoryName = c.Category.Name
              })
               .ToListAsync();                      // Listele
            return Ok(competitors);                 // competitors u döndür
        }

        [HttpGet("/api/competitor/{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {

            var competitor = await _context.Competitors
                            .Where(c => c.Id == id)                 // belirlenen id deki competitoru getir
                            .Select(c => new CompetitorDto()        //CompetitorDto daki propertyler ile bilgileri doldur
                            {
                                Id = c.Id,
                                CategoryId = c.CategoryId,
                                CategoryName = c.Category.Name,
                                CreatedDate = c.CreatedDate,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                ModifiedDate = c.ModifiedDate
                            })
                            .ToListAsync();                         // Listele
            if (competitor == null)
            {
                return NotFound();
            }
            return Ok(competitor);                                   // competitors u döndür
        }
        [HttpGet("/api/competitors/categories/{categoryId}")]
        public async Task<ActionResult<CompetitorDto>> GetCompetitorsByCategoryId(int categoryId)   //categoryid ye göre getir
        {
            var competitor = await _context.Competitors             
                            .Where(c => c.CategoryId == categoryId)             //categoryid yi tanımla
                            .Select(c => new CompetitorDto()                    //CompetitorDto daki propertyler ile bilgileri doldur
                            {
                                CategoryId = c.CategoryId,
                                CreatedDate = c.CreatedDate,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Id = c.Id,
                                ModifiedDate = c.ModifiedDate,
                                CategoryName = c.Category.Name
                            })
                            .ToListAsync();                                     //listele

            if (competitor == null)
                return NotFound();

            return Ok(competitor);                                              //competitor listesini döndür

        }

        [HttpPost("/api/competitors/add-competitor")]
        public async Task<IActionResult> AddCompetitor(CompetitorDto competitor)   
        {
            // CompetitorDto ya göre yeni newcompetitor oluşturduk
            var newCompetitor = new Competitor      
            {

                FirstName = competitor.FirstName,
                LastName = competitor.LastName,
                CategoryId = competitor.CategoryId,
                CreatedDate = competitor.CreatedDate,
                ModifiedDate = competitor.ModifiedDate
            };
            _context.Competitors.Add(newCompetitor);        //_context.Competitors ekledik
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetbyId), new { id = newCompetitor.Id }, newCompetitor);
        }

        [HttpDelete("/api/competitor/delete/{id}")]      
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors.FirstOrDefaultAsync(c => c.Id == id);       // id ye göre getir
            if (competitor == null)
            {
                return NotFound();
            }
            _context.Competitors.Remove(competitor);                // _Context.categories den sil
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("/api/competitor/update/{id}")]
        public async Task<IActionResult> UpdateCompetitor(int id, CompetitorDto competitorDto)  
        {
            var competitor = await _context.Competitors.FirstOrDefaultAsync(c => c.Id == id);   // id ye göre kategori getir
            if (competitor == null)
            {
                return NotFound();
            }
            //categoryDto ya göre düzenleme yap
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



