namespace BibleApp.Pages;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }       

    // Go to the reading page starting at Genesis 1 
    private async void OnReadSequentiallyClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ReadPage)}?book=Genesis&chapter=1");
    }
}
