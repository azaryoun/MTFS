using AutoMapper;
using MTFS.Business.Domain.Model;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.ExtentionMethods;
using MTFS.Business.Services.Interfaces;
using MTFS.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes 
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Customer> _Customers;
        private readonly ICountryService _CountryService;

        public CustomerService(IUnitOfWork uow,
                               ICountryService countryService)
        {

            _uow = uow;
            _Customers = _uow.Set<Customer>();
            _CountryService = countryService;
        }

        #region Retrive Data
        public async Task<GetCustomersManagementDto> getCustomersManagement(GridInitialDto gridInitialDto)
        {
            int pageNo = gridInitialDto.pageNo;

            if (gridInitialDto.pageNo < 1) pageNo = 1;

            IQueryable<Customer> oCustomers = _Customers.AsQueryable();

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oCustomers = oCustomers
                       .Where(w => w.fullName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oCustomers.AsNoTracking().CountAsync();

            GetCustomersManagementDto oGetCustomersManagementDto = new GetCustomersManagementDto();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);

                if (pageNo > totalPages) pageNo = totalPages;


                oGetCustomersManagementDto.getCustomersDto = Mapper.Map<IEnumerable<Customer>, List<GetCustomersDto>>
                                                       (await oCustomers.GetPageRecords(pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oGetCustomersManagementDto.currentPage = pageNo;
                oGetCustomersManagementDto.totalRecordCount = totalRecordCount;

            }

            return oGetCustomersManagementDto;
        }

        public async Task<GetCustomerDto> getCustomer(BaseDto baseDto)
        {
            GetCustomerDto oGetCustomerDto = Mapper.Map<Customer, GetCustomerDto>(await  _Customers.AsNoTracking().SingleOrDefaultAsync(i=>i.id==baseDto.id));
            oGetCustomerDto.countries = await _CountryService.getCountriesDdlDto();

            return oGetCustomerDto;
        }

        public async Task<GetCustomerDto> getCustomerInitial()
        {
            GetCustomerDto oGetCustomerDto = new GetCustomerDto();

            oGetCustomerDto.countries = await _CountryService.getCountriesDdlDto();

            return oGetCustomerDto;
        }

        public async Task<List<DdlDto>> getCustomersDdlDto(Expression<Func<Customer, bool>> predicate)
        {
            return  (Mapper.Map<IEnumerable<Customer>, List<DdlDto>>(await _Customers.Where(predicate).OrderBy(o => o.fullName).AsNoTracking().ToListAsync()));
        }

        public async Task<GetCustomerTypeDto> getCustomerType(int customerId)
        {
            return (Mapper.Map<Customer, GetCustomerTypeDto>(await _Customers.Where(i => i.id== customerId &&  i.isActive == true).AsNoTracking().FirstAsync()));
        }

        #endregion

        public async Task<bool> insertCustomer(GetCustomerDto getCustomerDto)
        {
            try
            {
                Customer oCustomer = Mapper.Map<GetCustomerDto, Customer>(getCustomerDto);
                _Customers.Add(oCustomer);
                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> updateCustomer(GetCustomerDto getCustomerDto)
        {
            try
            {
                Customer oCustomer = await _Customers.SingleAsync(i => i.id == getCustomerDto.id);
                oCustomer.address = getCustomerDto.address;
                oCustomer.countryId = getCustomerDto.countryId;
                oCustomer.email1 = getCustomerDto.email1;
                oCustomer.email2 = getCustomerDto.email2;
                oCustomer.email3 = getCustomerDto.email3;
                oCustomer.fax = getCustomerDto.fax;
                oCustomer.fullName = getCustomerDto.fullName;
                oCustomer.IdentityNo = getCustomerDto.IdentityNo;
                oCustomer.isActive = getCustomerDto.isActive;
                oCustomer.isReal = getCustomerDto.isReal;
                oCustomer.mobile = getCustomerDto.mobile;
                oCustomer.modiferUserId = getCustomerDto.userId;
                oCustomer.telephone = getCustomerDto.telephone;
                oCustomer.webSite = getCustomerDto.webSite;
                oCustomer.isConsignee = getCustomerDto.isConsignee;
                oCustomer.isNotify = getCustomerDto.isNotify;
                oCustomer.isShipper = getCustomerDto.isShipper;

                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            { 
                return false;
            }
        }

        public async Task<bool> deleteCustomer(BaseDto baseDto)
        {
            try
            {
                Customer oCustomer = await _Customers.SingleOrDefaultAsync(i => i.id == baseDto.id);

                if (oCustomer == null) return false;

                _Customers.Remove(oCustomer);
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
