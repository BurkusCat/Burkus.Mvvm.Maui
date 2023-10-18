namespace Burkus.Mvvm.Maui;

public class BurkusMvvmException : Exception
{
    public BurkusMvvmException(string? message)
        : base(message)
    {
    }

    public BurkusMvvmException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}