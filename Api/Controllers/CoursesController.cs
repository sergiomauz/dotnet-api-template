using Microsoft.AspNetCore.Mvc;
using Application.Commons.VMs;
using Application.UseCases.Courses.Commands.CreateCourse;
using Application.UseCases.Courses.Commands.DeleteCourses;
using Application.UseCases.Courses.Commands.UpdateCourse;
using Application.UseCases.Courses.Queries.GetCourseById;
using Application.UseCases.Courses.Queries.GetStudentsByCourseId;
using Application.UseCases.Courses.Queries.SearchCoursesByTextFilter;
using Application.UseCases.Courses.Queries.SearchCoursesByObject;


namespace Api.Controllers
{
    [Route("api/courses")]
    public class CoursesController : CustomControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<CreateCourseVm>> CreateCourse([FromBody] CreateCourseDto body)
        {
            var command = Mapper.Map<CreateCourseCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteCoursesVm>> DeleteCourse([FromRoute] DeleteCoursesRoute route)
        {
            var command = Mapper.Map<DeleteCoursesCommand>(route);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCourseVm>> UpdateCourse([FromRoute] UpdateCourseRoute route, [FromBody] UpdateCourseDto body)
        {
            var command = Mapper.Map<UpdateCourseCommand>(route);
            Mapper.Map(body, command);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCourseByIdVm>> GetCourseById([FromRoute] GetCourseByIdRoute route)
        {
            var query = Mapper.Map<GetCourseByIdQuery>(route);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}/get-students")]
        public async Task<ActionResult<PaginatedVm<GetStudentsByCourseIdVm>>> GetStudentsByCourseId([FromRoute] GetStudentsByCourseIdRoute route, [FromQuery] GetStudentsByCourseIdRequestParams queryParams)
        {
            var query = Mapper.Map<GetStudentsByCourseIdQuery>(route);
            Mapper.Map(queryParams, query);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("")]
        public async Task<ActionResult<PaginatedVm<SearchCoursesByTextFilterVm>>> SearchCoursesByTextFilter([FromQuery] SearchCoursesByTextFilterRequestParams queryParams)
        {
            var query = Mapper.Map<SearchCoursesByTextFilterQuery>(queryParams);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginatedVm<SearchCoursesByObjectVm>>> SearchCoursesByObject([FromBody] SearchCoursesByObjectDto queryBody)
        {
            var query = Mapper.Map<SearchCoursesByObjectQuery>(queryBody);
            Mapper.Map(Request, query);

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<DeleteCoursesVm>> DeleteCourse([FromBody] DeleteCoursesDto body)
        {
            var command = Mapper.Map<DeleteCoursesCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
