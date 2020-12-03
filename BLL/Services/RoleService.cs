using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService: IRoleService
    {

        private IUnitOfWork UnitOfWork { get; set; }
        private IMapper mapper { get; set; }

        public RoleService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
        }

        public async Task AddAsync(RoleModel model)
        {
            await UnitOfWork.RoleRepository.AddAsync(mapper.Map<Role>(model));
            await UnitOfWork.SaveAsync();
        }


        public IEnumerable<RoleModel> GetAll()
        { 
            var result = UnitOfWork.RoleRepository.GetRoles();
            return mapper.Map<IEnumerable<ApplicationRole>, List<RoleModel>>(result);
        }
    }
}
