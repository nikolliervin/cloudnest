public class UpdateUserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string OldPassword { get; set; }
    public string Email { get; set; }
}