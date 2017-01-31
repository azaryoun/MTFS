using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface INegotiationplanrouteService
    {

        #region Retrive Data 

        Task<GetNegotiationplanroutesManagementDto> getNegotiationplanroutesManagement(GridChildInitialDto gridInitialDto);
        Task<GetNegotiationplanrouteDto> getNegotiationplanroute(BaseDto baseDto);
        Task<GetNegotiationplanrouteDto> getNegotiationplanrouteInitial(BaseDto baseDto); 
        Task<int> getFromLocationId(int id);

        #endregion

        Task<bool> insertNegotiationplanroute(GetNegotiationplanrouteDto NegotiationplanrouteDto);

        Task<bool> updateNegotiationplanroute(GetNegotiationplanrouteDto NegotiationplanrouteDto);

    }
}
