using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        // api/projects?query=vai ficar assim
        [HttpGet]
        public IActionResult Get(string query)
        {
            // ? BUSCAR TODOS OU FILTRAR

            return Ok();
        }

        // api/projects/599
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // ? BUSCAR ÚNICO PELO ID

            // return NotFound();

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            // ? CRIAR REGISTRO

            if (createProject.Title.Length > 50)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = createProject.Id }, createProject);
        }

        // api/projects/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
        {
            // ? ATUALIZAR REGISTRO

            if (updateProject.Description.Length > 200)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // api/projects/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // ? BUSCAR REGISTRO SE NÃO EXISTIR RETORNA NotFound

            // ? REMOVER

            return NoContent();
        }
    }
}