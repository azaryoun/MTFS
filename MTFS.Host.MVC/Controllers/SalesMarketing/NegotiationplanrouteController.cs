using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers
{
    public class NegotiationplanrouteController : ApiController
    {
        private readonly INegotiationplanrouteService _NegotiationplanrouteService;

        public NegotiationplanrouteController(INegotiationplanrouteService NegotiationplanrouteService)

        {
            _NegotiationplanrouteService = NegotiationplanrouteService;
        }

        [HttpGet]
        public async Task<GetNegotiationplanroutesManagementDto> getNegotiationplanroutesManagement(int pageNo, string filter, int parentId, string parentTitle)
        {
            if (parentId == 0)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            GetNegotiationplanroutesManagementDto oGetNegotiationplanroutesManagementDto =
               await _NegotiationplanrouteService.getNegotiationplanroutesManagement(new GridChildInitialDto
               {
                   recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                   pageNo = pageNo,
                   filter = filter,
                   userId = Setting.payloadDto.userId,
                   parentId = parentId,
                   parentTitle = parentTitle
               });

            if (oGetNegotiationplanroutesManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oGetNegotiationplanroutesManagementDto;

        }

        [HttpGet]
        public async Task<GetNegotiationplanrouteDto> getNegotiationplanrouteInitial(int parentId)
        {
            GetNegotiationplanrouteDto oGetNegotiationplanrouteDto = await _NegotiationplanrouteService.getNegotiationplanrouteInitial(new BaseDto { id = parentId });
            if (oGetNegotiationplanrouteDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetNegotiationplanrouteDto;
        }

        [HttpGet]
        public async Task<GetNegotiationplanrouteDto> getNegotiationplanroute(int id)
        {
            GetNegotiationplanrouteDto oGetNegotiationplanrouteDto = await _NegotiationplanrouteService.getNegotiationplanroute(new BaseDto { id = id });
            if (oGetNegotiationplanrouteDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetNegotiationplanrouteDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertNegotiationplanroute(GetNegotiationplanrouteDto NegotiationplanrouteDto)
        {
            NegotiationplanrouteDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationplanrouteService.insertNegotiationplanroute(NegotiationplanrouteDto))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateNegotiationplanroute(GetNegotiationplanrouteDto NegotiationplanrouteDto)
        {
            NegotiationplanrouteDto.userId = Setting.payloadDto.userId;
            if (await _NegotiationplanrouteService.updateNegotiationplanroute(NegotiationplanrouteDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }

    }
}
