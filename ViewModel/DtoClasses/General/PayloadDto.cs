namespace MTFS.Business.Dtos.DtoClasses
{
    public class PayloadDto
    {
        public int userId { get; set; } //
        public bool isItemAdmin { get; set; } //
        public int companyId { get; set; } //
        public bool isDataAdmin { get; set; }//
        public long expireDate { get; set; }

    }

    public class JasonWebTokenDto
    {

        public string JWT { get; set; }

    }
}
