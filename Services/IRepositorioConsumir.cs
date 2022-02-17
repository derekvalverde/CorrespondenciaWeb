using INTI_PP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Services
{
    public interface IRepositorioConsumir
    {
        clsLoginResponse obtenerLogin(clsLoginRequest login);
       Task<List<clsListarProductosResponse>> obtenerProductosAsync(string cadAgeOficina, int cadUclId, string cadLike, string cliCodigo, string cadToken, int usuId);
       
        Task<List<clsPedidoSolicitudResponse>> obtenerHistorialPedidosAsync(string usuId, string cadToken);
        Task<clsClienteVentaCliCodigoResponse> obtenerMiCuentaAsync(int usuId, string cadToken);
        List<clsFacturaClienteListarCodigoCabeceraResponse> obtenerFactura(string cadUclId, string cadToken);
        Task<List<clsFacturaClienteListarCodigoCabeceraResponse>> obtenerFacturaAsync(string cadUclId,string usuId, string cadToken);
       
        Task<List<clsLineaListarResponse>> obtenerLineasAsync(string cadCodigo,string cadToken);
        
        Task<List<clsListarProductosResponse>> obtenerProductosLineaBuscadorAsync(string cadAgeOficina, string cadlinCodigo, string cadmatCodigo, string usuID, string cadToken);
        Task<List<clsListarProductosResponse>> obtenerProductosLineaAsync(string cadAgeOficina, string cadlinCodigo, string cadLinDes, string cliCodigo, string cadToken);
        Task<clsPedidoAdicionarClienteCabeceraResponse> adicionarPedidoAsync(clsPedidoAdicionarClienteCabeceraRequest pedidoAdicionar, string cadToken);
       
        Task<clsDescuentosResponse> calcularDescuentoAsync(clsDescuentosRequest descu, string cadToken);
        Task<clsUsuarioReseteoPasswordResponse> reseteoPasswordAsync(int usuID, string txtPassword, string txtNuevoPassword, string token);
        Task<clsFacturaDetalleMaterialCantidadPersonalResponse> obtenerCantComprada(int usuID, string matCodigo, string token);
        List<clsClienteResponsableUbicacionUsuIdResponse> obtenerUbicacionClientes(int usuId, string cadToken);
        List<clsLinkSubgrupoCabeceraResponse> obtenerLink(int cadSgrId, string cadToken);
        List<clsClienteResponsableCategoriaGrupoResponse> obtenerVendedores(int CadUsuId, string cadToken);
        List<clsClienteVisitaListarResponse> obtenerTablaVisitaClientes(int usuIdVendedor, DateTime fecIni, DateTime fecFin, string cadToken);
        List<clsAgenciaListarResponse> obtenerListadoAgencias(string cadToken);
        List<clsGrupoEquipoListarResponse> obtenerListadoGrupoEquipo(string cadToken);

        List<clsClienteResponsableUsuarioVendedorListarResponse> obtenerClienteResponsableVendedor(int ageId, string cagCodigo, string cadToken);


        //correspondencia
        
        Task<List<clsCorrespondenciaDetalleListarResponse>> obtenerCorrespondenciaDetalleListarAsync(string UsuCodigo, string cadToken);
        List<clsCorrespondenciaDetalleBusquedaResponse> obtenerCorrespondenciaDetalleBuscador(string UsuCodigo, string codigoBusquedaCorres, string detalleBusquedaCorres, string remitenteBusquedaCorres, DateTime fechaIniBusquedaCorres, DateTime fechaFinBusquedaCorres, string CadToken);
        Task<List<clsCorrespondenciaListarResponse>> obtenerListaPaquetesAsync(string UsuCodigo, string cadToken);
        Task<List<clsArchiveroListarResponse>> obtenerListaArchiveroAsync(string UsuCodigo, string cadToken);
        Task<List<clsDescripcionListarResponse>> obtenerDescripcionListarAsync(string cadToken);
        Task<List<clsAreaDestinoListarResponse>> obtenerAreaDestinoListarAsync(string cadToken);

        List<clsCorrespondenciaDetalleInformaciónResponse> obtenerCorrespondenciaDetalleInformación(int cdeId, string cadToken);
        Task<List<clsCorrespondenciaDetalleInformacionCelularResponse>> obtenerCorrespondenciaDetalleInformacionCelular(int cdeId, string cadToken);
        Task<List<clsDestinatarioListarResponse>> obtenerDestinatarioListarAsync(string usuCodigo, string cadToken);

        clsCorrespondenciaRegistrarResponse registrarCorrespondencia(clsCorrespondenciaRegistrarRequest obj, string cadToken);
        clsArchiveroAgregarResponse RegistrarArchivero(clsArchiveroAgregarRequest obj, string cadToken);
        clsCorrespondenciaDetalleRegistrarResponse registrarCorresDetalle(clsCorrespondenciaDetalleRegistrarRequest obj, string cadToken);
        clsCorrespondenciaDetalleDividirResponse registrarCorresDivisionD(clsCorrespondenciaDetalleDividirRequest obj, string cadToken);
        clsCorrespondenciaEliminarResponse eliminarCorrespondencia(string usuCodigo, int idCorrespondencia, int opcion, string cadToken);
        clsCorrespondenciaEliminarResponse eliminarCorrespondenciaDetalle(string usuCodigo, int idPaquetes, int opcion, string cadToken);

        //Actios
        Task<List<clsActivoUbicacionUsuarioListarResponse>> obtenerListaActivosAsync(string UsuCodigo, string cadToken);
        List<clsActivoBuscarResponse> obtenerActivoBuscador(string codigoBusquedaActivo, string detalleBusquedaActivo, string CadToken);
        Task<List<clsActivoUbicacionHistoricoListarResponse>> obtenerActivoUbicacionHistoricoListar(string actCodigo, string cadToken);


    }

}
