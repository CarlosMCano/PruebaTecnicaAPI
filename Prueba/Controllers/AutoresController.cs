using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Prueba.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly Context _context;

        public AutoresController(Context context, IConfiguration configuration)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            try
            {
                List<Autor> autores = _context.Autores.ToList();

                foreach (var item in autores)
                {
                    item.libro = _context.Libros.FirstOrDefault(z => z.Id == item.IdBook);
                }

                return Ok(autores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult AutorByFiltro(string filtro, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<Autor> autor = new List<Autor>();


                if (EsEntero(filtro))
                {
                    int identificador = Convert.ToInt32(filtro);
                    autor = (from a in _context.Autores
                             join l in _context.Libros on a.IdBook equals l.Id
                             where
                             (a.Id == identificador || a.FirstName.Contains(filtro)) && 
                             (l.PublishDate.Date >= FechaInicio.Date && l.PublishDate.Date <= FechaFin.Date)
                             select a).ToList();
                } else
                {
                    autor = (from a in _context.Autores
                             join l in _context.Libros on a.IdBook equals l.Id
                             where
                             a.FirstName.Contains(filtro) && l.PublishDate.Date >= FechaInicio.Date && l.PublishDate.Date <= FechaFin.Date
                             select a).ToList();
                }

                foreach (var item in autor)
                {
                    item.libro = _context.Libros.FirstOrDefault(x => x.Id == item.Id);
                }

                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        internal bool EsEntero(string num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
