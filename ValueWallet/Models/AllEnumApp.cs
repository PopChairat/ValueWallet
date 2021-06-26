namespace ValueWallet.Models
{
    public enum Platform
    {
        None = 0,
        iOS = 1,
        Android = 2
    }

    public enum KeyAppType
    {
        None,
        KeySecret,
        UserInfo,
    }

    public enum AlertLevel
    {
        Normal,
        Warning,
        Error
    }
}
