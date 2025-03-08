using action_elle_apk.Data;
using action_elle_apk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace action_elle_apk.Controllers
{
    [Route("api/v1/produitsassurances")]
    [ApiController]
    public class ProduitAssuranceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProduitAssuranceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // On Récupère tous les produits d'assurance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduitAssurance>>> GetProduitsAssurance()
        {
            var produits = await _context.ProduitsAssurances
                .Select(p => new
                {
                    p.ProduitAssuranceId,
                    p.NomProduitAssurance
                })
                .ToListAsync();

            return Ok(produits);
        }
    }
}
