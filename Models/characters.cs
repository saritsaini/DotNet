namespace DotNet.Models
{
public class Characters
{
    public int Id { get; set; }
    public string Name { get; set; }="";
    public RPG RPG { get; set; }=RPG.Knight;
}
}