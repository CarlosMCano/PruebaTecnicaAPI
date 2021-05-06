using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Prueba.Modelos;
using Prueba.VM;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SincronizacionController : ControllerBase
    {
        private readonly Context _context;
        private string sincronizacion;

        public SincronizacionController(Context context, IConfiguration configuration)
        {
            _context = context;
            sincronizacion = configuration.GetConnectionString("UrlApiExterna");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post()
        {
            VMRespuesta resul = new VMRespuesta();
            try
            {
                bool sincronizoAutores = SincronizarAutores();
                bool sincronizoLibros = SincronizarLibros();

                if (sincronizoAutores && sincronizoLibros)
                {
                    resul.Error = false;
                    resul.Mensaje = "La sincronizacion finalizó con exito";
                } else
                {
                    resul.Error = true;
                    resul.Mensaje = "La sincronizacion falló";
                }


                return Ok(resul);
            }
            catch (Exception ex)
            {
                resul.Error = true;
                resul.Mensaje = ex.Message;
                return BadRequest(resul);
            }
        }


        internal bool SincronizarAutores()
        {
            try
            {
                var client = new RestClient(sincronizacion + "/Authors");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                //Console.WriteLine(response.Content);
                List<Autor> autores = JsonConvert.DeserializeObject<List<Autor>>(response.Content);

                if (autores.Count > 0)
                {
                    List<Autor> autoresLocales = _context.Autores.ToList();

                    if (autoresLocales.Count == autores.Count)
                    {
                        return true;
                    } else
                    {
                        _context.Autores.RemoveRange(autoresLocales);
                    }
                }

                foreach (var item in autores)
                {
                    _context.Autores.Add(item);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        internal bool SincronizarLibros()
        {
            try
            {
                var client = new RestClient(sincronizacion + "/Books");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                //Console.WriteLine(response.Content);
                List<Libro> libros = JsonConvert.DeserializeObject<List<Libro>>(response.Content);

                if (libros.Count > 0)
                {
                    List<Libro> librosLocales = _context.Libros.ToList();

                    if (librosLocales.Count == libros.Count)
                        return true;
                }

                foreach (var item in libros)
                {
                    _context.Libros.Add(item);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
