using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers
{
    public class LocationController : ApiController
    {
        private readonly ILocationService _LocationService;

        public LocationController(ILocationService locationService)

        {
            _LocationService = locationService;
        }

        [HttpGet]
        public async  Task<GetLocationsManagementDto>  getLocationsManagement(int pageNo, string filter)
        {
            GetLocationsManagementDto oLocationsManagementDto =await 
                _LocationService.getLocationsManagement(new GridInitialDto {recordCountPerPage=Setting.RECORD_COUNT_PAGE,
                                                                           pageNo = pageNo,
                                                                           filter = filter,
                                                                           userId=Setting.payloadDto.userId });

            if (oLocationsManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oLocationsManagementDto;

        }

        [HttpGet]
        public async Task<GetLocationDto> getLocationInitial()
        {
            GetLocationDto oLocationDto = await  _LocationService.getLocationInitial();
            if (oLocationDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oLocationDto;
        }

        [HttpGet]
        public async Task<GetLocationDto> getLocation(int id)
        {
            GetLocationDto oLocationDto =await  _LocationService.getLocation(new BaseDto { id=id, userId  = Setting.payloadDto.userId });
            if (oLocationDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oLocationDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertLocation(GetLocationDto locationDto)
        {
            locationDto.userId = Setting.payloadDto.userId;
            if (await  _LocationService.insertLocation(locationDto))

                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            //string uri = Url.Link("DefaultApi", new { id = locationDto.Id });
            //response.Headers.Location = new Uri(uri); 

        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateLocation(GetLocationDto  locationDto)
        {
            locationDto.userId = Setting.payloadDto.userId;
            if (await  _LocationService.updateLocation(locationDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified); 

        }

        [HttpDelete]
        public async Task<HttpResponseMessage> deleteLocation(int id)
        {

            if (await  _LocationService.deleteLocation(new BaseDto { id=id, userId=Setting.payloadDto.userId}))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NoContent);

        }

    }
}