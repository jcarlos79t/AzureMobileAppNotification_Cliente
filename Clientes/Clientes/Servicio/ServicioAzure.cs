using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Clientes.Clases;
using System.Threading.Tasks;
using System.Linq;


namespace Clientes.Servicio
{
    public class ServicioAzure
    {
        public MobileServiceClient mCliente { get; set; }

        //Definimos las tablas de azure
        IMobileServiceSyncTable<Cliente> tablaClientes;
        //IMobileServiceSyncTable<Clientes> tablaClientes;
        //IMobileServiceSyncTable<Categorias> tablaCategorias;

        bool estaInicializado;

        public async Task Initialize()
        {
            if (estaInicializado)
                return;

            mCliente = new MobileServiceClient("http://cliente-jc.azurewebsites.net");

            //definimos la db local
            const string path = "syncstore-cliente.db";
            var store = new MobileServiceSQLiteStore(path);

            //definimos tablas de sqlite
            store.DefineTable<Cliente>();
            //store.DefineTable<Clientes>();
            //store.DefineTable<Categorias>();

            await mCliente.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            tablaClientes = mCliente.GetSyncTable<Cliente>();
            estaInicializado = true;
        }

        public async Task SyncClientes()
        {
            try
            {
                await tablaClientes.PullAsync("Cliente", tablaClientes.CreateQuery()); //traer del server
                await mCliente.SyncContext.PushAsync(); // mandar todo los cambios al server
            }
            catch (Exception ex)
            {

                
            }
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            await Initialize();
            await SyncClientes();
            return await tablaClientes.OrderBy(o => o.Apellidos).ToEnumerableAsync();
        }

        public async Task<Cliente> ObtenerUnCliente(string id)
        {
            await Initialize();
            await SyncClientes();

            return (await tablaClientes.Where(s => s.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<Cliente> AgregaCliente(Cliente pCliente)
        {
            await Initialize();

            await tablaClientes.InsertAsync(pCliente);
            await SyncClientes();
            return pCliente;

        }

        public async Task<Cliente> ModificaClientes(string pId, Cliente pClientes)
        {
            await Initialize();

            try 
            {
                var item = await ObtenerUnCliente(pId);
                item.Nombre = pClientes.Nombre;
                item.Apellidos = pClientes.Apellidos;
                item.Celular = pClientes.Celular;
                item.FechaNacimiento = pClientes.FechaNacimiento;
                item.Correo = pClientes.Correo;
                item.Activo = pClientes.Activo;

                await tablaClientes.UpdateAsync(item);
                await SyncClientes();
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }

           
        }

        public async Task EliminaCliente(string pId)
        {
            await Initialize();

            var item = await ObtenerUnCliente(pId);

            await tablaClientes.DeleteAsync(item);
            await SyncClientes();
        }


    }
}
