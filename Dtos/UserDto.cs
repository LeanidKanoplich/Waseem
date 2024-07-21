namespace SolarpayAPI.Dtos
{
    public class UserDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Id { get; internal set; }
    }
}
