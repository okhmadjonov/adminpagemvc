
using AdminPageMVC.DTO;
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
            CreateMap<Homework, HomeworkDTO>().ReverseMap();
            CreateMap<LessonDTO, AddLessonDto>().ReverseMap();
            CreateMap<Result, AddResultDto>().ReverseMap();
            CreateMap<TaskDTO, AddTaskDto>().ReverseMap();
            CreateMap<Teacher, AddTeacherDto>().ReverseMap();
            CreateMap<TeacherDTO, AddTeacherDto>().ReverseMap();
            CreateMap<Test, AddTestDto>().ReverseMap();
        }

    }
}
