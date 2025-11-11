using System;
using BibleApp.Services;
using Microsoft.Maui.Controls;

namespace BibleApp.Pages;

public partial class BooksPage : ContentPage
{
    APIService apiService = new APIService();

    public BooksPage()
    {
        InitializeComponent(); // This connects XAML and code-behind
        LoadBooks();            // This calls the function below when page opens
    }

    // This function will get the list of books from the API
    private async void LoadBooks()
    {
        try
        {
            // Ask the API for the books
            var books = await apiService.GetBooks();

            // Show the books in the CollectionView from the XAML file
            BooksCollectionView.ItemsSource = books;
        }
        catch (Exception ex)
        {
            // If something goes wrong, show an error message
            await DisplayAlert("Error", "Could not load books: " + ex.Message, "OK");
        }
    }

    private async void OnBookSelected(object sender, SelectionChangedEventArgs e)
    {
        // Get the book that the user tapped on
        var selectedBook = e.CurrentSelection.FirstOrDefault() as Books;

        // Make sure a book was really selected
        if (selectedBook == null)
            return;

        // Go to the ChaptersPage and send it the Book ID and Book Name
        await Navigation.PushAsync(new ChaptersPage(selectedBook.Id, selectedBook.Name));
    }


}