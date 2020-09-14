using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlmacenWS.Models;
using AlmacenWS.Models.Response;
using AlmacenWS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AlmacenWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (Almacen_dbContext db = new Almacen_dbContext())
                {
                    var listaCategorias = db.Categoria.OrderByDescending(t => t.IdCategoria).ToList();
                    respuesta.Exito = 1;
                    respuesta.Data = listaCategorias;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult Add(CategoriaViewModel Categoria)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (Almacen_dbContext db = new Almacen_dbContext())
                {
                    Categoria categoria = new Categoria();
                    categoria.NombreCategoria = Categoria.NombreCategoria;
                    categoria.DescripcionCategoria = Categoria.DescripcionCategoria;
                    db.Categoria.Add(categoria);
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPut]
        public IActionResult Edit(CategoriaViewModel Categoria)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (Almacen_dbContext db = new Almacen_dbContext())
                {
                    Categoria categoria = db.Categoria.Find(Categoria.IdCategoria);
                    categoria.NombreCategoria = Categoria.NombreCategoria;
                    categoria.DescripcionCategoria = Categoria.DescripcionCategoria;
                    db.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (Almacen_dbContext db = new Almacen_dbContext())
                {
                    Categoria categoria = db.Categoria.Find(Id);
                    db.Remove(categoria);
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
