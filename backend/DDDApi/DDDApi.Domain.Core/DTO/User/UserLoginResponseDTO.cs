namespace DDDApi.Domain.Core.DTO.User
{
    public class UserLoginResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
