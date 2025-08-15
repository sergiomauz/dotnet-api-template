using Microsoft.AspNetCore.Mvc;


namespace Application.Commons.RequestParams
{
    public class GuidRouteRequestParam
    {
        [FromRoute(Name = "id")]
        public Guid? Id { get; set; }
    }
}
