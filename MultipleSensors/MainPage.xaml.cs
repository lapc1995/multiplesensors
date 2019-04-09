using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace MultipleSensors
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenScanner(object sender, EventArgs e)
        {
            Scanner((Entry)((Grid)((Button)sender).Parent).Children[0]);
        }

        private async void Scanner(Entry id)
        {
            var ScannerPage = new ZXingScannerPage();
            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;
                id.Text = result.Text;
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(ScannerPage);
        }
    }
}
