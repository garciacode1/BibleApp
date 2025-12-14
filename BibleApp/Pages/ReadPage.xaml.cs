namespace BibleApp.Pages;
using BibleApp.Services;
using BibleApp.Services.Responses;
using System.Net;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

[QueryProperty(nameof(BookId), "bookId")]
[QueryProperty(nameof(ChapterId), "chapterId")]
[QueryProperty(nameof(Reference), "reference")]

public partial class ReadPage : ContentPage
{


    private readonly APIService api = new();

    public int currentChapter;
    public string BookId { get; set; }
    public string ChapterId { get; set; }
    public string Reference { get; set; }

    public ReadPage()
    {
        InitializeComponent();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadChapter();
    }

    private async Task LoadChapter()
    {
        ChapterTitleLabel.Text = "Loading chapter...";
        //api request for text
        var chapter = await api.GetChapterText(ChapterId);
        //error checking if API didnt return anything
        if (chapter == null)
        {
            ChapterTitleLabel.Text = "Error to load this chapter. ";
            return;
        }
        //title of chapter
        ChapterTitleLabel.Text = Reference;
        //cleaned text from html format
        string CleanedText = CleanHtml(chapter.Content);
        ChapterContentLabel.Text = CleanedText;
        //parse book and chapter when page loads
        var parts = Reference.Split(' ');
        currentChapter = int.Parse(parts.Last());


    }

    private string CleanHtml(string html)
    {
        if (string.IsNullOrEmpty(html)) return string.Empty;
        //remove html tags
        string text = Regex.Replace(html, "<.*?>", string.Empty);
        //decode httml entities
        text = WebUtility.HtmlDecode(text);  //decode htmlentities
        text = text.Replace("¶", ""); //decode symbols

        text = Regex.Replace(text, @"(\d+)(?=[A-Za-z])", "\n$1 "); //adds line before verse number

        return text.Trim();


    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        if (currentChapter <= 0)
            return;

        currentChapter++;

        await Shell.Current.GoToAsync($"{nameof(ReadPage)}" + $"?bookId={BookId}" +
            $"&chapterId={BookId}.{currentChapter}" + $"&reference={Uri.EscapeDataString($"{BookId} {currentChapter}")}");
    }

    private async void OnPreviousClicked(object sender, EventArgs e)
    {
        if (currentChapter <= 1)
            return;

        currentChapter--;

        await Shell.Current.GoToAsync($"{nameof(ReadPage)}" + $"?bookId={BookId}" + $"&chapterId={BookId}.{currentChapter}" +
            $"&reference={Uri.EscapeDataString($"{BookId} {currentChapter}")}");


    }

    private void OnSwipedLeft(object sender, SwipedEventArgs e)
    {

        OnNextClicked(sender, EventArgs.Empty);


    }

    private void OnSwipedRight(object sender, SwipedEventArgs e)
    {

        OnPreviousClicked(sender, EventArgs.Empty);

    }

    private void OnAddFavoriteClicked(object sender, EventArgs e)
    {
        FavoriteStore.Favorites.Add(new FavoriteChapter
        {
            ChapterId = ChapterId,
            Reference = Reference,
            BookId = BookId
        });

    }













}
