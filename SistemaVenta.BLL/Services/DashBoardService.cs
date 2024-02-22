using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.BLL.Services;
using SistemaVenta.BLL.Services.Interfaces;
using SistemaVenta.DAL.Repository.Interfaces;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Services
{
    public class DashBoardService:IDashBoardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public DashBoardService(IVentaRepository ventaRepository, IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaVentas,int restarCantidadDias) {
            DateTime? ultimaFecha = tablaVentas.OrderByDescending(v=>v.FechaRegistro).Select(v=>v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVentas.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();
            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery,-7);
                total = tablaVenta.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();
            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta.Select(v=>v.Total).Sum(v=>v.Value);
            }
            return Convert.ToString(resultado,new CultureInfo("es-PE"));
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _productoQuery = await _productoRepository.Consultar();

            int total = _productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string,int>> VentasUltimaSemana()
        {
            Dictionary<string,int> resultado = new Dictionary<string,int>();
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();
            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta
                    .GroupBy(v=>v.FechaRegistro.Value.Date)
                    .OrderBy(g=>g.Key)
                    .Select(dv => new
                    {
                        fecha = dv.Key.ToString("dd/MM/yyyy"),
                        total = dv.Count()
                    })
                    .ToDictionary(keySelector: r=> r.fecha, elementSelector:r =>r.total);
            }

            return resultado;
        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashBoard = new DashboardDTO();
            try
            {
                vmDashBoard.TotalVentas = await TotalVentasUltimaSemana();
                vmDashBoard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashBoard.TotalProductos = await TotalProductos();

                List<VentasSemanaDTO> listaVentaSemana = new List<VentasSemanaDTO> ();

                foreach (KeyValuePair<string,int> item in await VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VentasSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value,
                    });
                }
                vmDashBoard.VentasUltimaSemana = listaVentaSemana;

                return vmDashBoard;
            }
            catch
            {

                throw;
            }
        }
    }
}
