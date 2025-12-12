using BibleApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Net;
namespace BibleApp.Pages;

[QueryProperty(nameof(BookId), "bookId")]
[QueryProperty(nameof(BookName), "bookName")]

public partial class ChaptersPage : ContentPage
{
    private readonly APIService apiService = new();
    public string BookId { get; set; }
    public string BookName { get; set; }

    public ChaptersPage()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    { 
       base.OnAppearing();
        LoadChapters();
    
    }

    private async void LoadChapters()
    {
        try
        {
            BookTitleLabel.Text = "Loading chapters...";

            //Get chapters for book
            var chapters = await apiService.GetChapters(BookId);

            ChaptersCollectionView.ItemsSource = chapters;
            BookTitleLabel.Text = $"Chapters in {BookName}";
        }
        catch (Exception exception)
        {
            await DisplayAlert("Error", $"Could not load chapters: {exception.Message}", "OK");
        }


    }
  
    private async void ChaptersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //checks something was selected
        if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
            return;

        Chapter selected = (Chapter)e.CurrentSelection[0];
        //navigate from chapter to read page
        await Shell.Current.GoToAsync($"{nameof(ReadPage)}?chapterId={selected.Id}&reference={selected.Reference}"
);


        // remove highlight
        ((CollectionView)sender).SelectedItem = null;
    }
}