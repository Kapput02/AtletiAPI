using AtletiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AtletiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalitiesController : ControllerBase
    {
        // GET: api/<NationalitiesController>
        [HttpGet]
        [Route("")]
        public ActionResult Get()
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                List<Nationality> candidati = model.Nationalities.ToList();
                if (candidati == null) return NotFound();
                return Ok(candidati);

            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOne(int id)
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                Nationality candidato = model.Nationalities.FirstOrDefault(o => o.Id == id);
                if (candidato == null) return NotFound();
                return Ok(candidato);

            }

        }

        [HttpPost("")]
        public ActionResult Create([FromBody] Nationality nuovaNationality)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                try
                {
                    model.Nationalities.Add(nuovaNationality);
                    model.SaveChanges();

                    return Ok(nuovaNationality.Id);
                }
                    
                catch
                {
                    return Problem();
                }
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Nationality nationalityAggiornata)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                Nationality candidate = model.Nationalities.FirstOrDefault(o => o.Id == id);
                if (candidate == null) return NotFound();
                if (id != nationalityAggiornata.Id) return BadRequest();

                candidate.Name = nationalityAggiornata.Name;
                candidate.Code = nationalityAggiornata.Code;
                model.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                Nationality candidate = model.Nationalities.FirstOrDefault(o => o.Id == id);
                if (candidate == null) return NotFound();
                model.Remove(candidate);
                model.SaveChanges();
                return Ok();
            }

        }
        [HttpGet("{id}/athletes")]
        public ActionResult GetAthletes(int id)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                //return Ok(model.Sports.Where(w => w.Id == id).Select(s => s.Athletes).ToList());

                return Ok(model.Athletes.Where(w => w.Nationality.Id == id).ToList());
            }
        }
        [HttpGet("{id1}/sports/{id2}")]
        public ActionResult GetAthletesSports(int id1,int id2)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                return Ok(model.Athletes.Where(w => w.Nationality.Id == id1).Where(w=>w.Sport.Id==id2).ToList());
            }
        }
    }
}
