using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.Models
{
    public class User
    {
        public int Id { get; set; }    
        public string UserName { get; set; } =string.Empty;
        public byte[]  passwordhash { get; set; }=new byte[0];
        public byte[] passwordSalt { get; set; }  =new byte[0];

        private List<Character>? characters;

        public List<Character>? GetCharacters()
        {
            return characters;
        }

        public void SetCharacters(List<Character>? value)
        {
            characters = value;
        }
    }
}