using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Models;
using GameStore.DAL.Models.Base;

namespace GameStore.BLL.Services
{
    public abstract class BaseService<TBlEntity,TDalEntity>: IDisposable where TBlEntity: class where TDalEntity:BaseEntity
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private bool _isDisposed;

        protected void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static TDalEntity ToDalEntity(TBlEntity blEntity)
        {
            return AutoMapper.Mapper.Map<TBlEntity, TDalEntity>(blEntity);
        }

        public static IEnumerable<TDalEntity> ToDalEntity(IEnumerable<TBlEntity> blEntity)
        {
            return AutoMapper.Mapper.Map<IEnumerable<TBlEntity>, IEnumerable<TDalEntity>>(blEntity);
        }

        public static TBlEntity ToBlEntity(TDalEntity dalEntity)
        {
            return AutoMapper.Mapper.Map<TDalEntity,TBlEntity>(dalEntity);
        }

        public static IEnumerable<TBlEntity> ToBlEntity(IEnumerable<TDalEntity> dalEntity)
        {
            return AutoMapper.Mapper.Map<IEnumerable<TDalEntity>, IEnumerable<TBlEntity>>(dalEntity);
        }
    }
}
