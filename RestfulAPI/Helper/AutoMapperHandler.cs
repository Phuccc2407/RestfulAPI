using AutoMapper;
using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;

namespace RestfulAPI.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<User, CustomerModal>().ForMember(
                item => item.Statusname, 
                static opt => opt.MapFrom(
                    item => (item.IsActive ? "Active" : "Inactive")
                )
            ).ReverseMap();
        }
    }
}
