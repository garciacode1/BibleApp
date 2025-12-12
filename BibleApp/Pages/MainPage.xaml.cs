namespace BibleApp.Pages;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }       

    //event button read sequentially 
    private async void OnReadSequentiallyClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ReadPage)}" + "?bookId=GEN" + "&chapterId=GEN.1" + "&reference=Genesis%201");
    }
}
