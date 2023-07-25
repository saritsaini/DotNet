using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Models;

namespace DotNet.Dtos
{
    public class AddCharactersRequest
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public RPG RPG { get; set; } = RPG.Knight;
    }
}