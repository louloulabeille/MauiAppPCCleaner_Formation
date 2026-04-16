using MauiAppPCCleaner_Formation.ViewModels;

namespace MauiAppPCCleaner_Formation;

public partial class RamPage : ContentPage
{
	public RamPage(RamViewModel model)
	{
		InitializeComponent();

		BindingContext = model;
    }
}