using System.Security.Cryptography;
using System.Text;

namespace back_end_plante.Utils;

public static class Hasher
{
    public static string Hash(string str)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(str);
        var hashed = sha.ComputeHash(asByteArray);
        return Convert.ToBase64String(hashed);
    }
    
}