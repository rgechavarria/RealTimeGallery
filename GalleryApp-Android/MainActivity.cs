using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalleryApp.Core;

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

			_uploader = new PhotoUploader ();
			_listener = new PhotoListener ();

			button.Click += delegate {
				UploadPicture();
			};

			_listener.NewPhotosReceived += (sender, urls) =>
			{
				RunOnUiThread(() =>
				{
					foreach (var url in urls)
					{
						var toast = Toast.MakeText(BaseContext,"New Image: "+url.ToString(),ToastLength.Long);
						toast.Show();
					}
				});
			};

			_listener.StartListening ();
		}

		private void UploadPicture ()
		{
			var toast = Toast.MakeText (BaseContext, "Select from gallery not implemented yet", ToastLength.Short);
			toast.Show ();
		}
	}
}


