using AutoMapper;
using CommentoIntegrationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentoIntegrationTest
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistration, ApplicationUser>()
                .ForMember(u => u.UserName, x => x.MapFrom(y => y.Name));

            
        }
    }
}
