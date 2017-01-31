using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers 
{
    public class AgentCarrierController : ApiController
    {
        private readonly IAgentCarrierService _AgentCarrierService;

        public AgentCarrierController(IAgentCarrierService AgentCarrierService)

        {
            _AgentCarrierService = AgentCarrierService;
        }

        [HttpGet]
        public async Task<GetAgentCarriersManagementDto> getAgentCarriersManagement(int pageNo, string filter,int parentId,string parentTitle)
        {
            if(parentId == 0)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            GetAgentCarriersManagementDto oGetAgentCarriersManagementDto =
               await _AgentCarrierService.getAgentCarriersManagement(new GridChildInitialDto
               {
                   recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                   pageNo = pageNo,
                   filter = filter,
                   userId = Setting.payloadDto.userId,
                   parentId = parentId,
                   parentTitle = parentTitle
               });

            if (oGetAgentCarriersManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oGetAgentCarriersManagementDto;

        }

        [HttpGet]
        public async Task<GetAgentCarrierDto> getAgentCarrierInitial(int parentId)
        {
            GetAgentCarrierDto oGetAgentCarrierDto = await _AgentCarrierService.getAgentCarrierInitial(new BaseDto { id= parentId });
            if (oGetAgentCarrierDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetAgentCarrierDto;
        }

        [HttpGet]
        public async Task<GetAgentCarrierDto> getAgentCarrier(int id)
        {
            GetAgentCarrierDto oGetAgentCarrierDto = await _AgentCarrierService.getAgentCarrier(new BaseDto { id = id });
            if (oGetAgentCarrierDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetAgentCarrierDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertAgentCarrier(GetAgentCarrierDto AgentCarrierDto)
        {
            AgentCarrierDto.userId = Setting.payloadDto.userId;
            if (await _AgentCarrierService.insertAgentCarrier(AgentCarrierDto))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateAgentCarrier(GetAgentCarrierDto AgentCarrierDto)
        {
            AgentCarrierDto.userId = Setting.payloadDto.userId;
            if (await _AgentCarrierService.updateAgentCarrier(AgentCarrierDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }
         
    }
}
