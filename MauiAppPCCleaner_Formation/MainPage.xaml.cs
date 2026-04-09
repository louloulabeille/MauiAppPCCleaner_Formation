using MauiAppPCCleaner_Formation.ViewModels;

namespace MauiAppPCCleaner_Formation
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage( MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

    }
}
