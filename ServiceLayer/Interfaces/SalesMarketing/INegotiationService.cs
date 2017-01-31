using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface INegotiationService
    {
        #region Retrive Data
        Task<GetNegotiationsManagementDto> getNegotiationsManagement(GridInitialDto gridInitialDto);
        Task<GetNegotiationDto> getNegotiationInitial();
        Task<GetNegotiationDto> getNegotiation(BaseDto baseDto);

        #endregion

        Task<bool> insertNegotiation(GetNegotiationDto getNegotiationDto);
        Task<bool> updateNegotiation(GetNegotiationDto getNegotiationDto);
    }
}
