using MTFS.Business.Domain.Model;
using MTFS.Business.Services.Interfaces;
using MTFS.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MTFS.Business.Dtos.DtoClasses;
using AutoMapper;
using MTFS.Business.Services.ExtentionMethods;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class PackagetypeService : IPackagetypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Packagetype> _Packagetypes; 

        public PackagetypeService(IUnitOfWork uow )
        { 
            _uow = uow;
            _Packagetypes = _uow.Set<Packagetype>(); 
        }

        #region Retrive Data 
        public async Task<GetPackagetypesManagementDto> getPackagetypesManagement(GridInitialDto gridInitialDto)
        {
            int pageNo = gridInitialDto.pageNo;

            if (gridInitialDto.pageNo < 1) pageNo = 1;

            IQueryable<Packagetype> oPackagetypes = _Packagetypes.AsQueryable();

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oPackagetypes = oPackagetypes
                       .Where(w => w.packageName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oPackagetypes.AsNoTracking().CountAsync();

            GetPackagetypesManagementDto oGetPackagetypesManagementDto = new GetPackagetypesManagementDto();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);

                if (pageNo > totalPages) pageNo = totalPages;


                oGetPackagetypesManagementDto.getPackagetypesDto = Mapper.Map<IEnumerable<Packagetype>, List<GetPackagetypesDto>>
                                                       (await oPackagetypes.GetPageRecords(pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oGetPackagetypesManagementDto.currentPage = pageNo;
                oGetPackagetypesManagementDto.totalRecordCount = totalRecordCount;

            }

            return oGetPackagetypesManagementDto;
        }
        public async Task<GetPackagetypeDto> getPackagetype(BaseDto baseDto)
        {

            return Mapper.Map<Packagetype, GetPackagetypeDto>(
                await _Packagetypes.AsNoTracking().SingleOrDefaultAsync(i => i.id == baseDto.id));
 
        }
         
        #endregion

        public async Task<bool> insertPackagetype(GetPackagetypeDto getPackagetypeDto)
        {
            try
            {
                Packagetype oPackagetype = Mapper.Map<GetPackagetypeDto, Packagetype>(getPackagetypeDto);
                _Packagetypes.Add(oPackagetype);
                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> updatePackagetype(GetPackagetypeDto getPackagetypeDto)
        {
            try
            {
                Packagetype oPackagetype = await _Packagetypes.SingleAsync(i => i.id == getPackagetypeDto.id);

                oPackagetype.packageName = getPackagetypeDto.packageName;
                oPackagetype.packageCode = getPackagetypeDto.packageCode;
                oPackagetype.packageDesc = getPackagetypeDto.packageDesc;
                oPackagetype.isActive = getPackagetypeDto.isActive; 
                oPackagetype.modiferUserId = getPackagetypeDto.userId; 

                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> deletePackagetype(BaseDto baseDto)
        {
            try
            {
                Packagetype oPackagetype = await _Packagetypes.SingleOrDefaultAsync(i => i.id == baseDto.id);

                if (oPackagetype == null) return false;

                _Packagetypes.Remove(oPackagetype);
                await _uow.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
         
    }
}
