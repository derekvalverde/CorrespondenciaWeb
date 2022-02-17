using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using INTI_PP.Models;

using Microsoft.Extensions.Caching.Memory;
using INTI_PP.Services;
using Microsoft.AspNetCore.Http;
using INTI_PP.Herramientas;
using System.IO;
using Microsoft.Extensions.Options;
using System.Text;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace INTI_PP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly AppSettings _appSettings;
        public IRepositorioConsumir _repositorio { get; }

        public HomeController(IRepositorioConsumir repositorio, IMemoryCache memoryCache, ApplicationDbContext context, ILogger<HomeController> logger, IOptions<AppSettings> appSettings)
        {
            _repositorio = repositorio;
            _memoryCache = memoryCache;
            _context = context;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Carrito()
        {
            try
            {
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                var CadToken = HttpContext.Session.GetString("SessionToken");
                if (CadToken != null && CadToken != "")
                {
                    clsPedido pedido = new clsPedido();
                    
                    var Token = HttpContext.Session.GetString("SessionToken");

                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();

                    var listadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.origen == "Buscador" && elemento.usuId == usuID).ToList();
                    var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen == "ProductosLinea" && elemento.usuId == usuID).ToList();

                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuID).ToList();

                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    pedido.LoginResponse = loginResponse;
                    pedido.ListListadoCarrito = listadoCarrito;
                    pedido.ListListadoBuscador = listadoBuscador;
                    pedido.ListListadoProductosLinea = listadoProductosLinea;
                    pedido.ListListadoLink = ListadoLink;

                    return View(pedido);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }
           
        }
        public async Task<IActionResult> Vales()
        {
            try
            {
               var usuID = HttpContext.Session.GetString("SessionUsuId");
                var token = HttpContext.Session.GetString("SessionToken");

                var cliCodigo = HttpContext.Session.GetString("SessionCliCodigo");
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                if (token != null && token != "")
                {
                    List<clsFacturaClienteListarCodigoCabeceraResponse> listadoVales = _context.ListadoFacturas.Where(elemento => elemento.usuId == usuID).ToList();

                    var detalle = await _context.ListadoFacturaDetalle.ToListAsync();

                    if (listadoVales.Count == 0)
                    {
                        clsVales vales = new clsVales();
                        var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();
                        var facturas = await _repositorio.obtenerFacturaAsync(cliCodigo, usuID, token);
                        _context.ListadoFacturas.AddRange(facturas);
                        vales.ListListadoVale = facturas;

                        _context.SaveChanges();

                        var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, token);
                        vales.ListListadoLink = ListadoLink;
                        return View(vales);
                    }
                    else
                    {
                        clsVales vales = new clsVales();
                        vales.ListListadoVale = listadoVales;
                        var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();
                        var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, token);
                        vales.ListListadoLink = ListadoLink;
                        return View(vales);
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult Seguimiento()
        {
            try
            {

                var CadToken = HttpContext.Session.GetString("SessionToken");

                if (CadToken != null && CadToken != "")
                {
                    clsSeguimiento seguimiento = new clsSeguimiento();
                    var usuID = HttpContext.Session.GetString("SessionUsuId");
                    var Token = HttpContext.Session.GetString("SessionToken");

                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();

                    
                    var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen == "ProductosLinea" && elemento.usuId == usuID).ToList();

                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuID).ToList();

                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    //Consume Listado de Vendedores
                    var ListadoVendedores = _repositorio.obtenerVendedores(Convert.ToInt32(usuID), Token);

                    seguimiento.LoginResponse = loginResponse;
                    seguimiento.ListListadoCarrito = listadoCarrito;
                    
                    seguimiento.ListListadoProductosLinea = listadoProductosLinea;
                    seguimiento.ListListadoLink = ListadoLink;
                    seguimiento.ListListadoVendedor = ListadoVendedores;
                    

                    return View(seguimiento);




                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> HistorialPedidos()
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                if (CadToken != null && CadToken != "")
                {

                    /*
                    List<clsPedidoSolicitudResponse> listadoHistorialPedidos = _context.ListadoPedidos.Where(elemento => elemento.usuId == usuID).ToList();
                    var detalle = await _context.ListadoPedidosDetalle.ToListAsync();
                    if (listadoHistorialPedidos.Count == 0)
                    {
                        var historialPedidos = await _repositorio.obtenerHistorialPedidosAsync(usuID, CadToken);
                        _context.ListadoPedidos.AddRange(historialPedidos);
                        _context.SaveChanges();
                        return View(historialPedidos);
                    }
                    else
                    {

                        return View(listadoHistorialPedidos);
                    }*/
                    clsHistorialPedido historialpedido = new clsHistorialPedido();
                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();
                    var ListadohistorialPedidos = await _repositorio.obtenerHistorialPedidosAsync(usuID, CadToken);
                    _context.ListadoPedidos.AddRange(ListadohistorialPedidos);
                    _context.SaveChanges();
                    historialpedido.ListHistorialPedido = ListadohistorialPedidos;
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, CadToken);
                    historialpedido.ListListadoLink = ListadoLink;
                    return View(historialpedido);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult CerrarSesion()
        {
            try
            {
                HttpContext.Session.SetString("SessionToken", "");
                HttpContext.Session.SetString("SessionBuscar", "");                
                var usuId = HttpContext.Session.GetString("SessionUsuId");
                var listadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.origen == "Buscador" && elemento.usuId == usuId).ToList();
                _context.LitadoGeneral.RemoveRange(listadoBuscador);
                _context.SaveChanges();
                HttpContext.Session.SetString("SessionUsuId", "");
                HttpContext.Session.SetString("SessionCliCodigo", "");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError("Error en CerrarSesion " + e);                
                return RedirectToAction(nameof(Index));
            }

        }      
           
        public IActionResult Lineas()
        {
            try
            {
                
                var usuId = HttpContext.Session.GetString("SessionUsuId");
                
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuId).ToList().Count.ToString());
                var CadToken = HttpContext.Session.GetString("SessionToken");
                if (CadToken != null && CadToken != "")
                {
                    clsPedido pedido = new clsPedido();
                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuId).FirstOrDefault();
                    var listadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.origen == "Buscador" && elemento.usuId == usuId).ToList();
                    var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen == "ProductosLinea" && elemento.usuId == usuId).ToList();

                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuId).ToList();                    
                    var listadoLineas = _context.ListadoLineas.ToList();
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, CadToken);
                    pedido.LoginResponse = loginResponse;
                    pedido.ListListadoLink = ListadoLink;
                    pedido.ListListadoCarrito = listadoCarrito;
                    pedido.ListListadoBuscador = listadoBuscador;
                    pedido.ListListadoLinea = listadoLineas;
                    pedido.ListListadoProductosLinea = listadoProductosLinea;

                    return View(pedido);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Lineas GET " + e);
                
                return RedirectToAction(nameof(Index));
            }
            

        }
        public IActionResult Pedidos()
        {
            try
            {
                
                var CadToken = HttpContext.Session.GetString("SessionToken");

                if (CadToken != null && CadToken != "")
                {
                    clsPedido pedido = new clsPedido();
                    var usuID = HttpContext.Session.GetString("SessionUsuId");
                    var Token = HttpContext.Session.GetString("SessionToken"); 
                    
                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();
                   
                    var listadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.origen== "Buscador" && elemento.usuId == usuID).ToList();
                    var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen=="ProductosLinea"&& elemento.usuId == usuID).ToList();
                    
                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuID).ToList();

                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    pedido.LoginResponse = loginResponse;
                    pedido.ListListadoCarrito = listadoCarrito;
                    pedido.ListListadoBuscador = listadoBuscador;
                    pedido.ListListadoProductosLinea = listadoProductosLinea;
                    pedido.ListListadoLink = ListadoLink;

                    return View(pedido);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            catch (Exception e)
            {
                _logger.LogError("Error en Pedidos GET" + e);
                
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Adicionar(string CodigoProdH, string NombreProdH, string PrecioProdH, int cantidad, string OrigenH, string SectorH, string MavLineaH, string fMavFamiliaH, string MavOrigenH, string madDescuentoH, string matImagenH, int cantidadMaximaH, int cantidadCompradaH, int existenciaH)
        {
            try
                
            {

                if (CodigoProdH == "400490")
                {
                    cantidad = cantidad * 10;
                }
                //_logger.LogError("PRECIO:" + PrecioProdH);
                var usuId = HttpContext.Session.GetString("SessionUsuId");
               
                clsCarrito carrito = new clsCarrito();
                carrito.usuId = usuId;
                carrito.CodigoProducto = CodigoProdH;
                
                carrito.NombreProducto = NombreProdH;
                decimal valdec = Convert.ToDecimal(PrecioProdH.Replace(",", "."), new CultureInfo("en-US"));
                
                carrito.Precio = valdec;
                //_logger.LogError("PRECIO CARRITO:" + carrito.Precio);
                carrito.Cantidad = cantidad;               
                carrito.matImagen = matImagenH;
                carrito.origen = OrigenH;
                carrito.secCodigo = SectorH;
                carrito.mavLinea = MavLineaH;
                carrito.mavFamilia = MavLineaH;
                carrito.mavOrigen = MavOrigenH;
                valdec = Convert.ToDecimal(madDescuentoH.Replace(",", "."), new CultureInfo("en-US"));
                carrito.madDescuento = valdec;
                carrito.cantidadMaxima = cantidadMaximaH;
                carrito.cantidadComprada = cantidadCompradaH;
                carrito.mavExistencia = existenciaH;

                //buscamos si el producto ya se encuentra en el carrito
                var obj = _context.ListadoCarrito.Where(a => a.CodigoProducto == CodigoProdH && a.usuId == usuId).ToList();

                //si es que no se encuentra en el carrito lo adicionamos
                if (obj.Count == 0)
                {
                     // si es de la linea z17
                    if(MavLineaH=="Z98" || (MavLineaH== "Z12" && CodigoProdH== "502222"))
                    {
                        //no se descuenta al saldoCompras SOLO ADICIONAMOS ADAICIONAMOS AL CARRITO
                        _context.ListadoCarrito.Add(carrito);
                        _context.SaveChanges();
                        clsListarProductosResponse producto = new clsListarProductosResponse();
                        producto = _context.LitadoGeneral.Where(a => a.matCodigo == CodigoProdH && a.origen == OrigenH && a.usuId == usuId).Single();
                        _context.LitadoGeneral.Remove(producto);
                        _context.SaveChanges();

                        var saldoCantidad = Convert.ToInt32(cantidadMaximaH) - Convert.ToInt32(cantidadCompradaH);
                       
                            //si el saldo saldoCantidad es mayor adicionamos 
                            if (saldoCantidad > 0)
                            {
                                cantidad = saldoCantidad;
                               
                                //  HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                               
                                carrito.Cantidad = cantidad;
                                _context.ListadoCarrito.Update(carrito);
                                _context.SaveChanges();
                            }
                            else
                            {
                                HttpContext.Session.SetString("SessionAlertaSaldoCantidad", "S");
                            }                       

                    }
                    //DESCONTAMOS AL SALDO Y A LA CANTIDAD MAXIMA
                    else
                    {
                        //obtenemos el total del pedido de 1 producto

                        var totalCobrar = (carrito.Cantidad * carrito.Precio) - ((carrito.Cantidad * carrito.Precio) * carrito.madDescuento / 100);

                        //Actulizo variable de sesion restando el totalCobrar

                        var saldoCompras = Convert.ToDecimal(HttpContext.Session.GetString("SessionSaldoCompras"));

                        var saldoCantidad = Convert.ToInt32(cantidadMaximaH) - Convert.ToInt32(cantidadCompradaH);

                        if (saldoCompras > totalCobrar)
                        {
                            if (saldoCantidad >= cantidad)
                            {
                                saldoCompras = saldoCompras - totalCobrar;
                                // HttpContext.Session.SetString("SessionAlertaSaldoCantidad", "N");
                                HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                                _context.ListadoCarrito.Add(carrito);
                                _context.SaveChanges();
                                clsListarProductosResponse producto = new clsListarProductosResponse();
                                producto = _context.LitadoGeneral.Where(a => a.matCodigo == CodigoProdH && a.origen == OrigenH && a.usuId == usuId).Single();
                                _context.LitadoGeneral.Remove(producto);
                                _context.SaveChanges();

                            }
                            else
                            {
                                //si el saldo saldoCantidad es mayor adicionamos 
                                if (saldoCantidad > 0)
                                {
                                    cantidad = saldoCantidad;
                                    totalCobrar = (cantidad * carrito.Precio) - ((cantidad * carrito.Precio) * carrito.madDescuento / 100);
                                    saldoCompras = saldoCompras - totalCobrar;
                                    //  HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                                    HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                                    carrito.Cantidad = cantidad;
                                    _context.ListadoCarrito.Update(carrito);
                                    _context.SaveChanges();
                                }
                                else
                                {
                                    HttpContext.Session.SetString("SessionAlertaSaldoCantidad", "S");
                                }



                            }


                        }
                        else
                        {
                            while (totalCobrar > saldoCompras || cantidad > saldoCantidad)
                            {
                                //INTENTAMOS REDUCIR LA CANTIDAD PEDIDA PARA PODER GUARDAR
                                cantidad = cantidad - 1;
                                totalCobrar = (cantidad * carrito.Precio) - ((cantidad * carrito.Precio) * carrito.madDescuento / 100);
                            }
                            if (cantidad > 0 && saldoCompras >= totalCobrar)
                            {
                                saldoCompras = saldoCompras - totalCobrar;
                                //HttpContext.Session.SetString("SessionAlertaSaldoCompras", "S");
                                HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                                carrito.Cantidad = cantidad;
                                _context.ListadoCarrito.Update(carrito);
                                _context.SaveChanges();
                            }
                            else
                            {
                                HttpContext.Session.SetString("SessionAlertaSaldoCompras", "S");
                            }
                        }
                    }
                                   

                }

                
                
                //Buscador   
                //ProductosLinea
                switch (carrito.origen)
                {
                    
                    case "Buscador":
                        return RedirectToAction(nameof(Pedidos));
                    case "ProductosLinea":
                        return RedirectToAction(nameof(Lineas));
                    default:
                        return RedirectToAction(nameof(Pedidos));                        
                }                
            }
            catch (Exception e)
            {
                _logger.LogError("Error al adicionar al carrito POST" + e);
                
                return RedirectToAction(nameof(Index));
            }
           
        }

        [HttpPost]
        public IActionResult Editar(string CodigoProdH,int cantidad, int cantidadMaximaH, int cantidadCompradaH)
        {
            try
            {
                if (CodigoProdH == "400490")
                {
                    cantidad = cantidad * 10;
                }
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                clsCarrito carrito = new clsCarrito();
                carrito = _context.ListadoCarrito.Where(a => a.CodigoProducto == CodigoProdH && a.usuId == usuID).Single();

                // si es de la linea z98
                if (carrito.mavLinea == "Z98" || (carrito.mavLinea == "Z12" && CodigoProdH == "502222"))
                {
                    var saldoCantidad = Convert.ToInt32(cantidadMaximaH) - Convert.ToInt32(cantidadCompradaH);
                    //no se descuenta al saldoCompras SOLO ADICIONAMOS ADAICIONAMOS AL CARRITO
                    carrito.Cantidad = cantidad;
                    _context.ListadoCarrito.Update(carrito);
                    _context.SaveChanges();
                  

                    //cantidad maxima de a molmed

                    if (saldoCantidad >= cantidad)
                    {
                       
                        //  HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                       
                        carrito.Cantidad = cantidad;
                        _context.ListadoCarrito.Update(carrito);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Carrito));
                    }
                    else
                    {
                        cantidad = saldoCantidad;
                       
                        //  HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                       
                        carrito.Cantidad = cantidad;
                        _context.ListadoCarrito.Update(carrito);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Carrito));
                    }

                }
                //DESCONTAMOS AL SALDO Y A LA CANTIDAD MAXIMA
                else
                {
                    var saldoCompras = Convert.ToDecimal(HttpContext.Session.GetString("SessionSaldoCompras"));
                    saldoCompras = saldoCompras + (carrito.Cantidad * carrito.Precio) - ((carrito.Cantidad * carrito.Precio) * carrito.madDescuento / 100);
                    var saldoCantidad = Convert.ToInt32(cantidadMaximaH) - Convert.ToInt32(cantidadCompradaH);
                    Decimal totalCobrar = 0;
                    totalCobrar = (cantidad * carrito.Precio) - ((cantidad * carrito.Precio) * carrito.madDescuento / 100);

                    if (saldoCompras > totalCobrar)
                    {
                        if (saldoCantidad >= cantidad)
                        {
                            saldoCompras = saldoCompras - totalCobrar;
                            //  HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                            HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                            carrito.Cantidad = cantidad;
                            _context.ListadoCarrito.Update(carrito);
                            _context.SaveChanges();
                            return RedirectToAction(nameof(Carrito));
                        }
                        else
                        {
                            cantidad = saldoCantidad;
                            totalCobrar = (cantidad * carrito.Precio) - ((cantidad * carrito.Precio) * carrito.madDescuento / 100);
                            saldoCompras = saldoCompras - totalCobrar;
                            //  HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                            HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                            carrito.Cantidad = cantidad;
                            _context.ListadoCarrito.Update(carrito);
                            _context.SaveChanges();
                            return RedirectToAction(nameof(Carrito));
                        }

                    }
                    else
                    {

                        while (totalCobrar > saldoCompras || cantidad > saldoCantidad)
                        {
                            //INTENTAMOS REDUCIR LA CANTIDAD PEDIDA PARA PODER GUARDAR
                            cantidad = cantidad - 1;
                            totalCobrar = (cantidad * carrito.Precio) - ((cantidad * carrito.Precio) * carrito.madDescuento / 100);
                        }

                        if (cantidad > 0 && saldoCompras >= totalCobrar)
                        {
                            saldoCompras = saldoCompras - totalCobrar;
                            HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");
                            HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                            carrito.Cantidad = cantidad;
                            _context.ListadoCarrito.Update(carrito);
                            _context.SaveChanges();

                        }
                        return RedirectToAction(nameof(Carrito));
                    }

                }




            }
            catch (Exception e)
            {
                _logger.LogError("Error al editar al carrito POST" + e);
                
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult Eliminar(string CodigoProdH)
        {
            try
            {
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                clsCarrito carrito = new clsCarrito();
                carrito = _context.ListadoCarrito.Where(a => a.CodigoProducto == CodigoProdH && a.usuId == usuID).Single();
                _context.ListadoCarrito.Remove(carrito);
                _context.SaveChanges();

                clsListarProductosResponse obj = new clsListarProductosResponse();
                obj.origen = carrito.origen;
                obj.usuId = usuID;
                obj.matCodigo = carrito.CodigoProducto;
                obj.matNombre = carrito.NombreProducto;
                obj.secCodigo = carrito.secCodigo;
                obj.mavPrecio = carrito.Precio.ToString();
                obj.mavLinea = carrito.mavLinea;
                obj.mavFamilia = carrito.mavFamilia;
                obj.mavOrigen = carrito.mavOrigen;
                obj.mavExistencia = carrito.mavExistencia;
                obj.madDescuento = carrito.madDescuento;
                obj.matImagen = carrito.matImagen;
                obj.mprCantidad = carrito.cantidadMaxima;
                obj.cantidadComprada = carrito.cantidadComprada;

                _context.LitadoGeneral.Add(obj);
                _context.SaveChanges();
                var molmed = "N";

                if (carrito.mavLinea == "Z12" && CodigoProdH == "502222")
                {
                    molmed = "S";
                }
                    if (carrito.mavLinea != "Z98" && molmed == "N")//Para la linea Z98 no reponemos el saldo porque tampoco descontamos
                {
                    var totalCobrar = (carrito.Cantidad * carrito.Precio) - ((carrito.Cantidad * carrito.Precio) * carrito.madDescuento / 100);
                    var saldoCompras = Convert.ToDecimal(HttpContext.Session.GetString("SessionSaldoCompras"));
                    saldoCompras = saldoCompras + totalCobrar;
                    HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                }                    
                return RedirectToAction(nameof(Carrito));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al eliminar al carrito POST" + e);
                
                return RedirectToAction(nameof(Index));
            }
            
        }
        

        [HttpPost]
        public async Task<IActionResult> Pedidos(clsLoginRequest model, string txtCaptcha)
        {
            try
            {
                string EncodedResponse = Request.Form["g-Recaptcha-Response"];
                bool IsCaptchaValid = (clsReCaptcha.Validate(EncodedResponse) == "true" ? true : false);
              if (!IsCaptchaValid)
              //if (IsCaptchaValid)

                    {
                    
                    HttpContext.Session.SetString("SessionErrorIngreso", "Capcha INCORRECTO");
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    HttpContext.Session.SetString("SessionToken", "");
                    HttpContext.Session.SetString("SessionBuscar", "");                    
                    HttpContext.Session.SetString("SessionUsuId", "");
                    HttpContext.Session.SetString("SessionCliCodigo", "");
                    
                    //Consume Api_ValidaUsuario
                    var login = _repositorio.obtenerLogin(model);

                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(login.sgrId, login.Token);


                    //Borro las tablas en memoria
                    _context.LoginResponse.RemoveRange(_context.LoginResponse.Where(elemento => elemento.usuId == login.usuId ).ToList());
                    _context.LitadoGeneral.RemoveRange(_context.LitadoGeneral.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList());
                    _context.ListadoLineas.RemoveRange(_context.ListadoLineas.ToList());
                    //_context.ListadoCarrito.RemoveRange(_context.ListadoCarrito.Where(elemento => elemento.cliCodigo.Equals(model.Usuario, StringComparison.InvariantCultureIgnoreCase)).ToList());
                    _context.ListadoFacturas.RemoveRange(_context.ListadoFacturas.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList());                    
                    _context.ListadoPedidos.RemoveRange(_context.ListadoPedidos.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList());                    

                    _context.SaveChanges();

                    clsPedido pedido = new clsPedido();

                   

                    pedido.LoginResponse = login;
                    pedido.ListListadoLink = ListadoLink;



                    List<clsLineaListarResponse> lineas = new List<clsLineaListarResponse>();

                    List<clsFacturaClienteListarCodigoCabeceraResponse> facturas = new List<clsFacturaClienteListarCodigoCabeceraResponse>();
                    List<clsPedidoSolicitudResponse> historialPedidos = new List<clsPedidoSolicitudResponse>();
                    
                    
                    //guardo saldo de la compra
                    var miCuenta =await _repositorio.obtenerMiCuentaAsync(login.usuId, login.Token);

                    var ListCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList();
                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == login.usuId.ToString()).ToList().Count.ToString());
                    Decimal Subtotal = 0;
                    Decimal descuento = 0;
                   
                    Decimal totalSubtotal = 0;


                    foreach (clsCarrito obj in ListCarrito)
                    {
                        Subtotal =obj.Cantidad * obj.Precio;

                        if (obj.madDescuento > 0)
                        {
                            descuento = Subtotal * Convert.ToDecimal(obj.madDescuento / 100);
                            descuento = Decimal.Round(descuento, 2);
                            
                        }
                        else
                        {
                            descuento = 0;
                            
                        }
                        Subtotal = Subtotal - descuento;
                        totalSubtotal = totalSubtotal + Subtotal;

                       
                    }

                    var saldoCompras = miCuenta.perLimite - miCuenta.perComprado - totalSubtotal;
                    




                    //Seteamos objetos vacios para la vista de pedidos
                    pedido.ListListadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList();
                    pedido.ListListadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList();
                    pedido.ListListadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.usuId == login.usuId.ToString()).ToList();

                    //Guardamos en variables de sesion
                    HttpContext.Session.SetString("SessionToken", login.Token);
                    HttpContext.Session.SetString("SessionUsuario", login.uscId.ToString());
                    HttpContext.Session.SetString("SessionUsuId", login.usuId.ToString());
                    HttpContext.Session.SetString("SessionRazonSocial", login.usuNombre);
                    HttpContext.Session.SetString("SessionAgeOficina", login.ageOficina);
                    HttpContext.Session.SetString("SessionPassword", model.Contra);
                    HttpContext.Session.SetString("SessionSaldoCompras", saldoCompras.ToString("0.00").TrimEnd('0').TrimEnd('.'));
                    HttpContext.Session.SetString("SessionAlertaSaldoCompras", "N");                    
                    HttpContext.Session.SetString("SessionAlertaSaldoCantidad", "N");
                    HttpContext.Session.SetString("SessionCargarModales", "S");
                    HttpContext.Session.SetString("SessionCliCodigo", miCuenta.cliCodigo);
                    HttpContext.Session.SetString("SessionEmpCodigo", login.usuCodigo.ToString());

                    //HttpContext.Session.SetString("SessionImagenes", PaginaImagenes);

                    HttpContext.Session.SetString("SessionNroPedido", "0");
                    HttpContext.Session.SetString("SessionCorresRegistrada", "0");
                    HttpContext.Session.SetString("SessionCorresDRegistrada", "0");

                    //Guardamos en Base de datos
                    _context.LoginResponse.Add(pedido.LoginResponse);
                    _context.MiCuenta.Add(miCuenta);
                    _context.ListadoLineas.AddRange(lineas);
                    _context.ListadoFacturas.AddRange(facturas);
                    _context.ListadoPedidos.AddRange(historialPedidos);
                    _context.SaveChanges();

                    /*BORRO LA VARIABLE DE SESION DE ERROR DE INGRESAR*/
                    HttpContext.Session.SetString("SessionErrorIngreso", "");
                    
                    return View(pedido);
                    //   });
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error al cargar Pedidos POST(consumiendo APIS) " + e);
                HttpContext.Session.SetString("SessionErrorIngreso", "Usuario o Contraseña INCORRECTOS");
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Buscar(string txtProducto)
        {
            try
            {
                if (txtProducto != null)
                {
                    
                    var CadToken = HttpContext.Session.GetString("SessionToken");
                    var uclId = Convert.ToInt32(HttpContext.Session.GetString("SessionUsuId"));
                    var usuId_ = HttpContext.Session.GetString("SessionUsuId");
                    var ageOficina = HttpContext.Session.GetString("SessionAgeOficina");
                    var usuId = HttpContext.Session.GetString("SessionUsuId");
                    HttpContext.Session.SetString("SessionBuscar", txtProducto);

                    var listadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.origen == "Buscador" && elemento.usuId== usuId).ToList();
                    _context.LitadoGeneral.RemoveRange(listadoBuscador);
                    var listaBuscadorCandidato = await _repositorio.obtenerProductosAsync(ageOficina, uclId, txtProducto, usuId, CadToken, Int32.Parse(usuId));
                    List<clsListarProductosResponse> listaBuscador = new List<clsListarProductosResponse>();
                   
                    

                    //veo si en el carrito ya esta el producto buscado
                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuId).ToList();
                    for (int i = 0; i < listaBuscadorCandidato.Count; i = i + 1)
                    {
                        var estaEnCarrito = "N";
                        for (int j = 0; j < listadoCarrito.Count; j = j + 1)
                        {
                            if (listaBuscadorCandidato.ElementAt(i).matCodigo == listadoCarrito.ElementAt(j).CodigoProducto)
                            {
                                estaEnCarrito = "S";
                                j = listadoCarrito.Count;
                            }
                        }
                        //
                        if (estaEnCarrito == "N")
                        {
                            //aqui obtenemos la cantidad comprada y adicionamos a la lista

                           
                            //adicionamos casos especiales para la cantidad maxima mprcantidad
                            if (  listaBuscadorCandidato.ElementAt(i).usuId=="209"||
                                listaBuscadorCandidato.ElementAt(i).usuId == "210" ||
                                listaBuscadorCandidato.ElementAt(i).usuId == "32" ||
                                 listaBuscadorCandidato.ElementAt(i).usuId == "42" ||
                                listaBuscadorCandidato.ElementAt(i).usuId == "330" ||
                                    listaBuscadorCandidato.ElementAt(i).usuId == "2518"
                                )

                            {
                                listaBuscadorCandidato.ElementAt(i).mprCantidad = listaBuscadorCandidato.ElementAt(i).mprCantidad + 6;
                            }
                           
                            listaBuscador.Add(listaBuscadorCandidato.ElementAt(i));
                        }


                    }

                    /*******************/

                    //aqui obtenemos la cantidad comprada
                    /*
                    List<Task> TListBL = new List<Task>();
                    for (int i = 0; i < listaBuscador.Count; i = i + 1)
                    {
                        var tarea = _repositorio.obtenerCantComprada(Convert.ToInt32(usuId), listaBuscador.ElementAt(i).matCodigo, CadToken);

                        TListBL.Add(tarea);
                    }
                    await Task.WhenAll(TListBL.ToArray());
                    for (int i = 0; i < listaBuscador.Count; i = i + 1)
                    {
                        foreach (Task<clsFacturaDetalleMaterialCantidadPersonalResponse> t in TListBL)
                        {
                            var resultado = t.Result;

                            if (listaBuscador.ElementAt(i).matCodigo == resultado.matCodigo)
                            {
                                listaBuscador.ElementAt(i).cantidadComprada = resultado.cantidad;
                                break;
                            }

                        }

                    }
                    /*************************/

                    _context.LitadoGeneral.AddRange(listaBuscador);
                    _context.SaveChanges();


                }
                else
                {
                    HttpContext.Session.SetString("SessionBuscar", "");
                    var usuId = HttpContext.Session.GetString("SessionUsuId");
                    var listadoBuscador = _context.LitadoGeneral.Where(elemento => elemento.origen == "Buscador" && elemento.usuId == usuId).ToList();
                    _context.LitadoGeneral.RemoveRange(listadoBuscador);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Pedidos));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al buscar un producto POST" + e);
                Console.WriteLine("{0} Exception caught.", e);
                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> LineasProd(string linCodigo, string linDescripcion)
        {
            try
            {
                HttpContext.Session.SetString("SessionLinea", linDescripcion);
                HttpContext.Session.SetString("SessionCodLinea", linCodigo);
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                var ageOficina = HttpContext.Session.GetString("SessionAgeOficina");
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var listaBuscadorCandidato = await _repositorio.obtenerProductosLineaAsync(ageOficina, linCodigo, linDescripcion, usuID, CadToken);

                List<clsListarProductosResponse> listadoBuscador = new List<clsListarProductosResponse>();
                //veo si en el carrito ya esta el producto buscado
                var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuID).ToList();
                for (int i = 0; i < listaBuscadorCandidato.Count; i = i + 1)
                {
                    var estaEnCarrito = "N";
                    for (int j = 0; j < listadoCarrito.Count; j = j + 1)
                    {
                        if (listaBuscadorCandidato.ElementAt(i).matCodigo == listadoCarrito.ElementAt(j).CodigoProducto)
                        {
                            estaEnCarrito = "S";
                            j = listadoCarrito.Count;
                        }
                    }
                    //
                    if (estaEnCarrito == "N")
                    {
                        //aqui obtenemos la cantidad comprada y adicionamos a la lista

                       //listaBuscadorCandidato.ElementAt(i).cantidadComprada = _repositorio.obtenerCantComprada(Convert.ToInt32(usuID), listaBuscadorCandidato.ElementAt(i).matCodigo, CadToken);
                        //adicionamos casos especiales para la cantidad maxima mprcantidad
                        if (listaBuscadorCandidato.ElementAt(i).usuId == "209" ||
                            listaBuscadorCandidato.ElementAt(i).usuId == "210" ||
                            listaBuscadorCandidato.ElementAt(i).usuId == "32" ||
                            listaBuscadorCandidato.ElementAt(i).usuId == "330" ||
                            listaBuscadorCandidato.ElementAt(i).usuId == "42" ||
                                    listaBuscadorCandidato.ElementAt(i).usuId == "2518"
                            )
                        {
                            listaBuscadorCandidato.ElementAt(i).mprCantidad = listaBuscadorCandidato.ElementAt(i).mprCantidad + 6;
                        }
                        listadoBuscador.Add(listaBuscadorCandidato.ElementAt(i));
                    }


                }

                //aqui obtenemos la cantidad comprada
                /*
                List<Task> TListBL = new List<Task>();
                for (int i = 0; i < listadoBuscador.Count; i = i + 1)
                {
                    var tarea = _repositorio.obtenerCantComprada(Convert.ToInt32(usuID), listadoBuscador.ElementAt(i).matCodigo, CadToken);
                    
                    TListBL.Add(tarea);
                }
                await Task.WhenAll(TListBL.ToArray());
                for (int i = 0; i < listadoBuscador.Count; i = i + 1)
                {
                    foreach (Task<clsFacturaDetalleMaterialCantidadPersonalResponse> t in TListBL)
                    {
                        var resultado = t.Result;
                        
                        if (listadoBuscador.ElementAt(i).matCodigo == resultado.matCodigo)
                        {
                            listadoBuscador.ElementAt(i).cantidadComprada = resultado.cantidad;
                            break;
                        }

                    }

                }

                //**********************/


                _context.LitadoGeneral.AddRange(listadoBuscador);                
                var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen == "ProductosLinea" && elemento.usuId == usuID).ToList();
                _context.LitadoGeneral.RemoveRange(listadoProductosLinea);
                
                _context.SaveChanges();
                return RedirectToAction(nameof(Lineas));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al LineasProd POST " + e);

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> BuscarLineas(string codLinea, string txtLikeLinea)
        {
            try
            {
                if (txtLikeLinea != null)
                {
                    var usuID = HttpContext.Session.GetString("SessionUsuId");
                    var ageOficina = HttpContext.Session.GetString("SessionAgeOficina");
                    var CadToken = HttpContext.Session.GetString("SessionToken");
                    
                    var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen.Equals("ProductosLinea", StringComparison.InvariantCultureIgnoreCase) && elemento.usuId == usuID).ToList();
                    _context.LitadoGeneral.RemoveRange(listadoProductosLinea);
                    var listaBuscadorCandidato =await _repositorio.obtenerProductosLineaBuscadorAsync(ageOficina, codLinea, txtLikeLinea, usuID, CadToken);

                    List<clsListarProductosResponse> listaBuscadorLineaLike = new List<clsListarProductosResponse>();



                    //veo si en el carrito ya esta el producto buscado
                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuID).ToList();
                    for (int i = 0; i < listaBuscadorCandidato.Count; i = i + 1)
                    {
                        var estaEnCarrito = "N";
                        for (int j = 0; j < listadoCarrito.Count; j = j + 1)
                        {
                            if (listaBuscadorCandidato.ElementAt(i).matCodigo == listadoCarrito.ElementAt(j).CodigoProducto)
                            {
                                estaEnCarrito = "S";
                                j = listadoCarrito.Count;
                            }
                        }
                        //
                        if (estaEnCarrito == "N")
                        {
                            //aqui obtenemos la cantidad comprada y adicionamos a la lista

                            //listaBuscadorCandidato.ElementAt(i).cantidadComprada = _repositorio.obtenerCantComprada(Convert.ToInt32(usuID), listaBuscadorCandidato.ElementAt(i).matCodigo, CadToken);
                            //adicionamos casos especiales para la cantidad maxima mprcantidad
                            if (listaBuscadorCandidato.ElementAt(i).usuId == "209" ||
                                listaBuscadorCandidato.ElementAt(i).usuId == "210" ||
                                listaBuscadorCandidato.ElementAt(i).usuId == "32" ||                               
                                listaBuscadorCandidato.ElementAt(i).usuId == "330" ||
                                  listaBuscadorCandidato.ElementAt(i).usuId == "42" ||
                                    listaBuscadorCandidato.ElementAt(i).usuId == "2518"
                                )
                            {
                                listaBuscadorCandidato.ElementAt(i).mprCantidad = listaBuscadorCandidato.ElementAt(i).mprCantidad + 6;
                            }

                            listaBuscadorLineaLike.Add(listaBuscadorCandidato.ElementAt(i));
                        }


                    }
                    /*******************/

                    //aqui obtenemos la cantidad comprada
                    /*
                    List<Task> TListBL = new List<Task>();
                    for (int i = 0; i < listaBuscadorLineaLike.Count; i = i + 1)
                    {
                        var tarea = _repositorio.obtenerCantComprada(Convert.ToInt32(usuID), listaBuscadorLineaLike.ElementAt(i).matCodigo, CadToken);

                        TListBL.Add(tarea);
                    }
                    await Task.WhenAll(TListBL.ToArray());
                    for (int i = 0; i < listaBuscadorLineaLike.Count; i = i + 1)
                    {
                        foreach (Task<clsFacturaDetalleMaterialCantidadPersonalResponse> t in TListBL)
                        {
                            var resultado = t.Result;

                            if (listaBuscadorLineaLike.ElementAt(i).matCodigo == resultado.matCodigo)
                            {
                                listaBuscadorLineaLike.ElementAt(i).cantidadComprada = resultado.cantidad;
                                break;
                            }

                        }

                    }
                    /*************************/

                    _context.LitadoGeneral.AddRange(listaBuscadorLineaLike);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Lineas));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al BuscarLineas un producto POST" + e);
                Console.WriteLine("{0} Exception caught.", e);
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> confirmarCompra()
        {
            try
            {
                clsPedidoAdicionarClienteCabeceraRequest pedidoAdicionar = new clsPedidoAdicionarClienteCabeceraRequest();

                var usuId = HttpContext.Session.GetString("SessionUsuId");
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var cliCodigo = HttpContext.Session.GetString("SessionCliCodigo");

                List<clsCarrito> carrito = new List<clsCarrito>();
               // sacamos los que no son mwchardansing
                carrito = _context.ListadoCarrito.Where(a => a.usuId == usuId && a.mavLinea != "Z98").ToList();


                //para sacar el total del pedido
                decimal Subtotal = 0;
                decimal totalSubtotal = 0;
                decimal descuento = 0;
                decimal totalDescuento = 0;
                decimal descuentoUnitario = 0;
                List<clsPedidoAdicionarClienteDetalleRequest> detallePedidoAdicionar = new List<clsPedidoAdicionarClienteDetalleRequest>();
                for (int i = 0; i < carrito.Count; i = i + 1)
                {
                    Subtotal = carrito.ElementAt(i).Cantidad * carrito.ElementAt(i).Precio;
                    totalSubtotal = totalSubtotal + Subtotal;
                    clsPedidoAdicionarClienteDetalleRequest pedidoAdicionarProducto = new clsPedidoAdicionarClienteDetalleRequest();
                    if (carrito.ElementAt(i).madDescuento > 0)
                    {
                        descuentoUnitario = carrito.ElementAt(i).Precio * Convert.ToDecimal(carrito.ElementAt(i).madDescuento / 100);
                        descuento = Subtotal * Convert.ToDecimal(carrito.ElementAt(i).madDescuento / 100);
                        descuento = Decimal.Round(descuento, 2);
                        totalDescuento = totalDescuento + descuento;
                    }
                    else
                    {
                        totalDescuento = 0;
                    }
                    pedidoAdicionarProducto.matCodigo = carrito.ElementAt(i).CodigoProducto;
                    pedidoAdicionarProducto.pddCantidad = carrito.ElementAt(i).Cantidad;
                    pedidoAdicionarProducto.pddPrecio = carrito.ElementAt(i).Precio - descuentoUnitario;
                    pedidoAdicionarProducto.pddPrecioVenta = carrito.ElementAt(i).Precio;
                    detallePedidoAdicionar.Add(pedidoAdicionarProducto);
                }
                if (carrito.Count > 0)
                {
                    pedidoAdicionar.pedFechaEntrega = DateTime.Now;
                    pedidoAdicionar.pedPrecio = totalSubtotal - totalDescuento;
                    pedidoAdicionar.pedFechaPedido = DateTime.Now;
                    pedidoAdicionar.usuId = Convert.ToInt32(usuId);
                    pedidoAdicionar.uclId = Convert.ToInt32(usuId);
                    pedidoAdicionar.cliCodigo = cliCodigo;
                    pedidoAdicionar.detallePedidoAdicionar = detallePedidoAdicionar;
                    var respuesta = await _repositorio.adicionarPedidoAsync(pedidoAdicionar, CadToken);
                    HttpContext.Session.SetString("SessionNroPedido", respuesta.pedId.ToString());
                   
                    if (respuesta.pedId > 0)
                    {
                        _context.ListadoCarrito.RemoveRange(_context.ListadoCarrito.Where(elemento => elemento.usuId == usuId && elemento.mavLinea != "Z98").ToList());
                        _context.SaveChanges();
                    }
                }
                //borro el carrito
               

                //Consulto si en el carrito esta al menos un producto de la linea Z98
                List<clsCarrito> carritoVE = new List<clsCarrito>();
                carritoVE = _context.ListadoCarrito.Where(a => a.usuId == usuId && a.mavLinea == "Z98").ToList();

                if (carritoVE.Count > 0)
                {
                    //creamos un pedido para una linea especial
                    clsPedidoAdicionarClienteCabeceraRequest pedidoAdicionarE = new clsPedidoAdicionarClienteCabeceraRequest();

                    var usuIdE = HttpContext.Session.GetString("SessionUsuId");
                    var CadTokenE = HttpContext.Session.GetString("SessionToken");
                    var cliCodigoE = HttpContext.Session.GetString("SessionCliCodigo");

                    List<clsCarrito> carritoE = new List<clsCarrito>();
                    carritoE = _context.ListadoCarrito.Where(a => a.usuId == usuIdE && a.mavLinea == "Z98").ToList();


                    //para sacar el total del pedido
                    decimal SubtotalE = 0;
                    decimal totalSubtotalE = 0;
                    decimal descuentoE = 0;
                    decimal totalDescuentoE = 0;
                    decimal descuentoUnitarioE = 0;
                    List<clsPedidoAdicionarClienteDetalleRequest> detallePedidoAdicionarE = new List<clsPedidoAdicionarClienteDetalleRequest>();
                    for (int i = 0; i < carritoE.Count; i = i + 1)
                    {
                        SubtotalE = carritoE.ElementAt(i).Cantidad * carritoE.ElementAt(i).Precio;
                        totalSubtotalE = totalSubtotalE + SubtotalE;
                        clsPedidoAdicionarClienteDetalleRequest pedidoAdicionarProductoE = new clsPedidoAdicionarClienteDetalleRequest();
                        if (carritoE.ElementAt(i).madDescuento > 0)
                        {
                            descuentoUnitarioE = carritoE.ElementAt(i).Precio * Convert.ToDecimal(carritoE.ElementAt(i).madDescuento / 100);
                            descuentoE = SubtotalE * Convert.ToDecimal(carritoE.ElementAt(i).madDescuento / 100);
                            descuentoE = Decimal.Round(descuentoE, 2);
                            totalDescuentoE = totalDescuentoE + descuentoE;
                        }
                        else
                        {
                            totalDescuentoE = 0;
                        }
                        pedidoAdicionarProductoE.matCodigo = carritoE.ElementAt(i).CodigoProducto;
                        pedidoAdicionarProductoE.pddCantidad = carritoE.ElementAt(i).Cantidad;
                        pedidoAdicionarProductoE.pddPrecio = carritoE.ElementAt(i).Precio - descuentoUnitarioE;
                        pedidoAdicionarProductoE.pddPrecioVenta = carritoE.ElementAt(i).Precio;
                        detallePedidoAdicionarE.Add(pedidoAdicionarProductoE);
                    }
                    pedidoAdicionarE.pedFechaEntrega = DateTime.Now;
                    pedidoAdicionarE.pedPrecio = totalSubtotalE - totalDescuentoE;
                    pedidoAdicionarE.pedFechaPedido = DateTime.Now;
                    pedidoAdicionarE.usuId = Convert.ToInt32(usuIdE);
                    pedidoAdicionarE.uclId = Convert.ToInt32(usuIdE);
                    pedidoAdicionarE.cliCodigo = cliCodigoE;
                    pedidoAdicionarE.detallePedidoAdicionar = detallePedidoAdicionarE;
                    var respuestaE = await _repositorio.adicionarPedidoAsync(pedidoAdicionarE, CadTokenE);
                    HttpContext.Session.SetString("SessionNroPedido", respuestaE.pedId.ToString());
                    //borro el carrito Especial
                    if (respuestaE.pedId > 0)
                    {
                        _context.ListadoCarrito.RemoveRange(_context.ListadoCarrito.Where(elemento => elemento.usuId == usuIdE && elemento.mavLinea == "Z98").ToList());
                        _context.SaveChanges();
                    }
                }
                

                return RedirectToAction(nameof(Carrito));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al eliminar al carrito POST" + e);

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<int> CambioPassword(string txtPassword, string txtNuevoPassword)
        {
            try
            {
                var usuID = Int32.Parse(HttpContext.Session.GetString("SessionUsuId"));
               
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var nuevoPassword =await _repositorio.reseteoPasswordAsync(usuID, txtPassword, txtNuevoPassword, CadToken);
                if (nuevoPassword.sw > 0)
                {                    
                   HttpContext.Session.SetString("SessionPassword", txtNuevoPassword);
                }
                return nuevoPassword.sw;
                
            }
            catch (Exception e)
            {
                _logger.LogError("Error al cAMBIAR PASS POST " + e);

                return 0;
            }

        }

        [HttpPost]
        public async Task<int> ObtenerCantidadComprada(string matCodigo)
        {
            try
            {
                var usuID = Int32.Parse(HttpContext.Session.GetString("SessionUsuId"));

                var CadToken = HttpContext.Session.GetString("SessionToken");

                var respuesta = await _repositorio.obtenerCantComprada(Convert.ToInt32(usuID), matCodigo, CadToken);
                
                
                return respuesta.cantidad;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);

                return 0;
            }

        }

        [HttpPost]
        public List<clsClienteResponsableUbicacionUsuIdResponse> ObtenerUbicacionClientes()
        {
            try
            {
                var usuID = Int32.Parse(HttpContext.Session.GetString("SessionUsuId"));

                var CadToken = HttpContext.Session.GetString("SessionToken");

                var ListaClienteUbicacion = _repositorio.obtenerUbicacionClientes(usuID, CadToken);


                return ListaClienteUbicacion;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);
                List<clsClienteResponsableUbicacionUsuIdResponse> res = new List<clsClienteResponsableUbicacionUsuIdResponse>();
                return res;
            }

        }

        [HttpPost]
        public List<clsClienteVisitaListarResponse> ObtenerVendedorVisitaClientes(int usuIdVendedor, DateTime fechaInicio, DateTime fechaFin )
        {
            try
            { 
                var CadToken = HttpContext.Session.GetString("SessionToken");

                var ListaVendedorVisitaC = _repositorio.obtenerTablaVisitaClientes(usuIdVendedor, fechaInicio, fechaFin, CadToken);


                return ListaVendedorVisitaC;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);
                List<clsClienteVisitaListarResponse> res = new List<clsClienteVisitaListarResponse>();
                return res;
            }

        }

        [HttpPost]
        public List<clsClienteResponsableUbicacionUsuarioResponse> ObtenerGeolocalizacionEquipos(int idAgencia, string idEquipo)
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");

                List<clsClienteResponsableUsuarioVendedorListarResponse> ListaClienteResponsableUsuarioVendedor = _repositorio.obtenerClienteResponsableVendedor(idAgencia, idEquipo,CadToken);

                List<clsClienteResponsableUbicacionUsuarioResponse> ListaUbicacionesPorVendedor = new List<clsClienteResponsableUbicacionUsuarioResponse>();
                foreach (clsClienteResponsableUsuarioVendedorListarResponse obj in ListaClienteResponsableUsuarioVendedor)
                {                    
                    foreach (clsClienteResponsableUbicacionUsuarioResponse objD in obj.detalleClientes)
                    {
                        ListaUbicacionesPorVendedor.Add(objD);
                    }
                }


                return ListaUbicacionesPorVendedor;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener Cliente responsable vendedor " + e);
                List<clsClienteResponsableUbicacionUsuarioResponse> res = new List<clsClienteResponsableUbicacionUsuarioResponse>();
                return res;
            }

        }

        public IActionResult GeolocalizacionEquipos()
        {
            try
            {

                var usuID = HttpContext.Session.GetString("SessionUsuId");
                var token = HttpContext.Session.GetString("SessionToken");

                var cliCodigo = HttpContext.Session.GetString("SessionCliCodigo");

                if (token != null && token != "")
                {
                    clsSeguimiento geolocalizacion = new clsSeguimiento();
                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();
                    geolocalizacion.LoginResponse = loginResponse;
                    var listAgencias = _repositorio.obtenerListadoAgencias(token);
                    var listGrupoEquipo = _repositorio.obtenerListadoGrupoEquipo(token);

                    geolocalizacion.ListListadoAgencias = listAgencias;
                    geolocalizacion.ListListadoGrupoEquipo = listGrupoEquipo;

                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, token);
                    geolocalizacion.ListListadoLink = ListadoLink;
                    return View(geolocalizacion);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }

        

        //************correspondencia
        [HttpPost]
        public List<clsCorrespondenciaDetalleInformaciónResponse> obtenerCorrespondenciaDetalleInformación(int idCorrespondencia)
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");

                var ListaCorresDetalleInformacion = _repositorio.obtenerCorrespondenciaDetalleInformación(idCorrespondencia, CadToken);
                List<clsCorrespondenciaDetalleInformaciónResponse> obj = new List<clsCorrespondenciaDetalleInformaciónResponse>();
                clsCorrespondenciaDetalleInformaciónResponse obj1 = new clsCorrespondenciaDetalleInformaciónResponse();

                obj.Add(obj1);
                return ListaCorresDetalleInformacion;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);
                List<clsCorrespondenciaDetalleInformaciónResponse> res = new List<clsCorrespondenciaDetalleInformaciónResponse>();
                return res;
            }

        }

        [HttpPost]
        public async Task<List<clsCorrespondenciaDetalleInformacionCelularResponse>> ObtenerCorrespondenciaDetalleInformacionCelular(int idCorrespondencia)
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");

                var ListaCorresDetalleInformacionCelular = await _repositorio.obtenerCorrespondenciaDetalleInformacionCelular(idCorrespondencia, CadToken);

                return ListaCorresDetalleInformacionCelular;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);
                List<clsCorrespondenciaDetalleInformacionCelularResponse> res = new List<clsCorrespondenciaDetalleInformacionCelularResponse>();
                return res;
            }

        }
        public async Task<IActionResult> CorrespondenciasRecibidas()
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                var divId = HttpContext.Session.GetString("SessionDivId");

                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                if (CadToken != null && CadToken != "")
                {
                    clsCorrespondencia listarCorrespondencia = new clsCorrespondencia();


                    var Token = HttpContext.Session.GetString("SessionToken");

                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();


                    var listadoProductosLinea = _context.LitadoGeneral.Where(elemento => elemento.origen == "ProductosLinea" && elemento.usuId == usuID).ToList();

                    var listadoCarrito = _context.ListadoCarrito.Where(elemento => elemento.usuId == usuID).ToList();

                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    //Consume Listado de correspondencia
                    var ListadoCorrespondencia = await _repositorio.obtenerCorrespondenciaDetalleListarAsync(empCodigo, Token);

                    listarCorrespondencia.LoginResponse = loginResponse;
                    listarCorrespondencia.ListListadoCarrito = listadoCarrito;

                    listarCorrespondencia.ListListadoProductosLinea = listadoProductosLinea;
                    listarCorrespondencia.ListListadoLink = ListadoLink;
                    listarCorrespondencia.ListListadoCorrespondencia = ListadoCorrespondencia;

                    return View(listarCorrespondencia);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public List<clsCorrespondenciaDetalleBusquedaResponse> CorrespondenciaDetalleBuscador(string codigoBusquedaCorres, string detalleBusquedaCorres, string remitenteBusquedaCorres, DateTime fechaIniBusquedaCorres, DateTime fechaFinBusquedaCorres)
        {
            try
            {
                if (codigoBusquedaCorres == null)
                {
                    codigoBusquedaCorres = "";
                }
                if (remitenteBusquedaCorres == null)

                    if (detalleBusquedaCorres == null)
                    {
                        detalleBusquedaCorres = "";
                    }
                if (remitenteBusquedaCorres == null)
                {
                    remitenteBusquedaCorres = "";
                }
                var fecini = Convert.ToDateTime("01/01/0001 00:00:00");
                var fecIniF = Convert.ToDateTime("9999/12/31 00:00:00");
                var fecfin = Convert.ToDateTime("01/01/0001 00:00:00");
                var fecfinF = Convert.ToDateTime("9999/12/31 00:00:00");

                if (fechaIniBusquedaCorres == fecini)
                {
                    fechaIniBusquedaCorres = fecIniF;
                }
                if (fechaFinBusquedaCorres == fecini)
                {
                    fechaFinBusquedaCorres = fecIniF;
                }

                var CadToken = HttpContext.Session.GetString("SessionToken");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                var ListaCorresDetalleBuscador = _repositorio.obtenerCorrespondenciaDetalleBuscador(empCodigo, codigoBusquedaCorres, detalleBusquedaCorres, remitenteBusquedaCorres, fechaIniBusquedaCorres, fechaFinBusquedaCorres, CadToken);


                return ListaCorresDetalleBuscador;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);
                List<clsCorrespondenciaDetalleBusquedaResponse> res = new List<clsCorrespondenciaDetalleBusquedaResponse>();
                return res;
            }

        }

        public async Task<IActionResult> Paquetes()
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                if (CadToken != null && CadToken != "")
                {
                    clsPaquetes listarPaquetes = new clsPaquetes();


                    var Token = HttpContext.Session.GetString("SessionToken");

                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();



                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    //Consume Listado de correspondencia
                    var ListadoPaquetes = await _repositorio.obtenerListaPaquetesAsync(empCodigo, Token);

                    listarPaquetes.LoginResponse = loginResponse;

                    listarPaquetes.ListListadoLink = ListadoLink;
                    listarPaquetes.ListListadoPaquetes = ListadoPaquetes;


                    return View(listarPaquetes);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }
        public async Task<IActionResult> Archiveros()
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                if (CadToken != null && CadToken != "")
                {
                    clsArchivero listarArchivero = new clsArchivero();


                    var Token = HttpContext.Session.GetString("SessionToken");

                    HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();



                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    //Consume Listado de correspondencia
                    var ListadoArchiveros = await _repositorio.obtenerListaArchiveroAsync(empCodigo, Token);

                    listarArchivero.LoginResponse = loginResponse;

                    listarArchivero.ListListadoLink = ListadoLink;
                    listarArchivero.ListListadoArchivero = ListadoArchiveros;


                    return View(listarArchivero);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public IActionResult RegistrarCorrespondencia(string inputCodigoC, string inputNroGuiaC, int txtDescripcionC, int inputDestinoC, string inputRemitenteC, string inputUrgenteC)
        {
            try
            {
                var empCod = HttpContext.Session.GetString("SessionEmpCodigo");

                var CadToken = HttpContext.Session.GetString("SessionToken");
                clsCorrespondenciaRegistrarRequest corres = new clsCorrespondenciaRegistrarRequest();

                corres.UsuCodigo = empCod;
                corres.CorCodigo = inputCodigoC;
                corres.CorNumGuia = inputNroGuiaC;
                corres.CodId = txtDescripcionC;
                corres.AreId = inputDestinoC;
                corres.CorRemitente = inputRemitenteC;
                if (inputUrgenteC != null)
                {
                    corres.CorUrgente = "S";
                }
                else
                {
                    corres.CorUrgente = "N";
                }


                var registroCorres = _repositorio.registrarCorrespondencia(corres, CadToken);


                HttpContext.Session.SetString("SessionCorresRegistrada", "1");

                return RedirectToAction(nameof(Paquetes));

            }
            catch (Exception e)
            {
                _logger.LogError("Error al guardar correspondencia POST " + e);

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public IActionResult RegistrarArchivero(string inputCodigoA, string inputNombreA, string inputDescripcionA, int inputAreaA)
        {
            try
            {
                var empCod = HttpContext.Session.GetString("SessionEmpCodigo");

                var CadToken = HttpContext.Session.GetString("SessionToken");
                clsArchiveroAgregarRequest archivero = new clsArchiveroAgregarRequest();

                archivero.UsuCodigo = empCod;
                archivero.Codigo = inputCodigoA;
                archivero.Nombre = inputNombreA;
                archivero.Descripcion = inputDescripcionA;
                archivero.AreId = inputAreaA;

                var registroArchivero = _repositorio.RegistrarArchivero(archivero, CadToken);


                HttpContext.Session.SetString("SessionCorresRegistrada", "1");

                return RedirectToAction(nameof(Archiveros));

            }
            catch (Exception e)
            {
                _logger.LogError("Error al guardar archivero POST " + e);

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public IActionResult registrarDivisionCorres(int inputCorIdOculto, string inputCdeCodigo, int inputusuFinalId, string inputCdeDetalle, string inputCdeUrgente)
        {
            try
            {
                var empCod = HttpContext.Session.GetString("SessionEmpCodigo");

                var CadToken = HttpContext.Session.GetString("SessionToken");
                clsCorrespondenciaDetalleRegistrarRequest corresDetalle = new clsCorrespondenciaDetalleRegistrarRequest();

                corresDetalle.usuCodigo = empCod;
                corresDetalle.corId = inputCorIdOculto;
                corresDetalle.cdeCodigo = "NOSE";
                corresDetalle.usuFinalId = inputusuFinalId;
                corresDetalle.cdeDetalle = inputCdeDetalle;

                if (inputCdeUrgente != null)
                {
                    corresDetalle.cdeUrgente = "S";
                }
                else
                {
                    corresDetalle.cdeUrgente = "N";
                }




                var registroCorrespondenciaD = _repositorio.registrarCorresDetalle(corresDetalle, CadToken);


                HttpContext.Session.SetString("SessionCorresRegistrada", "1");

                return RedirectToAction(nameof(Paquetes));

            }
            catch (Exception e)
            {
                _logger.LogError("Error al guardar correspondencia POST " + e);

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult registrarCorresDivisionD(int inputCorIdOcultoD, string inputCorIdD, int inputusuFinalIdD, string inputCdeDetalleD, string inputCdeUrgenteD)
        {
            try
            {
                var empCod = HttpContext.Session.GetString("SessionEmpCodigo");

                var CadToken = HttpContext.Session.GetString("SessionToken");
                clsCorrespondenciaDetalleDividirRequest corresDivision = new clsCorrespondenciaDetalleDividirRequest();

                corresDivision.usuCodigo = empCod;
                corresDivision.corId = inputCorIdOcultoD;
                corresDivision.cdeCodigo = inputCorIdD;
                corresDivision.usuFinalId = inputusuFinalIdD;
                corresDivision.cdeDetalle = inputCdeDetalleD;

                if (inputCdeUrgenteD != null)
                {
                    corresDivision.cdeUrgente = "S";
                }
                else
                {
                    corresDivision.cdeUrgente = "N";
                }




                var registroCorrespondenciaD = _repositorio.registrarCorresDivisionD(corresDivision, CadToken);


                HttpContext.Session.SetString("SessionCorresRegistrada", "1");

                return RedirectToAction(nameof(Paquetes));

            }
            catch (Exception e)
            {
                _logger.LogError("Error al guardar correspondencia POST " + e);

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public IActionResult eliminarCorrespondencia(int idPaquetes)
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                var CorresEliminado = _repositorio.eliminarCorrespondencia(empCodigo, idPaquetes, 1, CadToken);


                return RedirectToAction(nameof(Paquetes));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al eliminar al carrito POST" + e);

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public IActionResult eliminarCorrespondenciaDetalle(int idCorrespondencia)
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                var CorresEliminado = _repositorio.eliminarCorrespondenciaDetalle(empCodigo, idCorrespondencia, 2, CadToken);


                return RedirectToAction(nameof(CorrespondenciasRecibidas));
            }
            catch (Exception e)
            {
                _logger.LogError("Error al eliminar al carrito POST" + e);

                return RedirectToAction(nameof(Index));
            }

        }
        /***************************************************/
        /*Aactivos*/
        public async Task<IActionResult> Activo(string codigoBusquedaActivo, string detalleBusquedaActivo)
        {
            try
            {
                //lista de activos

                var CadToken = HttpContext.Session.GetString("SessionToken");
                var usuID = HttpContext.Session.GetString("SessionUsuId");
                var empCodigo = HttpContext.Session.GetString("SessionEmpCodigo");
                HttpContext.Session.SetString("SessionCarrito", _context.ListadoCarrito.Where(elemento => elemento.usuId.ToString() == usuID).ToList().Count.ToString());
                if (CadToken != null && CadToken != "")
                {
                    clsActivo listarActivos = new clsActivo();
                    var Token = HttpContext.Session.GetString("SessionToken");

                    var loginResponse = _context.LoginResponse.Where(elemento => elemento.usuId.ToString() == usuID).FirstOrDefault();

                    //COnsume Link
                    var ListadoLink = _repositorio.obtenerLink(loginResponse.sgrId, Token);

                    //Consume Listado de correspondencia
                    var ListadoActivo = await _repositorio.obtenerListaActivosAsync(usuID, Token);
                    List<clsActivoBuscarResponse> ListaActivoBuscador = new List<clsActivoBuscarResponse>();
                    
                    //Buscador
                    if (codigoBusquedaActivo != null || detalleBusquedaActivo != null)
                    {
                        if (codigoBusquedaActivo == null)
                        {
                            codigoBusquedaActivo = "";
                        }

                        if (detalleBusquedaActivo == null)
                        {
                            detalleBusquedaActivo = "";
                        }
                        ListaActivoBuscador = _repositorio.obtenerActivoBuscador(codigoBusquedaActivo, detalleBusquedaActivo, CadToken);

                    }


                    listarActivos.LoginResponse = loginResponse;

                    listarActivos.ListListadoLink = ListadoLink;
                    listarActivos.ListListadoActivoUbicacion = ListadoActivo;
                    listarActivos.ListListadoActivoBuscador = ListaActivoBuscador;

                    return View(listarActivos);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }



            }
            catch (Exception e)
            {
                _logger.LogError("Error en Get Carrito" + e);
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<List<clsActivoUbicacionHistoricoListarResponse>> ObtenerActivoUbicacionHistoricoListar(string idActivo)
        {
            try
            {
                var CadToken = HttpContext.Session.GetString("SessionToken");

                var ListActivoUbicacion = await _repositorio.obtenerActivoUbicacionHistoricoListar(idActivo, CadToken);

                return ListActivoUbicacion;

            }
            catch (Exception e)
            {
                _logger.LogError("Error al obtener cantidad comprada " + e);
                List<clsActivoUbicacionHistoricoListarResponse> res = new List<clsActivoUbicacionHistoricoListarResponse>();
                return res;
            }

        }
        //************
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
