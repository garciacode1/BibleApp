using BibleApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;


namespace BibleApp.Pages;

public partial class ChaptersPage : ContentPage
{
    private readonly APIService apiService = new();
    private readonly string bookId;
    private readonly string bookName;

    public ChaptersPage(string? selectedBookId, string? selectedBookName)
    {
        InitializeComponent();

        apiService = new APIService();
        bookId = selectedBookId ?? string.Empty;
        bookName = selectedBookName ?? "Unknown Book";

        LoadChapters();
    }

   

    private async void LoadChapters()
    {
        try
        {
            
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                BookTitleLabel.Text = $"Loading chapters in {bookName}...";
            });

            
            var chapters = await Task.Run(() => apiService.GetChapters(bookId));

            
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                ChaptersCollectionView.ItemsSource = chapters;
                BookTitleLabel.Text = $"Chapters in {bookName}";
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load chapters: {ex.Message}", "OK");
        }



    }

    private async void OnChapterSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedChapter = e.CurrentSelection.FirstOrDefault()?.ToString();
        if (selectedChapter != null)
            await DisplayAlert("Chapter selected", $"You selected chapter {selectedChapter}", "OK");
    }
















}