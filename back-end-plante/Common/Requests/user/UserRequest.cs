using back_end_plante.Common.Models;

namespace back_end_plante.Common.Requests.user;

public class UserRequest : BaseUserRequest
{
    public Adress Adresse { get; set; }

    public override bool IsValid()
    {
        return base.IsValid() && Adresse.IsValid();
    }
}