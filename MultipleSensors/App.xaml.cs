using MultipleSensors.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MultipleSensors
{
    public partial class App : Application
    {
        public static INavigationService NavigationService { get; } = new NavigationService();

        public static string activity = "Test";

        public App()
        {
            InitializeComponent();

            NavigationService.Configure("MainPage", typeof(MainPage));
            NavigationService.Configure("SensorPage", typeof(SensorPage));
            NavigationService.Configure("DevicePage", typeof(DevicePage));
            NavigationService.Configure("CalibrationPage", typeof(CalibrationPage));
            NavigationService.Configure("FileListPage", typeof(FileListPage));
            NavigationService.Configure("SettingsPage", typeof(SettingsPage));
            var mainPage = ((NavigationService)NavigationService).SetRootPage("MainPage");

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
