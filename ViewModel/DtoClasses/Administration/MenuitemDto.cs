namespace MTFS.Business.Dtos.DtoClasses
{
    public class MenuitemDto
    {
        public int id { get; set; }
        public int meuntitleId { get; set; }
        public string pageTitle { get; set; }
        public int displayOrder { get; set; }
        public string href { get; set; }
        public string itemStyle { get; set; }
        public string itemText { get; set; }
        public bool isChecked { get; set; } 
    }

    public class MenuitemMenuDto
    {
        public int id { get; set; }
        public int meuntitleId { get; set; }
        public string pageTitle { get; set; }
        public int displayOrder { get; set; }
        public string href { get; set; }
        public string itemStyle { get; set; }
        public string itemText { get; set; }
    }

}