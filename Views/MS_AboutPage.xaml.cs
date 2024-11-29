namespace MS_MauiApp.Views;

public partial class MS_AboutPage : ContentPage
{
	public MS_AboutPage()
	{

		InitializeComponent();
	}
    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.MS_About about)
            // Navigate to the specified URL in the system browser. hola
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
    }
}