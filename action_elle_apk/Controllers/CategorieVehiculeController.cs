using action_elle_apk.Data;
using action_elle_apk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace action_elle_apk.Controllers
{
    [Route("/api/v1/categorievehicules")]
    [ApiController]
    public class CategorieVehiculeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategorieVehiculeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // On Récupère toutes les categories de vehicule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategorieVehicule>>> GetCategorieVehicules()
        {
            var categoriesvehicules = await _context.CategoriesVehicules
                .Select(c => new
                {
                    c.CategorieVehiculeId,
                    c.Code,
                    c.Libelle,
                    c.Description
                })
                .ToListAsync();

            return Ok(categoriesvehicules);
        }
    }
}
