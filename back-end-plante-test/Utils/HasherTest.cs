using back_end_plante.Utils;

namespace back_end_plante_test.Utils;


public class HasherTest
{

    [Test]
    public void test_hasher()
    {
        //Variables
        String password = "password";

        // Class to test
        String result = Hasher.Hash(password);

        //Asserts
        Assert.AreNotEqual(password, result);

    }
    
    
}