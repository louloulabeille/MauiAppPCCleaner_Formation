using MauiAppPCCleaner_Formation.ViewModels;

namespace MauiAppPCCleaner_Formation;

public partial class OptionsPage : ContentPage
{
	public OptionsPage(OptionsViewModel model)
	{
		InitializeComponent();

		BindingContext = model;
	}
}