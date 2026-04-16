using MauiAppPCCleaner_Formation.ViewModels;

namespace MauiAppPCCleaner_Formation;

public partial class OutilsPage : ContentPage
{
	public OutilsPage(OutilsViewModel model)
	{
		InitializeComponent();

		BindingContext = model;
	}
}