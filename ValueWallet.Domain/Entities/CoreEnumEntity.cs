namespace ValueWallet.Domain.Entities
{
    public enum StatusCode
    {
        Success = 1000,
        Fail = 2000,
        Error = 3000,
        Unknow = 9999
    }

    public enum LoginBy
    {
        None = 0,
        Password = 1,
        Fingerprint = 2,
        FaceId = 3,
        BiometricDroid = 4,
        PassCode = 5
    }
}
