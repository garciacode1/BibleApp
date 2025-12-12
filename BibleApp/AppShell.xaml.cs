using BibleApp.Pages;

namespace BibleApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ChaptersPage), typeof(ChaptersPage));
        Routing.RegisterRoute(nameof(ReadPage), typeof(ReadPage));
    }
}
