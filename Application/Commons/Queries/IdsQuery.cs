using AutoMapper;
using Application.Commons.Mapping;
using Application.Commons.RequestParams;


namespace Application.Commons.Queries
{
    public class IdsQuery :
        IMapFrom<IdRouteRequestParam>,
        IMapFrom<IdsBodyRequestParam>
    {
        public int? Id { get; set; }
        public List<int>? Ids { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<IdRouteRequestParam, IdsQuery>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id));

            profile.CreateMap<IdsBodyRequestParam, IdsQuery>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => o.Ids));
        }
    }
}
