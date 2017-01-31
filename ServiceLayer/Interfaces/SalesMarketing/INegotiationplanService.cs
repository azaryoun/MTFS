using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface INegotiationplanService
    {
        #region Retrive Data 

        Task<GetNegotiationplansManagementDto> getNegotiationplansManagement(GridChildInitialDto gridInitialDto);
        Task<GetNegotiationplanDto> getNegotiationplan(BaseDto baseDto);

        #endregion

        Task<bool> insertNegotiationplan(GetNegotiationplanDto NegotiationplanDto); 

        Task<bool> updateNegotiationplan(GetNegotiationplanDto NegotiationplanDto);

    }
}
