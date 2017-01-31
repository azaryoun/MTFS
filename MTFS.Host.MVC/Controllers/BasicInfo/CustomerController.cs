using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers 
{ 
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _CustomerService;

        public CustomerController(ICustomerService CustomerService)

        {
            _CustomerService = CustomerService;
        }

        [HttpGet]
        public async Task<GetCustomersManagementDto> getCustomersManagement(int pageNo, string filter)
        {
            GetCustomersManagementDto oGetCustomersManagementDto =
               await _CustomerService.getCustomersManagement(new GridInitialDto
               {
                   recordCountPerPage = Setting.RECORD_COUNT_PAGE,
                   pageNo = pageNo,
                   filter = filter,
                   userId = Setting.payloadDto.userId
               });

            if (oGetCustomersManagementDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return oGetCustomersManagementDto;

        }

        [HttpGet]
        public async Task<GetCustomerDto> getCustomerInitial()
        {
            GetCustomerDto oGetCustomerDto = await _CustomerService.getCustomerInitial();
            if (oGetCustomerDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetCustomerDto;
        }

        [HttpGet]
        public async Task<GetCustomerDto> getCustomer(int id)
        {
            GetCustomerDto oGetCustomerDto = await _CustomerService.getCustomer(new BaseDto { id = id });
            if (oGetCustomerDto == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            else
                return oGetCustomerDto;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertCustomer(GetCustomerDto CustomerDto)
        {
            CustomerDto.userId = Setting.payloadDto.userId;
            if (await _CustomerService.insertCustomer(CustomerDto))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateCustomer(GetCustomerDto CustomerDto)
        {
            CustomerDto.userId = Setting.payloadDto.userId;
            if (await _CustomerService.updateCustomer(CustomerDto))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);

        }

        [HttpDelete]
        public async Task<HttpResponseMessage> deleteCustomer(int id)
        {

            if (await _CustomerService.deleteCustomer(new BaseDto { id = id }))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NoContent);

        }
    }
}
