namespace AuthAPI.Models.Dto
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required DateTime Updated { get; set; }
        public required DateTime Created { get; set; }
    }
}
