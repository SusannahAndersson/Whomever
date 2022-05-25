using AutoMapper;
using Whomever.Data.Entities;
using Whomever.Models;

namespace Whomever.Controllers
{
    //container for automapper
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            //mapping order to orderviewmodel (match property, spec orderid to get corr orderid)
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, opt => opt.MapFrom(o => o.Id))
                .ReverseMap();
        }
    }
}