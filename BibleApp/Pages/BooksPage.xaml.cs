using System;
using BibleApp.Services;
using Microsoft.Maui.Controls;

namespace BibleApp.Pages;

public partial class BooksPage : ContentPage
{
    APIService apiService = new APIService();

    public BooksPage()
    {
        InitializeComponent(); 
        LoadBooks();           
    }

    
    private async Task LoadBooks()
    {
        try
        {
            
            var books = await apiService.GetBooks();
            BooksCollectionView.ItemsSource = books;
        }
        catch (Exception ex)
        {
            
            await DisplayAlert("Error", "Could not load books: " + ex.Message, "OK");
        }
    }

     

    private async void OnBookSelected(object sender, SelectionChangedEventArgs e)
    {
        
        var selectedBook = e.CurrentSelection.FirstOrDefault() as Books;

        if (selectedBook == null)
            return;

        await Navigation.PushAsync(new ChaptersPage(selectedBook.Id, selectedBook.Name));
    }


}