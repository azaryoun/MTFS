using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetPersonDto : BaseDto
        {
            public GetPersonDto()
            {
                countries = new List<DdlDto>();
            }
            public string fullName { get; set; }
            public string IdentityNo { get; set; }
            public string telephone { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email1 { get; set; }
            public string email2 { get; set; }
            public string email3 { get; set; }
            public string address { get; set; }
            public string webSite { get; set; }
            public bool isReal { get; set; } 
            public bool isActive { get; set; } = true;
            public int countryId { get; set; }
            public List<DdlDto> countries { get; set; }

        }

    public class GetPersonsDto : BaseDto

        {
            public string fullName { get; set; }
            public string IdentityNo { get; set; }
            public string telephone { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email { get; set; }
            public bool isActive { get; set; }
            public string isActiveText
            {
                get
                {
                    if (isActive)
                        return "Active";
                    else
                        return "In Active";
                }
            }
        } 
}
