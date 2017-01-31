using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers
{ 
    public class NegotiationController : ApiController
    {
        private readonly INegotiationService _NegotiationService;

        public NegotiationController(INegotiationService NegotiationService)

        {
            _NegotiationService = NegotiationService;
        }

        [HttpGet]
        public async Task<GetNegotiationsManagementDto> getNegotiationsManagement(int pageNo, string filter)
        {
            GetNegotiationsManagementDto oNegotiationsManagementDto = await
                _NegotiationService.getNegotiationsManagement(new GridInitialDto
                {
                    recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                    pageNo = pageNo,
                    filter = filter,
                    userId = Setting.payloadDto.userId
                });

            if (oNegotiationsManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oNegotiationsManagementDto;

        }

        [HttpGet]
        public async Task<GetNegotiationDto> getNegotiationInitial()
        {
            GetNegotiationDto oNegotiationDto = await _NegotiationService.getNegotiationInitial();
            if (oNegotiationDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oNegotiationDto;
        }

        [HttpGet]
        public async Task<GetNegotiationDto> getNegotiation(int id)
        {
            GetNegotiationDto oNegotiationDto = await _NegotiationService.getNegotiation(new BaseDto { id = id, userId = Setting.payloadDto.userId });
            if (oNegotiationDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oNegotiationDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertNegotiation(GetNegotiationDto NegotiationDto)
        {
            NegotiationDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationService.insertNegotiation(NegotiationDto))

                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError); 

        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateNegotiation(GetNegotiationDto NegotiationDto)
        {
            NegotiationDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationService.updateNegotiation(NegotiationDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }
         
    }
}
