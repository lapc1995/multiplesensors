using MultipleSensors.ViewModels;
using Xamarin.Forms;

namespace MultipleSensors
{
    public partial class SensorPage : ContentPage
    {
        public SensorPage()
        {
            InitializeComponent();
        }

        void HandleItemTapped(object sender, ItemTappedEventArgs e)
        {
            var viewModel = BindingContext as SensorPageViewModel;
            viewModel.GoToDevice(e.Item);
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            var viewModel = BindingContext as SensorPageViewModel;
            return viewModel.GoBack();
        }
    }
}
