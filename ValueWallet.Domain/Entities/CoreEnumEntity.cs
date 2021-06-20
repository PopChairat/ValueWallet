using System;
namespace ValueWallet.Domain.Entities
{
    public enum StatusCode
    {
        Success = 1000,
        UnSucess = 2000,
        Unknow = 9999
    }

    public enum LoginBy
    {
        None = 0,
        Password = 1,
        Fingerprint = 2,
        FaceId = 3
    }
}
