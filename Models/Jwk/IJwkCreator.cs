using JwkTool2.Models.Dto;

namespace JwkTool2.Models.Jwk
{
    public interface IJwkCreator
    {
        JwkResponse CreateJwk(string pubKey, string kid, string alg);
        bool SaveToFile(string content);
    }
}
