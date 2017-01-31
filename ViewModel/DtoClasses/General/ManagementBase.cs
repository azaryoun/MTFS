namespace MTFS.Business.Dtos.DtoClasses
{
    public abstract class ManagementBaseDto
    {
        public int totalRecordCount { get; set; }
        public int currentPage { get; set; }
        public int recordCountPage { get; set; }
      

        public ManagementBaseDto()
        {
            this.totalRecordCount = 0;
            this.currentPage = 0;
            this.recordCountPage = 0;
        }

    }
}