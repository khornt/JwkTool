namespace JwkTool2.Models.Jwk
{
    public interface IPubKeyHandler
    {
        string GetPublicKey(string filename);
        string? GetPublicKeyFromSertificate(string kid);        
    }
}
