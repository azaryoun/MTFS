using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers 
{
    public class PackagetypeController : ApiController
    {
        private readonly IPackagetypeService _PackagetypeService;

        public PackagetypeController(IPackagetypeService PackagetypeService)

        {
            _PackagetypeService = PackagetypeService;
        }

        [HttpGet]
        public async Task<GetPackagetypesManagementDto> getPackagetypesManagement(int pageNo, string filter)
        {
            GetPackagetypesManagementDto oGetPackagetypesManagementDto =
               await _PackagetypeService.getPackagetypesManagement(new GridInitialDto
               {
                   recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                   pageNo = pageNo,
                   filter = filter,
                   userId = Setting.payloadDto.userId
               });

            if (oGetPackagetypesManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oGetPackagetypesManagementDto;

        }

        [HttpGet]
        public  GetPackagetypeDto  getPackagetypeInitial()
        {
            return new GetPackagetypeDto(); 
        }

        [HttpGet]
        public async Task<GetPackagetypeDto> getPackagetype(int id)
        {
            GetPackagetypeDto oGetPackagetypeDto = await _PackagetypeService.getPackagetype(new BaseDto { id = id });
            if (oGetPackagetypeDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetPackagetypeDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertPackagetype(GetPackagetypeDto PackagetypeDto)
        {
            PackagetypeDto.userId = Setting.payloadDto.userId;
            if (await _PackagetypeService.insertPackagetype(PackagetypeDto))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> updatePackagetype(GetPackagetypeDto PackagetypeDto)
        {
            PackagetypeDto.userId = Setting.payloadDto.userId;
            if (await _PackagetypeService.updatePackagetype(PackagetypeDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }

        [HttpDelete]
        public async Task<HttpResponseMessage> deletePackagetype(int id)
        {

            if (await _PackagetypeService.deletePackagetype(new BaseDto { id = id }))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NoContent);

        }
    }
}
