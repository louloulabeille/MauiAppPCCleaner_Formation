using MauiAppPCCleaner_Formation.ViewModels;

namespace MauiAppPCCleaner_Formation;

public partial class MajPage : ContentPage
{
	public MajPage(MajViewModel model)
	{
		InitializeComponent();

		BindingContext = model;
	}
}