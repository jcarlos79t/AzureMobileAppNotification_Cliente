using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clientes.Clases;

namespace Clientes.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaListaClientes : ContentPage
    {
        public PaginaListaClientes()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lsvClientes.ItemsSource = await App.AzureService.ObtenerClientes();
        }

        private void lsvClientes_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Cliente cl = e.SelectedItem as Cliente;
                PaginaCliente pagina = new PaginaCliente();
                pagina.ID = cl.Id;
                Navigation.PushAsync(pagina);
            }
        }

        void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaCliente());
        }
    }
}
