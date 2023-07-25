using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNet.Models;
using DotNet.Services.CharacterServices;
using DotNet.Dtos;

namespace DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Character>>> Get()
        {
            return Ok(await this._characterService.GetAllCharacters());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Int32 Id) {
            return Ok(await this._characterService.GetCharacterById(Id));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharactersRequest newChar) {
            return Ok(await this._characterService.AddCharacter(newChar));
        }
    }
}