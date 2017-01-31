using MTFS.Business.Dtos.DtoClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface IAgentCarrierService
    {
        #region Retrive data 

        Task<GetAgentCarriersManagementDto> getAgentCarriersManagement(GridChildInitialDto gridInitialDto);
        Task<GetAgentCarrierDto> getAgentCarrier(BaseDto baseDto);
        Task<GetAgentCarrierDto> getAgentCarrierInitial(BaseDto baseDto);
        Task<List<DdlWithParentDto>> getAgentCarriersDdlDto(int locationId);

        #endregion

        Task<bool> insertAgentCarrier(GetAgentCarrierDto getCarrierDto); 
        Task<bool> updateAgentCarrier(GetAgentCarrierDto getCarrierDto);
    }
}
