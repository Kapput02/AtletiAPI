using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtletiAPI.Models;

namespace AtletiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        //sports
        //Get
        [HttpGet]
        [Route("")]
        public ActionResult Get()
        {
            using (OlympicsContext model = new OlympicsContext())
            {

                List<Sport> candidati = model.Sports.ToList();
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

                Sport candidato = model.Sports.FirstOrDefault(o => o.Id == id);
                if (candidato == null) return NotFound();
                return Ok(candidato);

            }
            
        }

        [HttpPost("")]
        public ActionResult Create([FromBody]Sport nuovoSport)
        {
            using(OlympicsContext model = new OlympicsContext())
            {
                try
                {
                    model.Sports.Add(nuovoSport);
                    model.SaveChanges();
                    return Ok();
                }
                catch
                {
                    return Problem();
                }
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id,[FromBody] Sport sportAggiornato)
        {
            using(OlympicsContext model = new OlympicsContext())
            {
                Sport candidate = model.Sports.FirstOrDefault(o => o.Id == id);
                if (candidate == null) return NotFound();
                if (id != sportAggiornato.Id) return BadRequest();

                candidate.Name = sportAggiornato.Name;
                model.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using(OlympicsContext model = new OlympicsContext()) 
            {
                Sport candidate = model.Sports.FirstOrDefault(o => o.Id == id);
                if (candidate == null) return NotFound();
                model.Remove(candidate);
                model.SaveChanges();
                return Ok();
            }

        }
        [HttpGet("{id}/athletes")]
        public ActionResult GetAthletes(int id)
        {
            using(OlympicsContext model = new OlympicsContext())
            {
                //return Ok(model.Sports.Where(w => w.Id == id).Select(s => s.Athletes).ToList());

                return Ok(model.Athletes.Where(w => w.Sport.Id == id).ToList());
            }
        }
        //sports/76
        //GetOne(int id)
    }
}
