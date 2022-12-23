using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;
using UserPermission.Domain.Interface.Repository;

namespace UserPermission.Business
{
    public class PermissionService : IPermissionService<Permission>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public void Add(Permission entity)
        {
            try
            {
                var rs = _unitOfWork.PermissionTypeRepository.Entities.Where(x => x.Description.Equals(entity.PermissionTypes.Description)).FirstOrDefault();

                if (rs == null)
                {
                    if (entity.PermissionTypes?.Id == 0)
                    {
                        _unitOfWork.PermissionTypeRepository.Add(entity.PermissionTypes);
                    }
                    else
                    {
                        var data = _unitOfWork.PermissionTypeRepository.Entities.Where(x => x.Id == entity.PermissionTypes!.Id).FirstOrDefault();
                        if (data == null)
                        {
                            entity.PermissionTypes.Id = 0;
                            _unitOfWork.PermissionTypeRepository.Add(entity.PermissionTypes!);
                        }
                        else
                        {
                            data.Description = entity.PermissionTypes.Description;
                        }
                    }
                }
                else
                {
                    entity.PermissionTypes = rs;
                }

                if (entity.Id == 0) {
                    _unitOfWork.PermissionRepository.Add(entity);
                }

                _unitOfWork.Commit();
            }
            catch{
                _unitOfWork.RejectChanges();
                throw;
            }

        }

        public void Remove(Permission entity) { 
        
        }
        public void Modify(Permission entity)
        {
            try
            {

                var rs = _unitOfWork.PermissionTypeRepository.Entities.Where(x=>x.Description.Equals(entity.PermissionTypes.Description)).FirstOrDefault();

                if (rs == null)
                {
                    if (entity.PermissionTypes?.Id == 0)
                    {
                        _unitOfWork.PermissionTypeRepository.Add(entity.PermissionTypes);
                    }
                    else
                    {
                        var data = _unitOfWork.PermissionTypeRepository.Entities.Where(x => x.Id == entity.PermissionTypes!.Id).FirstOrDefault();
                        if (data == null)
                        {
                            entity.PermissionTypes.Id = 0;
                            _unitOfWork.PermissionTypeRepository.Add(entity.PermissionTypes!);
                        }
                        else
                        {
                            data.Description = entity.PermissionTypes.Description;
                        }
                    }
                }
                else {
                    entity.PermissionTypes = rs;
                }
                

                if (entity.Id == 0)
                {
                    _unitOfWork.PermissionRepository.Add(entity);
                }
                else
                {
                    var data = _unitOfWork.PermissionRepository.Entities.Where(x => x.Id == entity!.Id).FirstOrDefault();
                    if (data == null)
                    {
                        entity.Id = 0;
                        _unitOfWork.PermissionRepository.Add(entity!);
                    }
                    else
                    {
                        data.EmployeeForename = entity.EmployeeForename;
                        data.EmployeeSurname = entity.EmployeeSurname;
                    }
                }

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.RejectChanges();
                throw;
            }
        }

        public IList<Permission> Select(Permission entity)
        {
            try
            {
                var rs = _unitOfWork.PermissionRepository.Entities.Include(x=>x.PermissionTypes).Where(x=>x.Id == entity.Id || (entity==null || entity.Id == 0)).ToList();
                return rs;
            }
            catch
            {
                throw;
            }
        }
    }
}
