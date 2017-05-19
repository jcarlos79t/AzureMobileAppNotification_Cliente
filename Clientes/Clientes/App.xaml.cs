using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clientes.Clases;
using Clientes.Servicio;
using Clientes.Paginas;

using Xamarin.Forms;

namespace Clientes
{
	public partial class App : Application
	{
        public static ServicioAzure AzureService;

        public App ()
		{
			InitializeComponent();

            AzureService = new ServicioAzure();            
            //MainPage = new Clientes.MainPage();
            MainPage = new NavigationPage(new PaginaListaClientes());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
