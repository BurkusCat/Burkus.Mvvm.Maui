namespace DemoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

#if NET8_0_OR_GREATER && !ANDROID && !MACCATALYST && !IOS && !WINDOWS
    // workaround for unit tests in Maui https://github.com/dotnet/maui/issues/3552#issuecomment-1172606125
    public static void Main(string[] args) {}
#endif
}