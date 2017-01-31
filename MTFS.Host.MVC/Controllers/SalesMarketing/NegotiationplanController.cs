using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers
{
    public class NegotiationplanController : ApiController
    {
        private readonly INegotiationplanService _NegotiationplanService;

        public NegotiationplanController(INegotiationplanService NegotiationplanService)

        {
            _NegotiationplanService = NegotiationplanService;
        }

        [HttpGet]
        public async Task<GetNegotiationplansManagementDto> getNegotiationplansManagement(int pageNo, string filter, int parentId, string parentTitle)
        {
            GetNegotiationplansManagementDto oNegotiationplansManagementDto = await
                _NegotiationplanService.getNegotiationplansManagement(new GridChildInitialDto
                {
                    recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                    pageNo = pageNo,
                    filter = filter,
                    userId = Setting.payloadDto.userId,
                    parentId=parentId,
                    parentTitle=parentTitle 
                });

            if (oNegotiationplansManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oNegotiationplansManagementDto;

        }

        [HttpGet]
        public   GetNegotiationplanDto  getNegotiationplanInitial(int parentId)
        {
            return new GetNegotiationplanDto { negotiationId= parentId }; 
        }

        [HttpGet]
        public async Task<GetNegotiationplanDto> getNegotiationplan(int id)
        {
            GetNegotiationplanDto oNegotiationplanDto = await _NegotiationplanService.getNegotiationplan(new BaseDto { id = id, userId = Setting.payloadDto.userId });
            if (oNegotiationplanDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oNegotiationplanDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertNegotiationplan(GetNegotiationplanDto NegotiationplanDto)
        {
            NegotiationplanDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationplanService.insertNegotiationplan(NegotiationplanDto))

                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateNegotiationplan(GetNegotiationplanDto NegotiationplanDto)
        {
            NegotiationplanDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationplanService.updateNegotiationplan(NegotiationplanDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }

    }
}
