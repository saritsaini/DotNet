using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Dtos;
using DotNet.Models;

namespace DotNet.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<Character>> GetCharacterById(int Id);
        Task<ServiceResponse<List<Character>>> AddCharacter(AddCharactersRequest newCharacter);
    }
}