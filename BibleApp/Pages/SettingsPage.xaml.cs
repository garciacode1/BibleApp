using BibleApp.Services;

namespace BibleApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

		BibleVersionPicker.ItemsSource = BibleVersions.All;
		BibleVersionPicker.SelectedIndex = 0; //defaut bible id
	}

	private void OnBibleVersionChanged(object sender, EventArgs e)
	{
	   if (BibleVersionPicker.SelectedItem is not BibleVersion selected) return;
	
	    //store selected version Id
	   AppState.CurrentBibleId = selected.Id;
	
	}
}