using AutoMapper;
using Microsoft.AspNetCore.Http;
using UniversityApi.Data;
using UniversityApi.Dtos;

namespace Services.Profiles
{
    public class MapProfile : Profile
    {
        private readonly IHttpContextAccessor _context;

        public MapProfile(IHttpContextAccessor httpContextAccessor)
        {

            _context = httpContextAccessor;

            var uriBuilder = new UriBuilder(_context.HttpContext.Request.Scheme, _context.HttpContext.Request.Host.Host, _context.HttpContext.Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;

            CreateMap<Group, GroupGetDto>();
            //.ForMember(dest => dest.Name, s => s.MapFrom(s => s.No));
            CreateMap<CreateDto, Group>();

            CreateMap<Student, GetStudentDto>()
                .ForMember(dest => dest.Age, s => s.MapFrom(s => DateTime.Now.Year - s.BirthDate.Year))
                .ForMember(dest => dest.ImageName, s => s.MapFrom(s => baseUrl + "students/" + s.ImageName));
        }
    }
}
