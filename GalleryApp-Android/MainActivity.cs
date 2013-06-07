using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using GalleryApp.Core;
using Java.IO;
using LoopJ;
using System.IO;


namespace GalleryAppAndroid
{
	[Activity (Label = "GalleryApp-Android", MainLauncher = true)]
	public class Activity1 : Activity
	{
		private readonly int PICK_IMAGE_GALLERY = 1;
		private PhotoUploader _uploader;
		private PhotoListener _listener;
		private SmartImageView imageView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);		

			SetContentView (Resource.Layout.Main);		
			Button button = FindViewById<Button> (Resource.Id.myButton);
			imageView = FindViewById<SmartImageView> (Resource.Id.image);
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

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (resultCode == Result.Ok)
			{
				var targetUri = data.Data;
				var stream = ContentResolver.OpenInputStream(targetUri);
				using(MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					byte[] photoBytes = memoryStream.ToArray();
					_uploader.UploadPhoto(photoBytes,"jpg");
				}
				
			}
		}

		private void UploadPicture ()
		{
			var galleryIntent = new Intent (Intent.ActionPick, Android.Provider.MediaStore.Images.Media.ExternalContentUri);
			StartActivityForResult(galleryIntent,PICK_IMAGE_GALLERY);			
		}
	}
}


