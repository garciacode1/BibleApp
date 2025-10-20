namespace BibleApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            var query = SearchEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(query))
            {
                await DisplayAlert("Search", "Type something to search.", "OK");
                return;
            }

            await DisplayAlert("Search",
                $"You typed:\n\n{query}\n\n(We'll connect this to the Bible API later.)",
                "OK");
        }

        // Go to the reading page starting at Genesis 1 (placeholder for now)
        private async void OnReadSequentiallyClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ReadPage)}?book=Genesis&chapter=1");
        }
    }
}
