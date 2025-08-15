using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Enrollments.Commands.CreateEnrollment;
using Application.UseCases.Enrollments.Commands.DeleteEnrollments;


namespace Api.Controllers
{
    [Route("api/enrollments")]
    public class EnrollmentsController : CustomControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<CreateEnrollmentVm>> CreateEnrollment([FromBody] CreateEnrollmentDto body)
        {
            var command = Mapper.Map<CreateEnrollmentCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteEnrollmentsVm>> DeleteEnrollment([FromRoute] DeleteEnrollmentsRoute route)
        {
            var command = Mapper.Map<DeleteEnrollmentsCommand>(route);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<DeleteEnrollmentsVm>> DeleteEnrollment([FromBody] DeleteEnrollmentsDto body)
        {
            var command = Mapper.Map<DeleteEnrollmentsCommand>(body);
            Mapper.Map(Request, command);

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
