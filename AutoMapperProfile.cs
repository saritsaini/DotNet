using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNet.Dtos;
using DotNet.Models;

namespace DotNet
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, AddCharactersRequest>();
            CreateMap<AddCharactersRequest, Character>();
        }
    }
}