using System;
using System.Collections.Generic;
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
    public class CategoriaService:ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try
            {
                var listaCategorias = await _categoriaRepository.Consultar();
                return _mapper.Map<List<CategoriaDTO>>(listaCategorias).ToList();
            }
            catch
            {

                throw;
            }
        }
    }
}
