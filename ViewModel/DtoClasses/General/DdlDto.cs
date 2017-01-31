namespace MTFS.Business.Dtos.DtoClasses
{
    public class DdlWithParentDto: DdlDto
    {
        public int parentId { get; set; }
        public string  parentName { get; set; }
    }

    public  class DdlDto
    {
        public int id { get; set; }
        public string  text { get; set; }
    }

}
