using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnnonceAPI.Models;
using AnnonceAPI.Services;

namespace AnnonceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnoncesController : ControllerBase
    {
        private readonly AnnonceContext _context;
        private readonly IWeatherService _weatherService;

        public AnnoncesController(AnnonceContext context, IWeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        // POST: api/annonces
        [HttpPost]
        public async Task<ActionResult<Annonce>> PostAnnonce(Annonce annonce)
        {
            annonce.DateDeCreation = DateTime.UtcNow;
            annonce.Statut = "En attente de validation";
            _context.Annonces.Add(annonce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnonce", new { id = annonce.Id }, annonce);
        }

        // PUT: api/annonces/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnonce(int id, Annonce annonce)
        {
            if (id != annonce.Id)
            {
                return BadRequest();
            }

            _context.Entry(annonce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnonceExists(id))
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

        // GET: api/annonces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Annonce>> GetAnnonce(int id)
        {
            var annonce = await _context.Annonces.FindAsync(id);

            if (annonce == null)
            {
                return NotFound();
            }

            if (annonce.Statut != "Publiée")
            {
                return NotFound();
            }

            annonce.Weather = await _weatherService.GetLocalWeatherAsync(annonce.Localisation);

            return annonce;
        }

        private bool AnnonceExists(int id)
        {
            return _context.Annonces.Any(e => e.Id == id);
        }
    }
}
