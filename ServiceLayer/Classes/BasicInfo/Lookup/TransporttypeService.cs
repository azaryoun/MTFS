using System.Collections.Generic;
using System.Linq;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.DAL.Context;
using MTFS.Business.Domain.Model;
using System.Data.Entity;
using AutoMapper;
using MTFS.Business.Services.Interfaces;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class TransporttypeService : ITransporttypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Transporttype> _Transporttypes;
        private readonly IDbSet<LocationTransporttype> _LocationTransporttypes;
        public TransporttypeService(IUnitOfWork uow)
        {

            _uow = uow;
            _Transporttypes = _uow.Set<Transporttype>();
            _LocationTransporttypes = _uow.Set<LocationTransporttype>();

        }

        #region Retrive Data 

        public async  Task<List<TransporttypeDto>> getTransporttypesDto()
        {
            return Mapper.Map<IEnumerable<Transporttype>, List<TransporttypeDto>>(await  _Transporttypes.OrderBy(o => o.typeName).AsNoTracking().ToListAsync());
        }

        public async Task<List<TransporttypeDto>> getTransporttypesDto(BaseDto baseDto)
        {
            List<TransporttypeDto> oTransportType = Mapper.Map<IEnumerable<Transporttype>, List<TransporttypeDto>>(await  _Transporttypes.OrderBy(o => o.typeName).AsNoTracking().ToListAsync());

            foreach (var item in oTransportType)
            {
                LocationTransporttype  lnqLocationTransporttypes =await  _LocationTransporttypes
                                                                    .AsNoTracking()
                                                                    .SingleOrDefaultAsync(x =>
                                                                     x.locationId == baseDto.id &&
                                                                     x.transporttypeId == item.id);

                if (lnqLocationTransporttypes != null)
                    item.isChecked = true; 
            }
             
            return  oTransportType ;
        }

        public async Task<List<DdlDto>> getTransporttypesDdlDto()
        {
            return (Mapper.Map<IEnumerable<Transporttype>, List<DdlDto>>(await _Transporttypes.OrderBy(o => o.typeName).AsNoTracking().ToListAsync()));
        }

        #endregion
    }
}
