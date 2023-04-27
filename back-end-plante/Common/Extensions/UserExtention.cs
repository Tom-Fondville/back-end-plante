using back_end_plante.Common.Models;
using back_end_plante.Common.Request;
using back_end_plante.Utils;

namespace back_end_plante.Common.Extensions;

public static class UserExtention
{
    public static User ToUser(this UserRequest userRequest)
    {
        return new User
        {
            Name = userRequest.Name,
            SurName = userRequest.SurName,
            Mail = userRequest.Mail,
            Password = Hasher.Hash(userRequest.Password),
            PhoneNumer = userRequest.PhoneNumer,
            IsAdmin = false,
            IsBotaniste = false,
            Adresses = new List<Adress> { userRequest.Adresse }
        };
    }
}