namespace Coders.WebAPI.Models.Entities
{
    public class Coder
    {
        public int Id { get; set; }

        public string First_Name { get; set; } = null!;

        public string Job { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
