using Microsoft.AspNetCore.Mvc;
using Application.Commons.VMs;
using Application.UseCases.Teachers.Commands.CreateTeacher;
using Application.UseCases.Teachers.Commands.DeleteTeachers;
using Application.UseCases.Teachers.Commands.UpdateTeacher;
using Application.UseCases.Teachers.Queries.GetTeacherById;
using Application.UseCases.Teachers.Queries.GetCoursesByTeacherId;
using Application.UseCases.Teachers.Queries.GetStudentsByTeacherId;
using Application.UseCases.Teachers.Queries.SearchTeachersByTextFilter;
using Application.UseCases.Teachers.Queries.SearchTeachersByObject;


namespace Api.Controllers
{
    [Route("api/teachers")]
    public class TeachersController : CustomControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<CreateTeacherVm>> CreateTeacher([FromBody] CreateTeacherDto body)
        {
            var command = Mapper.Map<CreateTeacherCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteTeachersVm>> DeleteTeacher([FromRoute] DeleteTeachersRoute route)
        {
            var command = Mapper.Map<DeleteTeachersCommand>(route);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateTeacherVm>> UpdateTeacher([FromRoute] UpdateTeacherRoute route, [FromBody] UpdateTeacherDto body)
        {
            var command = Mapper.Map<UpdateTeacherCommand>(route);
            Mapper.Map(body, command);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTeacherByIdVm>> GetTeacherById([FromRoute] GetTeacherByIdRoute route)
        {
            var query = Mapper.Map<GetTeacherByIdQuery>(route);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}/get-courses")]
        public async Task<ActionResult<PaginatedVm<GetCoursesByTeacherIdVm>>> GetCoursesByTeacherId([FromRoute] GetCoursesByTeacherIdRoute route, [FromQuery] GetCoursesByTeacherIdRequestParams queryParams)
        {
            var query = Mapper.Map<GetCoursesByTeacherIdQuery>(route);
            Mapper.Map(queryParams, query);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}/get-students")]
        public async Task<ActionResult<PaginatedVm<GetStudentsByTeacherIdVm>>> GetStudentsByTeacherId([FromRoute] GetStudentsByTeacherIdRoute route, [FromQuery] GetStudentsByTeacherIdRequestParams queryParams)
        {
            var query = Mapper.Map<GetStudentsByTeacherIdQuery>(route);
            Mapper.Map(queryParams, query);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("")]
        public async Task<ActionResult<PaginatedVm<SearchTeachersByTextFilterVm>>> SearchCoursesByTextFilter([FromQuery] SearchTeachersByTextFilterRequestParams queryParams)
        {
            var query = Mapper.Map<SearchTeachersByTextFilterQuery>(queryParams);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginatedVm<SearchTeachersByObjectVm>>> SearchCoursesByObject([FromBody] SearchTeachersByObjectDto queryBody)
        {
            var query = Mapper.Map<SearchTeachersByObjectQuery>(queryBody);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<DeleteTeachersVm>> DeleteTeacher([FromBody] DeleteTeachersDto body)
        {
            var command = Mapper.Map<DeleteTeachersCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
