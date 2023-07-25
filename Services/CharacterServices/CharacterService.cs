using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNet.Data;
using DotNet.Dtos;
using DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
         private static List<Character> characters = new List<Character> {
            new Character(),
            new Character {Id=1, Name="Sarit"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Character>>> AddCharacter(AddCharactersRequest newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();            
            _context.Characters.Add(_mapper.Map<Character>(newCharacter));
            await _context.SaveChangesAsync();
            serviceResponse.Data=await _context.Characters.ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();  
            var dbCharacters= await _context.Characters.ToListAsync();
            serviceResponse.Data=dbCharacters.Select(c=> _mapper.Map<Character>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int Id)
        {
            var serviceResponse = new ServiceResponse<Character>();  
            var dbCharacters= await _context.Characters.FirstOrDefaultAsync(x=>x.Id==Id);
            //var charData=characters.FirstOrDefault(x=>x.Id==Id);
            serviceResponse.Data=dbCharacters;
             return serviceResponse;
        }
    }
}