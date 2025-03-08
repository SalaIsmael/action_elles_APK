using action_elle_apk.Data;
using action_elle_apk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace action_elle_apk.Controllers
{
    [Route("api/v1/subscriptions")]
    [ApiController]
    public class SouscriptionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SouscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }


        //On récupère une souscription par son id

        [HttpGet("{id}")]
        public async Task<ActionResult<Souscription>> GetSouscription(int id)
        {
            var souscription = await _context.Souscriptions
                .FindAsync(id);

            if (souscription == null)
                return NotFound("Souscription introuvable.");

            return souscription;
        }

        // On Crée une souscription
        [HttpPost]
        public async Task<ActionResult<Souscription>> CreateSouscription(Souscription souscription)
        {
            souscription.DateMiseEnService = DateTime.SpecifyKind(souscription.DateMiseEnService, DateTimeKind.Utc);
            _context.Souscriptions.Add(souscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSouscription), new { id = souscription.SouscriptionId }, souscription);
        }

        //On récupère le statut d'une souscription

        [HttpGet("status/{id}")]
        public async Task<ActionResult<string>> GetSouscriptionStatus(int id)
        {
            var souscription = await _context.Souscriptions.FindAsync(id);
            if (souscription == null)
                return NotFound("Souscription introuvable.");

            return Ok(new { Status = souscription.Status });
        }

    }
}
