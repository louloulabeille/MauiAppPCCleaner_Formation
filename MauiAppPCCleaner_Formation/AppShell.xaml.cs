using static System.Net.Mime.MediaTypeNames;

namespace MauiAppPCCleaner_Formation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Nettoyage", typeof(MainPage));
            Routing.RegisterRoute("Main", typeof(MainPage));
            Routing.RegisterRoute("Ram", typeof(RamPage));
            Routing.RegisterRoute("Outils", typeof(OutilsPage));
            Routing.RegisterRoute("Options", typeof(OptionsPage));
            Routing.RegisterRoute("Maj", typeof(MajPage));

            /// chargement de la page par défaut si elle existe
            Dispatcher.Dispatch(async () =>
            {
                string windows = Preferences.Get("DefaultWindows", "Nettoyage");
                if(windows != "Nettoyage")
                    await Shell.Current.GoToAsync(windows);
            });

        }
    }
}
