namespace back_end_plante.Common.Responses.User;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string UserId { get; set; }
}