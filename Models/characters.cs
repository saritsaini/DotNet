namespace DotNet.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public RPG RPG { get; set; } = RPG.Knight;

        public User? User { get; set; }
    }
}