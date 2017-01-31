using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers
{
    public class AccessgroupController : ApiController
    {

        private readonly  IAccessgroupService _AccessgroupService;
        private readonly  IMenuitemService _MenuitemService;


        public AccessgroupController(IAccessgroupService accessgroupService,  IMenuitemService menuitemService)
             
        {
      
             _AccessgroupService = accessgroupService;
             _MenuitemService = menuitemService;
        }

        [HttpGet]
        public async Task<AccessgroupsManagementDto> getAccessgroupsManagement(int pageNo, string filter)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            GridInitialDto gridInitialDto = new GridInitialDto { recordCountPerPage = Setting.RECORD_COUNT_PAGE, pageNo = pageNo, filter = filter,
                                                                 userId = Setting.payloadDto.userId, companyId = Setting.payloadDto.companyId  };

            return await  _AccessgroupService.getAccessgroupsManagement(gridInitialDto); 
        }

        [HttpGet]
        public async Task<AccessgroupDto> getAccessgroupInitial()
        {

            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            var intUserId = Setting.payloadDto.userId;
        
            var oAccessgroup = new AccessgroupDto();
            oAccessgroup.menuItemsDto =await  _MenuitemService.getMenuitemsDto();

            return (oAccessgroup);
          
        }

        [HttpGet]
        public async Task<AccessgroupDto> getAccessgroup(int id)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            BaseDto baseDto = new BaseDto {id=id, userId = Setting.payloadDto.userId, companyId = Setting.payloadDto.companyId };

            bool blnIsItemAdmin = Setting.payloadDto.isItemAdmin;

            AccessgroupDto oAccessgroupDto =await  _AccessgroupService.getAccessgroup(baseDto);


            if (oAccessgroupDto != null) 
                oAccessgroupDto.menuItemsDto =await  _MenuitemService.getMenuitemsDto(baseDto);
           

            return (oAccessgroupDto); 
        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertAccessgroup(AccessgroupDto accessgroupDto)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            HttpResponseMessage result = new HttpResponseMessage();
            //var oResult = new ResultDto();
            if (accessgroupDto.groupName.Trim() == "")
                result.StatusCode = HttpStatusCode.NotFound;
            //oResult.resultCode = "404";
            else
            {
                accessgroupDto.ownerCompanyId = Setting.payloadDto.companyId;
                accessgroupDto.userId= Setting.payloadDto.userId; 

                bool blnInserted =await  _AccessgroupService.insertAccessgroup(accessgroupDto);
                if (blnInserted)
                    result.StatusCode = HttpStatusCode.Created;
                // oResult.resultCode = "200";
                else
                    result.StatusCode = HttpStatusCode.NotFound;
               // oResult.resultCode = "404";
            }

            return result; 
        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateAccessgroup(AccessgroupDto accessgroupDto)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            HttpResponseMessage result = new HttpResponseMessage();
            //var oResultDto = new ResultDto();
            if (accessgroupDto.groupName.Trim() == "")
                result.StatusCode = HttpStatusCode.NotFound;
           // oResultDto.resultCode = "404";
            else
            {
                accessgroupDto.ownerCompanyId  = Setting.payloadDto.companyId;
                accessgroupDto.userId  = Setting.payloadDto.userId;

                bool blnUpdated =await  _AccessgroupService.updateAccessgroup(accessgroupDto);
                if (blnUpdated)
                    result.StatusCode = HttpStatusCode.OK;
                else
                    result.StatusCode = HttpStatusCode.NotFound;
                //oResultDto.resultCode = "404";
            }

            return result;

        }

        [HttpDelete]
        public async Task<HttpResponseMessage> deleteAccessgroup(int id)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            HttpResponseMessage result = new HttpResponseMessage();
            //var oResult = new ResultDto();
           
            bool blnDeleted =await  _AccessgroupService.deleteAccessgroup(new BaseDto { companyId = Setting.payloadDto.companyId, id = id , userId=Setting.payloadDto.userId });
             
            if (blnDeleted)
                result.StatusCode = HttpStatusCode.OK;
            // oResult.resultCode = "200"; 
            else
                result.StatusCode = HttpStatusCode.NotFound;
            // oResult.resultCode = "404";

            return result;

        }

        
    }
}