using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator) // * INJEÇÃO DE DEPENDÊNCIA
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            var getAllProjectsQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        /// <summary>
        /// Get Project by your id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var getProjectByIdQuery = new GetProjectByIdQuery(id);

            var project = await _mediator.Send(getProjectByIdQuery);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        /// <summary>
        /// Create a new Project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        /// <summary>
        /// Update a Project by your id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete a project by your id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Adds a comment to an existing project using your id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Changes the status of a project to "InProgress" by your id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns></returns>
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Changes the status of a project to "PaymentPending" by your id and sends a message to the payment microservice using RabbitMQ
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
        {
            command.IdProject = id;

            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("O pagamento não pôde ser processado.");

            return Accepted();
        }
    }
}