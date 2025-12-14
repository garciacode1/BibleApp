using BibleApp.Services;

namespace BibleApp.Pages;

public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FavoritesCollectionView.ItemsSource = null;
        FavoritesCollectionView.ItemsSource = FavoriteStore.Favorites;
    }

    private async void OnFavoriteSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var selected = (FavoriteChapter)e.CurrentSelection[0];

        await Shell.Current.GoToAsync($"{nameof(ReadPage)}" + $"?bookId={selected.BookId}" + 
            $"&chapterId={selected.ChapterId}" + $"&reference={Uri.EscapeDataString(selected.Reference)}"
        );

        ((CollectionView)sender).SelectedItem = null;



    }

}