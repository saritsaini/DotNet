using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNet.Models;

namespace DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {        
        private static List<Characters> charObjs = new List<Characters> {
            new Characters(),
            new Characters {Id=1, Name="Sarit"}
        };


        [HttpGet]
        public ActionResult<Characters> Get()
        {
            return Ok(charObjs);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Int32 Id) {
            return Ok(charObjs.FirstOrDefault(x=>x.Id==Id));
        }
        
        [HttpPost]
        public IActionResult AddCharacter(Characters newChar) {
            charObjs.Add(newChar);
            return Ok(charObjs);
        }
    }
}