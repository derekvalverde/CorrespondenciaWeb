using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class AplicationDBContext: DbContext
    {
        public AplicationDBContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<clsAutenticacionToken> tblAutenticacion { get; set; }

        public DbSet<clsListarProductosResponse> ListadoGeneral { get; set; }
    }
}
