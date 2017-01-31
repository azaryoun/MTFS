using MTFS.Business.Domain.Model;
using MTFS.Business.Dtos.DtoClasses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface ICustomerService
    {
        #region Retrive Data

        Task<GetCustomersManagementDto> getCustomersManagement(GridInitialDto gridInitialDto);
        Task<GetCustomerDto> getCustomer(BaseDto baseDto);
        Task<GetCustomerDto> getCustomerInitial();
        Task<List<DdlDto>> getCustomersDdlDto(Expression<Func<Customer, bool>> predicate); 
        Task<GetCustomerTypeDto> getCustomerType(int customerId);

        #endregion 

        Task<bool> insertCustomer(GetCustomerDto getCustomerDto);

        Task<bool> deleteCustomer(BaseDto baseDto);

        Task<bool> updateCustomer(GetCustomerDto getCustomerDto);

    }
}
