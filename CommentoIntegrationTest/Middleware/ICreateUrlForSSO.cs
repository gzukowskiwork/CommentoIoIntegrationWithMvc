using CommentoIntegrationTest.Models;

namespace CommentoIntegrationTest.Middleware
{
    public interface ICreateUrlForSSO
    {
        string DoOperations(ApplicationUser user, string token, string hmac);
    }
}
