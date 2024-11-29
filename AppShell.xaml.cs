namespace MS_MauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.MS_NotePage), typeof(Views.MS_NotePage));
        }
    }
}
