namespace BibleApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ReadPage), typeof(ReadPage));
        }
    }
}
