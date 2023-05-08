using back_end_plante.Common.Models;

namespace back_end_plante.Common.Requests.user;

public class UpdateUserRequest : BaseUserRequest
{
    public List<Adress> Adresses { get; set; }
}