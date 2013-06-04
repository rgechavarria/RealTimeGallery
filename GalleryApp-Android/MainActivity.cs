using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalleryApp.Core;
using LoopJ;


namespace GalleryAppAndroid
{
	[Activity (Label = "GalleryApp-Android", MainLauncher = true)]
	public class Activity1 : Activity
	{
		private PhotoUploader _uploader;
		private PhotoListener _listener;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);		

			SetContentView (Resource.Layout.Main);		
			Button button = FindViewById<Button> (Resource.Id.myButton);
			SmartImageView imageView = FindViewById<SmartImageView> (Resource.Id.image);
			imageView.SetImageUrl ("http://cdn1.xamarin.com/webimages/images/index/icon-cross-platform.png");


			_uploader = new PhotoUploader ();
			_listener = new PhotoListener ();

			_listener.NewPhotosReceived += (sender, urls) =>
			{
				RunOnUiThread(() =>
				              {
					foreach (var url in urls)
					{
						imageView.SetImageUrl(url);
						var toast = Toast.MakeText(BaseContext,"New Image: "+url.ToString(),ToastLength.Long);
						toast.Show();
					}
				});
			};

			_listener.StartListening ();

			button.Click += delegate {
				UploadPicture();
			};

		}

		private void UploadPicture ()
		{
			var toast = Toast.MakeText (BaseContext, "Select from gallery not implemented yet", ToastLength.Short);
			toast.Show ();
		}
	}
}


