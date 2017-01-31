using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class MenutitleDto
    {
        public int id { get; set; }
        public string pageTitle { get; set; }
        public int displayOrder { get; set; }
        public string href { get; set; }
        public string titleStyle { get; set; }
        public string titleText { get; set; }
        public List<MenuitemMenuDto> menuitemsDto { get; set; }
    }
}