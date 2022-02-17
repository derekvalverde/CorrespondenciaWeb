using INTI_PP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using INTI_PP.Herramientas;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace INTI_PP.Services
{
    public class ConsumirRepositorio : IRepositorioConsumir
    {
        private readonly AppSettings _appSettings;
        
        public ConsumirRepositorio(IOptions<AppSettings> appSettings)
        {
          
            _appSettings = appSettings.Value;
             
        }
        public clsLoginResponse obtenerLogin(clsLoginRequest login)
        {
            
           /* clsHerramienta herramienta = new clsHerramienta();*/
            clsLoginResponse loginResponse = new clsLoginResponse();
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //Api Login 
            string url = ApiIntiURL + "serviciosPPs/ApiUsuario/UsuarioLoginGeneral";


            clsLoginResponse datoscliente = new clsLoginResponse();
            var vm = new { usuLogin = login.Usuario, usuPassword = login.Contra, grpId=10};
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var respuesta = client.UploadString(new Uri(url), "POST", dataString);

                datoscliente = JsonConvert.DeserializeObject<clsLoginResponse>(respuesta);
            }
            loginResponse = datoscliente;


            return loginResponse;

        }
        //Aqui obtiene los lineas de productos buscados
        public async Task<List<clsListarProductosResponse>> obtenerProductosLineaBuscadorAsync(string cadAgeOficina, string cadlinCodigo, string cadmatCodigo, string usuID, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo Api 
            string url = ApiIntiURL + "serviciosPPs/ApiMaterial/MaterialLineaVentaStockBuscador";
            List<clsListarProductosResponse> ListBuscadorLinea = new List<clsListarProductosResponse>();
            var vm = new { matCodigo = cadmatCodigo, ageOficina = cadAgeOficina, linCodigo = cadlinCodigo, permiso=0, aplicacion="PP", cliCodigo="", like="" };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;
               
                
                ListBuscadorLinea = JsonConvert.DeserializeObject<List<clsListarProductosResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));
            }
            foreach (clsListarProductosResponse obj in ListBuscadorLinea)
            {
                obj.origen = "ProductosLinea";
                obj.usuId = usuID;
                obj.madDescuento = obj.madDescuento * -1;
            }

            return ListBuscadorLinea;
        }
        public async Task<List<clsListarProductosResponse>> obtenerProductosLineaAsync(string cadAgeOficina, string cadlinCodigo, string cadLinDes, string usuId, string cadToken)
        {
            //consumiendo Api 
            var ApiIntiURL = _appSettings.ApiIntiURL;
            string url = ApiIntiURL+ "serviciosPPs/ApiMaterial/MaterialLineaVentaStock";
            List<clsListarProductosResponse> ListProductosLinea = new List<clsListarProductosResponse>();
            var vm = new { ageOficina= cadAgeOficina, linCodigo = cadlinCodigo, permiso = 0, aplicacion="PP" };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;
                
                ListProductosLinea = JsonConvert.DeserializeObject<List<clsListarProductosResponse>>(
                        await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));
            }

            foreach (clsListarProductosResponse obj in ListProductosLinea)
            {
                obj.origen = "ProductosLinea";
                obj.usuId = usuId;
                obj.desLinea = cadLinDes;
                obj.madDescuento = obj.madDescuento * -1;
            }

            return ListProductosLinea;
        }
        //Aqui obtiene los productos buscados
        public async Task<List<clsListarProductosResponse>> obtenerProductosAsync(string cadAgeOficina, int cadUclId, string cadLike,string USUID, string cadToken, int usuId)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiMaterialVenta Stock 
            string url = ApiIntiURL + "serviciosPPs/ApiMaterial/MaterialVentaStock";
            List<clsListarProductosResponse> ListBuscador = new List<clsListarProductosResponse>();
            var vm = new { uclld = cadUclId, like = cadLike,cliCodigo= USUID, ageOficina=cadAgeOficina, usuId=usuId, aplicacion="PP", permiso=0 };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;
               
                ListBuscador = JsonConvert.DeserializeObject<List<clsListarProductosResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));
            }
            foreach (clsListarProductosResponse obj in ListBuscador)
            {
                obj.origen = "Buscador";
                obj.usuId = USUID;
                obj.madDescuento = obj.madDescuento * -1;
            }
            return ListBuscador;
        }
        //Aqui Calculamos si el material tiene descuento
        

        //Aqui Calculamos si el material tiene descuento
        public async Task<clsDescuentosResponse> calcularDescuentoAsync(clsDescuentosRequest descu, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo adicionar 
            string url = ApiIntiURL + "serviciosPPs/ApiDescuentos/Descuentos";
            clsDescuentosResponse descuentoRespuesta = new clsDescuentosResponse();


            var vm = new { cliCodigo = descu.usuId, ageOficina = descu.ageOficina, mavLinea = descu.mavLinea, mavOrigen = descu.mavOrigen, matCodigo = descu.matCodigo, canal = "40", usuId = Convert.ToInt32(descu.usuId) };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;



                descuentoRespuesta = JsonConvert.DeserializeObject<clsDescuentosResponse>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));


            }

            return descuentoRespuesta;
        }
        
        

        public async Task<List<clsLineaListarResponse>> obtenerLineasAsync(string cadCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosPPs/ApiLinea/LineaListarCanal";

            List<clsLineaListarResponse> listLinea = new List<clsLineaListarResponse>();
            var vm = new { cadCodigo= cadCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                listLinea = JsonConvert.DeserializeObject<List<clsLineaListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
           
            var ListaOrdenada = listLinea.OrderBy(o => o.linDescripcion).ToList();

            return ListaOrdenada;


        }

        


        //Aqui obtiene solicitud pedido detalle

        public async Task<List<clsPedidoSolicitudResponse>> obtenerHistorialPedidosAsync(string cadUsuID, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosPPs/ApiPedido/PedidoSolicitudUclId";

            List<clsPedidoSolicitudResponse> ListHistorial = new List<clsPedidoSolicitudResponse>();
            var vm = new { uclId = 0, usuId = Int32.Parse(cadUsuID), aplicacion = "PP" };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListHistorial=  JsonConvert.DeserializeObject<List<clsPedidoSolicitudResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }            
            foreach (clsPedidoSolicitudResponse obj in ListHistorial)
            {
                obj.usuId = cadUsuID;
            }

            var ListaOrdenada = ListHistorial.OrderByDescending(o => o.pedId).ToList();

            return ListaOrdenada;

            
        }
        //Aqui obtiene MiCuenta
        public async Task<clsClienteVentaCliCodigoResponse> obtenerMiCuentaAsync(  int usuId, string cadToken)
         {
             var ApiIntiURL = _appSettings.ApiIntiURL;
             //consumiendo Api 
           string url = ApiIntiURL + "serviciosPPs/ApiCliente/ClienteVentaCliCodigo";

             clsClienteVentaCliCodigoResponse miCuenta = new clsClienteVentaCliCodigoResponse();
             var vm = new {cliCodigo= "", usuid=usuId, aplicacion="PP" };
             using (var client = new WebClient())

             {
                 var dataString = JsonConvert.SerializeObject(vm);
                 client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                 client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                 
                 

                miCuenta = JsonConvert.DeserializeObject<clsClienteVentaCliCodigoResponse>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }

             return miCuenta;
         }
        //Aqui obtiene factura
        public List<clsFacturaClienteListarCodigoCabeceraResponse> obtenerFactura(string cadUclId, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiFactura/FacturaClienteListarCodigo";

            List<clsFacturaClienteListarCodigoCabeceraResponse> ListFactura = new List<clsFacturaClienteListarCodigoCabeceraResponse>();
            var vm = new {cliCodigo = cadUclId};
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListFactura = JsonConvert.DeserializeObject<List<clsFacturaClienteListarCodigoCabeceraResponse>>(respuesta);
            }

            return ListFactura;
        }
        public async Task<List<clsFacturaClienteListarCodigoCabeceraResponse>> obtenerFacturaAsync(string cliCodigo,string usuId, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiFactura/FacturaClienteListarCodigo";

            List<clsFacturaClienteListarCodigoCabeceraResponse> ListFactura = new List<clsFacturaClienteListarCodigoCabeceraResponse>();
            var vm = new { cliCodigo = cliCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListFactura = JsonConvert.DeserializeObject<List<clsFacturaClienteListarCodigoCabeceraResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );                
            }
            foreach (clsFacturaClienteListarCodigoCabeceraResponse obj in ListFactura)
            {
                obj.usuId = usuId;
            }

            var ListaOrdenada = ListFactura.OrderByDescending(o => o.facFecha).ToList();

            return ListaOrdenada;
          

        }
        //Aqui obtiene Token del BNB
        public clsTokenResponse obtenerTokenBNB()
        {
            //consumiendo ApiComprasListarImportancia 
            string url = "http://test.bnb.com.bo/ClientAuthentication.API/api/v1/auth/token";

            clsTokenResponse tokenBNB = new clsTokenResponse();
            clsTokenRequest datosToken = new clsTokenRequest();
            var vm = new { accountId = datosToken.accountId, authorizationId = datosToken.authorizationId };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                tokenBNB = JsonConvert.DeserializeObject<clsTokenResponse>(respuesta);
            }        
            return tokenBNB;
        }
        
      
        

        //Aqui Adiciono un pedido 
        public async Task<clsPedidoAdicionarClienteCabeceraResponse> adicionarPedidoAsync(clsPedidoAdicionarClienteCabeceraRequest pedidoAdicionar, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo adicionar
            string url = ApiIntiURL + "serviciosPPs/ApiPedido/PedidoAdicionarCliente";
            clsPedidoAdicionarClienteCabeceraResponse pedidoRegistrado = new clsPedidoAdicionarClienteCabeceraResponse();
            pedidoAdicionar.aplicacion = "PP";

            var vm = new { pedFechaEntrega = pedidoAdicionar.pedFechaEntrega, pedPrecio=pedidoAdicionar.pedPrecio, pedFechaPedido=pedidoAdicionar.pedFechaPedido, usuId=pedidoAdicionar.usuId, uclId=pedidoAdicionar.uclId, cliCodigo=pedidoAdicionar.cliCodigo, aplicacion = pedidoAdicionar.aplicacion, detallePedidoAdicionar =pedidoAdicionar.detallePedidoAdicionar };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;
               
                pedidoRegistrado = JsonConvert.DeserializeObject<clsPedidoAdicionarClienteCabeceraResponse>(
                        await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));
            }

            return pedidoRegistrado;
        }
        public async Task<clsUsuarioReseteoPasswordResponse> reseteoPasswordAsync(int usuID, string txtPassword, string txtNuevoPassword, string token)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiUsuario/UsuarioReseteoPassword";

            clsUsuarioReseteoPasswordResponse password = new clsUsuarioReseteoPasswordResponse();
            var vm = new { usuId = usuID, usuPassword = txtPassword , usuPasswordNuevo = txtNuevoPassword , aplicacion="PP"};
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;

                password = JsonConvert.DeserializeObject<clsUsuarioReseteoPasswordResponse>(
                        await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));
            }

            return password;
        }
        public async Task<clsFacturaDetalleMaterialCantidadPersonalResponse> obtenerCantComprada(int usuID, string matCodigo, string token)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiFactura/FacturaDetalleMaterialCantidadPersonal";

            clsFacturaDetalleMaterialCantidadPersonalResponse cantidadComprada = new clsFacturaDetalleMaterialCantidadPersonalResponse();
            var vm = new { usuId = usuID, matCodigo = matCodigo};
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;

                cantidadComprada = JsonConvert.DeserializeObject<clsFacturaDetalleMaterialCantidadPersonalResponse>(
                        await client.UploadStringTaskAsync(new Uri(url), "POST", dataString));

                
            }
            cantidadComprada.matCodigo= matCodigo;

            return cantidadComprada;
        }

        public List<clsClienteResponsableUbicacionUsuIdResponse> obtenerUbicacionClientes(int usuId, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiCliente/ClienteResponsableUbicacionUsuId";

            List<clsClienteResponsableUbicacionUsuIdResponse> ListUbicacionClientes = new List<clsClienteResponsableUbicacionUsuIdResponse>();
            var vm = new { usuId = usuId };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListUbicacionClientes = JsonConvert.DeserializeObject<List<clsClienteResponsableUbicacionUsuIdResponse>>(respuesta);
            }

            return ListUbicacionClientes;
        }

        public List<clsLinkSubgrupoCabeceraResponse> obtenerLink(int cadSgrId, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiLink/LinkSubGrupo";

            List<clsLinkSubgrupoCabeceraResponse> ListLink = new List<clsLinkSubgrupoCabeceraResponse>();
            var vm = new { sgrId = cadSgrId };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListLink = JsonConvert.DeserializeObject<List<clsLinkSubgrupoCabeceraResponse>>(respuesta);
            }

            return ListLink;
        }

        public List<clsClienteResponsableCategoriaGrupoResponse> obtenerVendedores(int CadUsuId, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/Cliente/ClienteResponsableCategoriaGrupo";

            List<clsClienteResponsableCategoriaGrupoResponse> ListVendedores = new List<clsClienteResponsableCategoriaGrupoResponse>();
            var vm = new { usuId = CadUsuId };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListVendedores = JsonConvert.DeserializeObject<List<clsClienteResponsableCategoriaGrupoResponse>>(respuesta);
            }

            return ListVendedores;
        }

        public List<clsClienteVisitaListarResponse> obtenerTablaVisitaClientes(int usuIdVendedor, DateTime fecIni, DateTime fecFin, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/Cliente/ClienteVisitaListar";

            List<clsClienteVisitaListarResponse> ListVendedorVisitaC = new List<clsClienteVisitaListarResponse>();
            var vm = new { usuId = usuIdVendedor, clgFecha = fecIni, clgFechaGPS= fecFin };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListVendedorVisitaC = JsonConvert.DeserializeObject<List<clsClienteVisitaListarResponse>>(respuesta);
            }

            return ListVendedorVisitaC;
        }

        public List<clsAgenciaListarResponse> obtenerListadoAgencias(string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiAgencia/AgenciaListar";


            List<clsAgenciaListarResponse> ListAgencias = new List<clsAgenciaListarResponse>();
            var vm = new { };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListAgencias = JsonConvert.DeserializeObject<List<clsAgenciaListarResponse>>(respuesta);
            }

            return ListAgencias;
        }
        public List<clsGrupoEquipoListarResponse> obtenerListadoGrupoEquipo(string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosPPs/ApiGrupoEquipo/GrupoEquipoListar";


            List<clsGrupoEquipoListarResponse> ListGrupoEquipo = new List<clsGrupoEquipoListarResponse>();
            var vm = new { };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListGrupoEquipo = JsonConvert.DeserializeObject<List<clsGrupoEquipoListarResponse>>(respuesta);
            }

            return ListGrupoEquipo;
        }

        public List<clsClienteResponsableUsuarioVendedorListarResponse> obtenerClienteResponsableVendedor(int ageId, string cagCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo Api
            string url = ApiIntiURL + "serviciosPPs/Cliente/ClienteResponsableUsuarioVendedorListar";

            List<clsClienteResponsableUsuarioVendedorListarResponse> ListVendedorCliente = new List<clsClienteResponsableUsuarioVendedorListarResponse>();
            var vm = new { ageId = ageId, cagCodigo=cagCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListVendedorCliente = JsonConvert.DeserializeObject<List<clsClienteResponsableUsuarioVendedorListarResponse>>(respuesta);
            }
            var i = 0;
            foreach (clsClienteResponsableUsuarioVendedorListarResponse obj in ListVendedorCliente)
            {
                i++;
                foreach (clsClienteResponsableUbicacionUsuarioResponse objD in obj.detalleClientes)
                {
                    objD.imagen = "../../ImagenClient/Geo" + i + ".png";
                    objD.usuNombre = obj.usuNombre;
                }
            }

            return ListVendedorCliente;
        }

        public async Task<List<clsCorrespondenciaListarResponse>> obtenerListaPaquetesAsync(string UsuCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaListar";

            List<clsCorrespondenciaListarResponse> ListPaquete = new List<clsCorrespondenciaListarResponse>();
            var vm = new { UsuCodigo = UsuCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListPaquete = JsonConvert.DeserializeObject<List<clsCorrespondenciaListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
            foreach (clsCorrespondenciaListarResponse obj in ListPaquete)
            {
                obj.usuCodigo = UsuCodigo;
            }

            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListPaquete;


        }

        public async Task<List<clsArchiveroListarResponse>> obtenerListaArchiveroAsync(string UsuCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiArchivero/ArchiveroListar";

            List<clsArchiveroListarResponse> ListArchivero = new List<clsArchiveroListarResponse>();
            var vm = new { UsuCodigo = UsuCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListArchivero = JsonConvert.DeserializeObject<List<clsArchiveroListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
            foreach (clsArchiveroListarResponse obj in ListArchivero)
            {
                obj.usuCodigo = UsuCodigo;
            }

            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListArchivero;


        }

        public List<clsCorrespondenciaDetalleInformaciónResponse> obtenerCorrespondenciaDetalleInformación(int cdeId, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaDetalleInformacion";

            List<clsCorrespondenciaDetalleInformaciónResponse> ListCorrDetalleInformacion = new List<clsCorrespondenciaDetalleInformaciónResponse>();
            var vm = new { CdeId = cdeId };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListCorrDetalleInformacion = JsonConvert.DeserializeObject<List<clsCorrespondenciaDetalleInformaciónResponse>>(respuesta);

            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListCorrDetalleInformacion;


        }

        public async Task<List<clsCorrespondenciaDetalleInformacionCelularResponse>> obtenerCorrespondenciaDetalleInformacionCelular(int cdeId, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaDetalleInformacionCelular";

            List<clsCorrespondenciaDetalleInformacionCelularResponse> ListCorresDetalleInformacionCelular = new List<clsCorrespondenciaDetalleInformacionCelularResponse>();
            var vm = new { CdeId = cdeId };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListCorresDetalleInformacionCelular = JsonConvert.DeserializeObject<List<clsCorrespondenciaDetalleInformacionCelularResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
            foreach (clsCorrespondenciaDetalleInformacionCelularResponse obj in ListCorresDetalleInformacionCelular)
            {
                obj.cdeId = cdeId;
            }

            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListCorresDetalleInformacionCelular;


        }
        public async Task<List<clsCorrespondenciaDetalleListarResponse>> obtenerCorrespondenciaDetalleListarAsync(string UsuCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaDetalleListar";

            List<clsCorrespondenciaDetalleListarResponse> ListCorrespondencia = new List<clsCorrespondenciaDetalleListarResponse>();
            var vm = new { UsuCodigo = UsuCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListCorrespondencia = JsonConvert.DeserializeObject<List<clsCorrespondenciaDetalleListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
            foreach (clsCorrespondenciaDetalleListarResponse obj in ListCorrespondencia)
            {
                obj.UsuCodigo = UsuCodigo;
            }

            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListCorrespondencia;


        }
        public List<clsCorrespondenciaDetalleBusquedaResponse> obtenerCorrespondenciaDetalleBuscador(string UsuCodigo, string codigoBusquedaCorres, string detalleBusquedaCorres, string remitenteBusquedaCorres, DateTime fechaIniBusquedaCorres, DateTime fechaFinBusquedaCorres, string CadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaDetalleBusqueda";

            List<clsCorrespondenciaDetalleBusquedaResponse> ListCorresDetalleB = new List<clsCorrespondenciaDetalleBusquedaResponse>();
            var vm = new { UsuCodigo = UsuCodigo, codigo = codigoBusquedaCorres, detalle = detalleBusquedaCorres, remitente = remitenteBusquedaCorres, fechaIni = fechaIniBusquedaCorres, fechaFin = fechaFinBusquedaCorres };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + CadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListCorresDetalleB = JsonConvert.DeserializeObject<List<clsCorrespondenciaDetalleBusquedaResponse>>(respuesta);
            }

            return ListCorresDetalleB;
        }

        public async Task<List<clsDescripcionListarResponse>> obtenerDescripcionListarAsync(string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiLineas 
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiDescripcion/DescripcionListar";


            List<clsDescripcionListarResponse> ListDescripcion = new List<clsDescripcionListarResponse>();


            using (var client = new WebClient())

            {

                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListDescripcion = JsonConvert.DeserializeObject<List<clsDescripcionListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST")
                );
            }

            return ListDescripcion;


        }

        public async Task<List<clsAreaDestinoListarResponse>> obtenerAreaDestinoListarAsync(string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiLineas 
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiArea/AreaDestinoListar";


            List<clsAreaDestinoListarResponse> ListAreaDestino = new List<clsAreaDestinoListarResponse>();


            using (var client = new WebClient())

            {

                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListAreaDestino = JsonConvert.DeserializeObject<List<clsAreaDestinoListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST")
                );
            }

            return ListAreaDestino;


        }
        public async Task<List<clsDestinatarioListarResponse>> obtenerDestinatarioListarAsync(string usuCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiLineas 
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiDestinatario/DestinatarioListar";


            List<clsDestinatarioListarResponse> ListDestinoFinal = new List<clsDestinatarioListarResponse>();

            var vm = new { usuCodigo = usuCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListDestinoFinal = JsonConvert.DeserializeObject<List<clsDestinatarioListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
            foreach (clsDestinatarioListarResponse obj in ListDestinoFinal)
            {
                obj.usuCodigo = usuCodigo;
            }

            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListDestinoFinal;


        }


        public clsArchiveroAgregarResponse RegistrarArchivero(clsArchiveroAgregarRequest obj, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiArchivero/ArchiveroAgregar";

            clsArchiveroAgregarResponse registrado = new clsArchiveroAgregarResponse();
            var vm = new { UsuCodigo = obj.UsuCodigo, Codigo = obj.Codigo, Nombre = obj.Nombre, Descripcion = obj.Descripcion, AreId = obj.AreId };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                registrado = JsonConvert.DeserializeObject<clsArchiveroAgregarResponse>(respuesta);

            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return registrado;


        }

        public clsCorrespondenciaRegistrarResponse registrarCorrespondencia(clsCorrespondenciaRegistrarRequest obj, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaRegistrar";

            clsCorrespondenciaRegistrarResponse registrado = new clsCorrespondenciaRegistrarResponse();
            var vm = new { UsuCodigo = obj.UsuCodigo, CorCodigo = obj.CorCodigo, CorNumGuia = obj.CorNumGuia, CodId = obj.CodId, AreId = obj.AreId, CorRemitente = obj.CorRemitente, CorUrgente = obj.CorUrgente };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                registrado = JsonConvert.DeserializeObject<clsCorrespondenciaRegistrarResponse>(respuesta);

            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return registrado;


        }
        public clsCorrespondenciaDetalleRegistrarResponse registrarCorresDetalle(clsCorrespondenciaDetalleRegistrarRequest obj, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaDetalleRegistrar";

            clsCorrespondenciaDetalleRegistrarResponse registradoD = new clsCorrespondenciaDetalleRegistrarResponse();
            var vm = new { UsuCodigo = obj.usuCodigo, corId = obj.corId, cdeCodigo = obj.cdeCodigo, usuFinalId = obj.usuFinalId, cdeDetalle = obj.cdeDetalle, cdeUrgente = obj.cdeUrgente };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                registradoD = JsonConvert.DeserializeObject<clsCorrespondenciaDetalleRegistrarResponse>(respuesta);

            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return registradoD;


        }
        public clsCorrespondenciaDetalleDividirResponse registrarCorresDivisionD(clsCorrespondenciaDetalleDividirRequest obj, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaDetalleDividir";

            clsCorrespondenciaDetalleDividirResponse registradoD = new clsCorrespondenciaDetalleDividirResponse();
            var vm = new { UsuCodigo = obj.usuCodigo, corId = obj.corId, cdeCodigo = obj.cdeCodigo, usuFinalId = obj.usuFinalId, cdeDetalle = obj.cdeDetalle, cdeUrgente = obj.cdeUrgente };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                registradoD = JsonConvert.DeserializeObject<clsCorrespondenciaDetalleDividirResponse>(respuesta);

            }
            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return registradoD;


        }
        public clsCorrespondenciaEliminarResponse eliminarCorrespondencia(string usuCodigo, int idPaquetes, int opcion, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaEliminar";

            clsCorrespondenciaEliminarResponse eliminado = new clsCorrespondenciaEliminarResponse();
            var vm = new { usuCodigo = usuCodigo, idCorrespondencia = idPaquetes, opcion = opcion };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                eliminado = JsonConvert.DeserializeObject<clsCorrespondenciaEliminarResponse>(respuesta);

            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return eliminado;


        }
        public clsCorrespondenciaEliminarResponse eliminarCorrespondenciaDetalle(string usuCodigo, int idCorrespondencia, int opcion, string cadToken)
        {

            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosCorrespondencia/ApiCorrespondencia/CorrespondenciaEliminar";

            clsCorrespondenciaEliminarResponse eliminado = new clsCorrespondenciaEliminarResponse();
            var vm = new { usuCodigo = usuCodigo, idCorrespondencia = idCorrespondencia, opcion = opcion };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                eliminado = JsonConvert.DeserializeObject<clsCorrespondenciaEliminarResponse>(respuesta);

            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return eliminado;
        }

        /*Activos*/
        public List<clsActivoBuscarResponse> obtenerActivoBuscador(string codigoBusquedaActivo, string detalleBusquedaActivo, string CadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiComprasListarImportancia 
            string url = ApiIntiURL + "serviciosActivos/ApiActivo/ActivoBuscar";

            List<clsActivoBuscarResponse> ListActivo = new List<clsActivoBuscarResponse>();
            var vm = new { actCodigo = codigoBusquedaActivo.Trim(), actDenomi = detalleBusquedaActivo.Trim() };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + CadToken;

                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                ListActivo = JsonConvert.DeserializeObject<List<clsActivoBuscarResponse>>(respuesta);
            }

            return ListActivo;
        }

        public async Task<List<clsActivoUbicacionUsuarioListarResponse>> obtenerListaActivosAsync(string usuID, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "serviciosActivos/ApiUbicacion/UbicacionUsuarioListar";

            List<clsActivoUbicacionUsuarioListarResponse> ListActivo = new List<clsActivoUbicacionUsuarioListarResponse>();
            var vm = new { usuId = Convert.ToInt32(usuID) };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListActivo = JsonConvert.DeserializeObject<List<clsActivoUbicacionUsuarioListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }


            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListActivo;


        }

        public async Task<List<clsActivoUbicacionHistoricoListarResponse>> obtenerActivoUbicacionHistoricoListar(string actCodigo, string cadToken)
        {
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //consumiendo ApiApiPedidoSolicitudUclId
            string url = ApiIntiURL + "ApiActivoUbicacionHistoricoListar";

            List<clsActivoUbicacionHistoricoListarResponse> ListActivoUbicacion = new List<clsActivoUbicacionHistoricoListarResponse>();
            var vm = new { actCodigo = actCodigo };
            using (var client = new WebClient())

            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;

                ListActivoUbicacion = JsonConvert.DeserializeObject<List<clsActivoUbicacionHistoricoListarResponse>>(
                    await client.UploadStringTaskAsync(new Uri(url), "POST", dataString)
                );
            }
            foreach (clsActivoUbicacionHistoricoListarResponse obj in ListActivoUbicacion)
            {
                obj.actCodigo = actCodigo;
            }

            //  var ListaOrdenada = ListCorrespondencia.OrderByDescending(o => o.usuCodigo).ToList();

            return ListActivoUbicacion;


        }

    }
}

