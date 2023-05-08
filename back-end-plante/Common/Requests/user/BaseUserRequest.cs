namespace back_end_plante.Common.Requests.user;

public abstract class BaseUserRequest
{
    public string Name { get; set; }
    
    public string SurName { get; set; }
    
    public string Mail { get; set; }
    
    public string Password { get; set; }
    
    public string PhoneNumer { get; set; }
    
    public string Avatar { get; set; }

    public virtual bool IsValid()
    {
        if (string.IsNullOrEmpty(Name)
            || string.IsNullOrEmpty(SurName)
            || string.IsNullOrEmpty(Mail)
            || string.IsNullOrEmpty(Password)
            || string.IsNullOrEmpty(Name)
            || string.IsNullOrEmpty(PhoneNumer)
            || string.IsNullOrEmpty(Avatar))
            return false;
        
        //TODO: Vérif if is realy a mail
        //TODO: Vérif if password is strong

        //TODO: Vérif vraiment la forme d'un numéro
        return true;
    }
}