using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repository.Interfaces
{
    public interface IVentaRepository:IGenericRepository<Venta>
    {
        Task<Venta> Registar(Venta modelo);
    }
}
