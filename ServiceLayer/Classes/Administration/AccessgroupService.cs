using System;
using System.Linq;
using MTFS.DAL.Context;
using System.Data.Entity;
using MTFS.Business.Domain.Model;
using System.Collections.Generic;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using MTFS.Business.Services.ExtentionMethods;
using AutoMapper;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class AccessgroupService : IAccessgroupService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Accessgroup> _Accessgroups;
        private readonly IDbSet<AccessgroupMenuitem> _AccessgroupMenuitems;

        public AccessgroupService(IUnitOfWork uow)
        {

            _uow = uow;
            _Accessgroups = _uow.Set<Accessgroup>();
            _AccessgroupMenuitems = _uow.Set<AccessgroupMenuitem>();

        }

        public async Task<AccessgroupsManagementDto> getAccessgroupsManagement(GridInitialDto gridInitialDto)
        {

            if (gridInitialDto.pageNo < 1)
                gridInitialDto.pageNo = 1; 

            var oAccessgroupsManagementDto = new AccessgroupsManagementDto();
             
            var lnqAccessgroup = _Accessgroups
                .Where(current =>
                current.ownerCompanyId == gridInitialDto.companyId
                )
                ;

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                lnqAccessgroup = lnqAccessgroup
                    .Where(current =>
                    current.groupName.Contains(gridInitialDto.filter) == true
                    )
                    ;

            var intTotalRecordCount =await  lnqAccessgroup.AsNoTracking().CountAsync();

            if (intTotalRecordCount != 0)
            {

                int intTotalPages = (int)Math.Ceiling((double)intTotalRecordCount / gridInitialDto.recordCountPerPage);
                if (gridInitialDto.pageNo > intTotalPages)
                    gridInitialDto.pageNo = intTotalPages;

                oAccessgroupsManagementDto.accessgroupsDto = Mapper.Map<IEnumerable<Accessgroup>, List<AccessgroupDto>>(await  lnqAccessgroup.GetPageRecords(gridInitialDto.pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                ///Previouse Code
                //foreach (var itmAccessgroup in lnqAccessgroup)
                //{
                //    var oAccessgroup = new Dto.AccessgroupDto();
                //    oAccessgroup.groupName = itmAccessgroup.groupName;
                //    oAccessgroup.id = itmAccessgroup.id;
                //    oAccessgroupsManagementDto.accessgroupsDto.Add(oAccessgroup);
                //}

                oAccessgroupsManagementDto.currentPage = gridInitialDto.pageNo;
                oAccessgroupsManagementDto.totalRecordCount = intTotalRecordCount;

            }

            return (oAccessgroupsManagementDto);

        }

        public async Task<bool> insertAccessgroup(AccessgroupDto accessgroupDto)
        {
            try
            {
                Accessgroup oAccessgroup = Mapper.Map<AccessgroupDto, Accessgroup>(accessgroupDto);
                // var oAccessgroup = new Accessgroup();
                //oAccessgroup.ownerCompanyId = companyId;
                //oAccessgroup.modiferUserId = accessgroupDto.userId;
                //oAccessgroup.groupName = accessgroupDto.groupName;
                 

                foreach (var itmMenuItems in accessgroupDto.menuItemsDto)
                    if (itmMenuItems.isChecked == true)
                    {
                        var itmAccessgroupMenuitem = new AccessgroupMenuitem();
                        itmAccessgroupMenuitem.createdUserId = accessgroupDto.userId;
                        itmAccessgroupMenuitem.menuitemId = itmMenuItems.id;
                        oAccessgroup.accessgroupMenuitems.Add(itmAccessgroupMenuitem);
                    }
                _Accessgroups.Add(oAccessgroup);

                await  _uow.SaveChangesAsync();

                return (true);
            }
            catch
            {
                return (false);
            }

        }

        public async  Task<AccessgroupDto> getAccessgroup(BaseDto baseDto)
        {

            var lnqAccessgroup =await  _Accessgroups
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync (x =>
                                         x.ownerCompanyId == baseDto.companyId &&
                                         x.id == baseDto.id );

            AccessgroupDto oAccessgroupDto = Mapper.Map<Accessgroup, AccessgroupDto>(lnqAccessgroup);
            oAccessgroupDto.menuItemsDto = null;

            //AccessgroupDto oAccessgroupDto = null; 

            //if (lnqAccessgroup != null)
            //{
            //    oAccessgroupDto = new AccessgroupDto();
            //    oAccessgroupDto.id = lnqAccessgroup.id;
            //    oAccessgroupDto.groupName = lnqAccessgroup.groupName;
            //    oAccessgroupDto.menuItemsDto = null;
            //}

            return oAccessgroupDto;

        }

        public async Task<bool> deleteAccessgroup(BaseDto baseDto)
        {

            Accessgroup  oAccessgroups =await  _Accessgroups
                                                .SingleOrDefaultAsync(x =>
                                                x.ownerCompanyId == baseDto.companyId &&
                                                x.id == baseDto.id
                                                );

            if (oAccessgroups == null)
                return (false);

            _Accessgroups.Remove(oAccessgroups);
            await  _uow.SaveChangesAsync();
            return (true);

        }

        public async Task<bool> updateAccessgroup(AccessgroupDto accessgroupDto)
        {
            try
            { 
                Accessgroup  oAccessgroup =await  _Accessgroups
                                            .SingleAsync(x =>
                                            x.ownerCompanyId == accessgroupDto.ownerCompanyId &&
                                            x.id == accessgroupDto.id
                                            );

                oAccessgroup.groupName = accessgroupDto.groupName;
                oAccessgroup.modiferUserId = accessgroupDto.userId;


                foreach (var oMenuItemDto in accessgroupDto.menuItemsDto)
                {
                    var oFoundAccessgroupMenuitem =await  _AccessgroupMenuitems
                                                            .SingleOrDefaultAsync(x =>
                                                            x.accessgroupId == accessgroupDto.id &&
                                                            x.menuitemId == oMenuItemDto.id
                                                            );

                    if (oFoundAccessgroupMenuitem == null)
                    {
                        if (oMenuItemDto.isChecked == true)
                        {
                            var oNewAccessgroupMenuitem = new AccessgroupMenuitem();
                            oNewAccessgroupMenuitem.createdUserId = accessgroupDto.userId;
                            oNewAccessgroupMenuitem.accessgroupId = accessgroupDto.id;
                            oNewAccessgroupMenuitem.menuitemId = oMenuItemDto.id;
                            _AccessgroupMenuitems.Add(oNewAccessgroupMenuitem);
                        }
                    }
                    else
                    {
                        if (oMenuItemDto.isChecked == false)
                            _AccessgroupMenuitems.Remove(oFoundAccessgroupMenuitem);
                    }
                }

               await  _uow.SaveChangesAsync();


                return (true);
            }
            catch
            {

                return (false);
            }


        } 
    }
}
