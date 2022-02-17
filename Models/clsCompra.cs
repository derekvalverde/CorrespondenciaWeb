using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCompra
    {
        public int Id { get; set; }
        public string matCodigo { get; set; }
        public float comPromedio { get; set; }
        public int comCantidadPromedio { get; set; }
        public int comImportancia { get; set; }

        public decimal obtPrecio(string mcodigo, List<clsMaterialVenta> ListListadoMaterialVenta)
        {
            foreach (clsMaterialVenta objMat in ListListadoMaterialVenta)
            {
                if (objMat.matCodigo == mcodigo)
                {
                    return objMat.marValor;
                }

            }
            return 0;
        }

        public string obtNomProd(string mcodigo, List<clsMaterialVenta> ListListadoMaterialVenta)
        {
            foreach (clsMaterialVenta objMat in ListListadoMaterialVenta)
            {
                if (objMat.matCodigo == mcodigo)
                {
                    return objMat.matNombre;
                }

            }
            return "NoTiene";
        }
        
    }
}
