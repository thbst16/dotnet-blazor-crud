using AutoMapper;
using BlazorCrud.Shared.Models;

namespace BlazorCrud.Shared.Data
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Patient, Patient>().ReverseMap();
            CreateMap<Organization, Organization>().ReverseMap();
            CreateMap<Claim, Claim>().ReverseMap();
            CreateMap<Upload, Upload>().ReverseMap();
            CreateMap<User, User>().ReverseMap();
        }
    }
}
