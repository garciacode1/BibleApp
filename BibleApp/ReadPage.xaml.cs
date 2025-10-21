namespace BibleApp;

[QueryProperty(nameof(Book), "book")]
[QueryProperty(nameof(Chapter), "chapter")]
public partial class ReadPage : ContentPage
{
    public string Book { get; set; } = "Genesis";
    public string Chapter { get; set; } = "1";

    public ReadPage()
    {
        InitializeComponent();
        UpdateHeader();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        UpdateHeader();
    }

    private void UpdateHeader()
    {
        if (HeaderLabel != null)
            HeaderLabel.Text = $"{Book} {Chapter}";
    }
}
