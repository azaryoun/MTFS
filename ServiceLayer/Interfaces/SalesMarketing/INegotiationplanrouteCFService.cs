using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface INegotiationplanrouteCFService
    {

        #region Retrive Data 

        Task<GetNegotiationplanrouteCFsManagementDto> getNegotiationplanrouteCFsManagement(GridChildInitialDto gridInitialDto);
        Task<GetNegotiationplanrouteCFDto> getNegotiationplanrouteCF(BaseDto baseDto);
        Task<GetNegotiationplanrouteCFDto> getNegotiationplanrouteCFInitial(BaseDto baseDto);

        #endregion

        Task<bool> insertNegotiationplanrouteCF(GetNegotiationplanrouteCFDto NegotiationplanrouteCFDto);

        Task<bool> updateNegotiationplanrouteCF(GetNegotiationplanrouteCFDto NegotiationplanrouteCFDto);

    }
}
