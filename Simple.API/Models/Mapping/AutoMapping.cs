using AutoMapper;
using Microsoft.Extensions.Hosting;
using Simple.API.Infrastructure.Entities;
using Simple.Common.Model.Auth;

namespace Simple.API.Models.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<LoginRes, AppUser>().ReverseMap();
        }
    }
}
