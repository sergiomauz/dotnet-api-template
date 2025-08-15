using Microsoft.AspNetCore.Mvc;
using Application.Commons.VMs;
using Application.UseCases.Students.Commands.CreateStudent;
using Application.UseCases.Students.Commands.DeleteStudents;
using Application.UseCases.Students.Commands.UpdateStudent;
using Application.UseCases.Students.Queries.GetCoursesByStudentId;
using Application.UseCases.Students.Queries.GetStudentById;
using Application.UseCases.Students.Queries.SearchStudentsByTextFilter;
using Application.UseCases.Students.Queries.SearchStudentsByObject;


namespace Api.Controllers
{
    [Route("api/students")]
    public class StudentsController : CustomControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<CreateStudentVm>> CreateStudent([FromBody] CreateStudentDto body)
        {
            var command = Mapper.Map<CreateStudentCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteStudentsVm>> DeleteStudent([FromRoute] DeleteStudentsRoute route)
        {
            var command = Mapper.Map<DeleteStudentsCommand>(route);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateStudentVm>> UpdateStudent([FromRoute] UpdateStudentRoute route, [FromBody] UpdateStudentDto body)
        {
            var command = Mapper.Map<UpdateStudentCommand>(route);
            Mapper.Map(body, command);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentByIdVm>> GetStudentById([FromRoute] GetStudentByIdRoute route)
        {
            var query = Mapper.Map<GetStudentByIdQuery>(route);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}/get-courses")]
        public async Task<ActionResult<PaginatedVm<GetCoursesByStudentIdVm>>> GetCoursesByStudentId([FromRoute] GetCoursesByStudentIdRoute studentRoute, [FromQuery] GetCoursesByStudentIdRequestParams queryParams)
        {
            var query = Mapper.Map<GetCoursesByStudentIdQuery>(studentRoute);
            Mapper.Map(queryParams, query);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("")]
        public async Task<ActionResult<PaginatedVm<SearchStudentsByTextFilterVm>>> SearchCoursesByTextFilter([FromQuery] SearchStudentsByTextFilterRequestParams queryParams)
        {
            var query = Mapper.Map<SearchStudentsByTextFilterQuery>(queryParams);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginatedVm<SearchStudentsByObjectVm>>> SearchCoursesByObject([FromBody] SearchStudentsByObjectDto queryBody)
        {
            var query = Mapper.Map<SearchStudentsByObjectQuery>(queryBody);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<DeleteStudentsVm>> DeleteStudent([FromBody] DeleteStudentsDto body)
        {
            var command = Mapper.Map<DeleteStudentsCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
