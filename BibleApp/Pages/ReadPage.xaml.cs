namespace BibleApp.Pages;
using BibleApp.Services;
using BibleApp.Services.Responses;
using System.Reflection.Metadata;

public partial class ReadPage : ContentPage
{

    private readonly APIService api = new();
    private readonly string chapterId;
    private readonly string chapterRef;

    public ReadPage(string chapterId, string reference)
    {
        InitializeComponent();

        this.chapterId = chapterId;
        this.chapterRef = reference;

        LoadChapter();

    }

    private async void LoadChapter()
    {
        ChapterTitleLabel.Text = "Loading chapter...";
        //api request for text
        var chapter = await api.GetChapterText(chapterId);
        //error checking if API didnt return anything
        if (chapter == null)
        {
            ChapterTitleLabel.Text = "Error to load this chapter. ";
            return;
        }
        //title of chapter
        ChapterTitleLabel.Text = chapter.Reference;
        //cleaned text from html format
        string CleanedText = chapter.Content
                                    .Replace("<p>", "")
                                    .Replace("</p>", "")
                                    .Replace("<span>", "")
                                    .Replace("</span>", "");

        ChapterContentLabel.Text = CleanedText;



    }




}
