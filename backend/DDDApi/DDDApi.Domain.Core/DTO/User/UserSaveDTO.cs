namespace DDDApi.Domain.Core.DTO.User
{
    public class UserSaveDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
