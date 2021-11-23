using AtletiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtletiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult Get()
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                List<Athlete> candidati = model.Athletes.ToList();
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

                Athlete candidato = model.Athletes.FirstOrDefault(o => o.Id == id);
                if (candidato == null) return NotFound();
                return Ok(candidato);

            }

        }
        [HttpGet]
        [Route("{id}/nationality")]
        public ActionResult GetOneByNations(int id)
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                Athlete a = model.Athletes.FirstOrDefault(o => o.Id == id);
                if (a == null || a.FkNationality == 0) return NotFound();
                var retVal = model.Nationalities.Select(s => new { s.Id, s.Name }).FirstOrDefault(q => q.Id == a.FkNationality);
                return Ok(retVal);


            }

        }
        [HttpGet]
        [Route("{id}/sport")]
        public ActionResult GetOneBySport(int id)
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                Athlete a = model.Athletes.FirstOrDefault(o => o.Id == id);
                if (a == null || a.FkSport == 0) return NotFound();
                var retVal = model.Sports.Select(s => new { s.Id, s.Name }).FirstOrDefault(q => q.Id == a.FkSport);
                return Ok(retVal);


            }

        }
        [HttpGet]
        [Route("the-best")]
        public ActionResult GetTheBest()
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                Athlete candidato = model.Athletes.OrderByDescending(o => o.Gold)
                    .ThenByDescending(o=> o.Silver)
                    .ThenByDescending(o=> o.Bronze)
                    .FirstOrDefault();
                if (candidato == null) return NotFound();
                return Ok(candidato);

            }

        }
        [HttpPost("")]
        public ActionResult Create([FromBody] Athlete nuovoAthlete)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                try
                {
                    model.Athletes.Add(nuovoAthlete);
                    model.SaveChanges();

                    return Ok(nuovoAthlete.Id);
                }
                catch
                {
                    return Problem();
                }
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Athlete athleteAggiornato)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                Athlete candidate = model.Athletes.FirstOrDefault(o => o.Id == id);
                if (candidate == null) return NotFound();
                if (id != athleteAggiornato.Id) return BadRequest();
                candidate.Name = athleteAggiornato.Name;
                candidate.Nationality = athleteAggiornato.Nationality;
                model.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (OlympicsContext model = new OlympicsContext())
            {
                Athlete candidate = model.Athletes.FirstOrDefault(o => o.Id == id);
                if (candidate == null) return NotFound();
                model.Remove(candidate);
                model.SaveChanges();
                return Ok();
            }

        }
    }
}
