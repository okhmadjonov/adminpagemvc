
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;

namespace AdminPageMVC.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Course, AddCourseDto>().ReverseMap();
        }

    }
}
