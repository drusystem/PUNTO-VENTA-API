﻿using System;
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
    public class MenuService:IMenuService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepository, IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _usuarioRepository.Consultar(
                    u=>u.IdUsuario == idUsuario
                );

            IQueryable<MenuRol> tbMenuRol = await _menuRolRepository.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepository.Consultar();

            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m
                                                ).AsQueryable();
                var listaMenus = tbResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch
            {

                throw;
            }
        }
    }
}
