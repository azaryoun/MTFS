namespace MTFS.Business.Dtos.DtoClasses
{
    public class GridInitialDto:BaseDto
    {
        public int recordCountPerPage { get; set; }
        public int pageNo { get; set; }
        public string filter { get; set; }   

    }
    public class GridChildInitialDto : BaseDto
    {
        public int recordCountPerPage { get; set; }
        public int pageNo { get; set; }
        public string filter { get; set; }
        public int parentId { get; set; }
        public string parentTitle { get; set; } 
    }
}
