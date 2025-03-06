using action_elle_apk.Data;
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetProduitsAssurance()
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
