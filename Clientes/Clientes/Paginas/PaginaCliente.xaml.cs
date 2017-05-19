using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clientes.Clases;

namespace Clientes.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaCliente : ContentPage
	{
		public PaginaCliente ()
		{
			InitializeComponent ();
		}

        public string ID = "";

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (ID != "")
            {
                Cliente cl = await App.AzureService.ObtenerUnCliente(ID);
                txtNombre.Text = cl.Nombre;
                txtApellido.Text = cl.Apellidos;
                dppCumple.Date = cl.FechaNacimiento;
                txtCorreo.Text = cl.Correo;
                txtTelefono.Text = cl.Celular.ToString();
                swtActivo.IsToggled = cl.Activo;
            }
        }

        void btnGuardar_Click(object sender, EventArgs a)
        {
            Cliente cl = new Cliente();
            cl.Nombre = txtNombre.Text;
            cl.Apellidos = txtApellido.Text;
            cl.FechaNacimiento  = dppCumple.Date;
            cl.Correo = txtCorreo.Text;
            cl.Celular = Convert.ToInt32(txtTelefono.Text);
            cl.Activo = swtActivo.IsToggled;

            if (ID == String.Empty)
                App.AzureService.AgregaCliente(cl);
            else
                App.AzureService.ModificaClientes(ID, cl);
            Navigation.PopAsync();
        }

        void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                App.AzureService.EliminaCliente(ID);
                Navigation.PopAsync();
            }
        }
    }
}
