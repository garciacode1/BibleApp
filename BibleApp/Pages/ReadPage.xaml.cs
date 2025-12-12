namespace BibleApp.Pages;
using BibleApp.Services;
using BibleApp.Services.Responses;
using System.Reflection.Metadata;

[QueryProperty(nameof(ChapterId), "chapterId")]
[QueryProperty(nameof(Reference), "reference")]

public partial class ReadPage : ContentPage
{
    

    private readonly APIService api = new();

    public string ChapterId { get; set;}
    public string Reference { get; set; }

    public ReadPage()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadChapter ();
    }

    private async void LoadChapter()
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
        string CleanedText = chapter.Content
            .Replace("<p>", "")
            .Replace("</p>", "")
            .Replace("<span>", "")
            .Replace("</span>", "");

        ChapterContentLabel.Text = CleanedText;



    }




}
