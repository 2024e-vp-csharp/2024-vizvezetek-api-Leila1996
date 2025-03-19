using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vizvezetek.API.Context;
using Vizvezetek.API.DTOs;
using Vizvezetek.API.Models;

namespace Vizvezetek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunkalapokController : ControllerBase
    {
        private readonly VizvezetekDbContext _context;

        public MunkalapokController(VizvezetekDbContext context)
        {
            _context = context;
        }

        // GET: api/munkalapok
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunkalapDto>>> GetMunkalapok()
        {
            // Lekérdezzük az adatokat, és átalakítjuk őket MunkalapDto típusra
            var munkalapok = await _context.Munkalapok
                .Include(m => m.hely) // Hely adatainak betöltése
                .Include(m => m.szerelo) // Szerelő adatainak betöltése
                .Select(m => new MunkalapDto(
                    m.id, // Az ID mező
                    m.beadas_datum, // Beadás dátuma
                    m.javitas_datum, // Javítás dátuma
                    $"{m.hely.telepules}, {m.hely.utca}", // Helyszín, település + utca
                    m.szerelo.nev, // Szerelő neve
                    m.munkaora, // Munkaórák száma
                    m.anyagar // Anyagár
                ))
                .ToListAsync(); // Az eredmény listára alakítása

            // Visszaadjuk a lekérdezett adatokat JSON formátumban
            return Ok(munkalapok);
        }


        // GET: api/munkalapok/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MunkalapDto>> GetMunkalap(int id)
        {
            var munkalap = await _context.Munkalapok
                .Include(m => m.hely)
                .Include(m => m.szerelo)
                .Where(m => m.id == id)
                .FirstOrDefaultAsync();

            if (munkalap == null)
            {
                return NotFound();
            }

            var munkalapDto = new MunkalapDto(
                munkalap.id,
                munkalap.beadas_datum,
                munkalap.javitas_datum,
                $"{munkalap.hely.telepules}, {munkalap.hely.utca}",
                munkalap.szerelo.nev,
                munkalap.munkaora,
                munkalap.anyagar
            );

            return Ok(munkalapDto);
        }

        [HttpPost("kereses")]
        public async Task<ActionResult<IEnumerable<MunkalapDto>>> SearchMunkalap(MunkalapKeresesDto munkalapKeresesDto)
        {
            var result = await _context.Munkalapok
                .Include(m => m.hely)
                .Include(m => m.szerelo)
                .Where(m => m.hely_id == munkalapKeresesDto.helyszin_id &&
                            m.szerelo_id == munkalapKeresesDto.szerelo_id)
                .ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            var selectResult = result.Select(m => new MunkalapDto(
                m.id,
                m.beadas_datum,
                m.javitas_datum,
                $"{m.hely.telepules}, {m.hely.utca}",
                m.szerelo.nev,
                m.munkaora,
                m.anyagar
            ));

            return Ok(selectResult);
        }

        [HttpGet("ev/{ev}")]
        public async Task<ActionResult<IEnumerable<MunkalapDto>>> GetByYear(int ev)
        {
            var result = await _context.Munkalapok
                .Where(m => m.javitas_datum.Year == ev)
                .Select(m => new MunkalapDto(
                    m.id,
                    m.beadas_datum,
                    m.javitas_datum,
                    $"{m.hely.telepules}, {m.hely.utca}",
                    m.szerelo.nev,
                    m.munkaora,
                    m.anyagar
                ))
                .ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
