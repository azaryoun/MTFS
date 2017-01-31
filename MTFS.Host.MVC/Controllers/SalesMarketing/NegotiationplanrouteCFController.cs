using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers 
{
    public class NegotiationplanrouteCFController : ApiController
    {
        private readonly INegotiationplanrouteCFService _NegotiationplanrouteCFService;

        public NegotiationplanrouteCFController(INegotiationplanrouteCFService NegotiationplanrouteCFService)

        {
            _NegotiationplanrouteCFService = NegotiationplanrouteCFService;
        }

        [HttpGet]
        public async Task<GetNegotiationplanrouteCFsManagementDto> getNegotiationplanrouteCFsManagement(int pageNo, string filter, int parentId, string parentTitle)
        {
            if (parentId == 0)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            GetNegotiationplanrouteCFsManagementDto oGetNegotiationplanrouteCFsManagementDto =
               await _NegotiationplanrouteCFService.getNegotiationplanrouteCFsManagement(new GridChildInitialDto
               {
                   recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                   pageNo = pageNo,
                   filter = filter,
                   userId = Setting.payloadDto.userId,
                   parentId = parentId,
                   parentTitle = parentTitle
               });

            if (oGetNegotiationplanrouteCFsManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oGetNegotiationplanrouteCFsManagementDto;

        }

        [HttpGet]
        public async Task<GetNegotiationplanrouteCFDto> getNegotiationplanrouteCFInitial(int parentId)
        {
            GetNegotiationplanrouteCFDto oGetNegotiationplanrouteCFDto = await _NegotiationplanrouteCFService.getNegotiationplanrouteCFInitial(new BaseDto { id = parentId });
            if (oGetNegotiationplanrouteCFDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetNegotiationplanrouteCFDto;
        }

        [HttpGet]
        public async Task<GetNegotiationplanrouteCFDto> getNegotiationplanrouteCF(int id)
        {
            GetNegotiationplanrouteCFDto oGetNegotiationplanrouteCFDto = await _NegotiationplanrouteCFService.getNegotiationplanrouteCF(new BaseDto { id = id });
            if (oGetNegotiationplanrouteCFDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetNegotiationplanrouteCFDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertNegotiationplanrouteCF(GetNegotiationplanrouteCFDto NegotiationplanrouteCFDto)
        {
            NegotiationplanrouteCFDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationplanrouteCFService.insertNegotiationplanrouteCF(NegotiationplanrouteCFDto))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateNegotiationplanrouteCF(GetNegotiationplanrouteCFDto NegotiationplanrouteCFDto)
        {
            NegotiationplanrouteCFDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationplanrouteCFService.updateNegotiationplanrouteCF(NegotiationplanrouteCFDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }

    }
}
