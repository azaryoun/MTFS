using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class ResultDto
    {
        public string resultCode { get; set; }
        public UserInfoDto userInfoDto { get; set; }
        public List<MenutitleDto> menutitlesDto { get; set; }
        public string pageTitle { get; set; }
    }
}
