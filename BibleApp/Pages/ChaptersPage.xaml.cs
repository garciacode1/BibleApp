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
            BookTitleLabel.Text = "Loading chapters...";

            //Get chapters for book
            var chapters = await apiService.GetChapters(bookId);

            ChaptersCollectionView.ItemsSource = chapters;
            BookTitleLabel.Text = $"Chapters in {bookName}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load chapters: {ex.Message}", "OK");
        }


    }
  
    private async void ChaptersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //checks something was selected
        if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
            return;

        Chapter selected = (Chapter)e.CurrentSelection[0];
        //navigate from chapter to read page
        await Navigation.PushAsync(new ReadPage(selected.Id, selected.Reference));

        // remove highlight
        ((CollectionView)sender).SelectedItem = null;
    }
}