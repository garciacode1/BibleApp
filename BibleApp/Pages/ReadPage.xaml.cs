namespace BibleApp.Pages;
using BibleApp.Services;
using BibleApp.Services.Responses;
using System.Net;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadChapter ();
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



}
