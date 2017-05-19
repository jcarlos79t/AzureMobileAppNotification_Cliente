using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm;
using Gcm.Client;

namespace Clientes.Droid
{
	[Activity (Label = "Clientes", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{

        static MainActivity instance = null;

        public static MainActivity CurrentActivity
        {
            get { return instance; }
        }

        protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

            instance = this;

            base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new Clientes.App ());

            // notificacion
            try
            {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                System.Diagnostics.Debug.WriteLine("Registrando...");
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("Error al registrar el cliente. Verifica la URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}

