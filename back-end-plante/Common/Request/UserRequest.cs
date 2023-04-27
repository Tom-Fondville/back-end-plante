using back_end_plante.Common.Models;
using back_end_plante.Utils;

namespace back_end_plante.Common.Request;

public class UserRequest
{
    public string Name { get; set; }
    
    public string SurName { get; set; }
    
    public string Mail { get; set; }
    
    public string Password { get; set; }
    
    public long PhoneNumer { get; set; }
    
    public Adress Adresse { get; set; }
    
    public string Avatar { get; set; }

    public bool Ivalid()
    {
        if (string.IsNullOrEmpty(Name)
            || string.IsNullOrEmpty(SurName)
            || string.IsNullOrEmpty(Mail)
            || string.IsNullOrEmpty(Password)
            || string.IsNullOrEmpty(Name)
            || string.IsNullOrEmpty(Avatar))
            return false;
        
        //TODO: Vérif if is realy a mail
        //TODO: Vérif if password is strong

        if (PhoneNumer is 0) return false;
        
        //TODO: Vérif vraiment la forme d'un numéro
        
        
        
        return true && Adresse.IsValid();
    }
}