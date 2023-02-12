namespace IManage.Authentication.Enum
{
    /// <summary>
    ///  Enum for all the possible exceptions.
    /// </summary>
    public enum TokenException
    {
        SecurityTokenExpiredException = 1,

        SecurityTokenInvalidSignatureException = 2,

        SecurityTokenInvalidSigningKeyException = 3,

        SecurityTokenInvalidException = 4,

        ArgumentNullException = 5,

        ArgumentException = 6
    }
}
