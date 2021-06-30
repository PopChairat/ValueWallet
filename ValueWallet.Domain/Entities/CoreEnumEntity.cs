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
        Biometric = 2,
        PinCode = 3
    }
}
