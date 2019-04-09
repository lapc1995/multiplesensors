using System;
using System.Threading.Tasks;
using Android.Content;
using MultipleSensors.Droid.Helpers;
using MultipleSensors.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(Share))]
namespace MultipleSensors.Droid.Helpers
{
    public class Share : IShare
    {
        private readonly Context _context;
        public Share()
        {
            _context = Android.App.Application.Context;
        }

        public Task Show(string title, string message, string filePath)
        {
            var extension = filePath.Substring(filePath.LastIndexOf(".", StringComparison.CurrentCulture) + 1).ToLower();
            var contentType = string.Empty;

            switch (extension)
            {
                case "csv":
                    contentType = "image/csv";
                    break;

                case "pdf":
                    contentType = "application/pdf";
                    break;

                case "png":
                    contentType = "image/png";
                    break;

                default:
                    contentType = "application/octetstream";
                    break;
            }

            var intent = new Intent(Intent.ActionSend);
            intent.SetType(contentType);
            //intent.PutExtra(Intent.ExtraStream, Uri.Parse("file://" + filePath));

            intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("file://" + filePath));
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            _context.StartActivity(chooserIntent);

            return Task.FromResult(true);
        }
    }
}
