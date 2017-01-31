using System.Collections.Generic;
using MTFS.Utilities.Enum;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetNegotiationDto : BaseDto
    {
        public int placeofReceiptLocationId { get; set; }
        public List<DdlDto> placeofReceiptLocations { get; set; }

        public int placeofDeliveryLocationId { get; set; }
        public List<DdlDto> placeofDeliveryLocations { get; set; }

        public bool isCostplus { get; set; } = true;
        public string referenceNo { get; set; }

        public string goodsDesc { get; set; }
        public double? costplusPercent { get; set; }

        public double? totalNetPrice { get; set; } 
        public double totalPayablePrice { get; set; }

        public int currencyId { get; set; }
        public List<DdlDto> currencies { get; set; }
        public NegotiationStates state { get; set; } = NegotiationStates.UnderNegotiation;

        public List<DdlDto> states { get; set; }
        public CustomerTypes  contractor  { get; set; }

        public List<DdlDto> contractors { get; set; } //Contractor in UI  
        public int shipperId { get; set; }

        public List<DdlDto> shippers { get; set; }
        public int consigneeId { get; set; }

        public List<DdlDto> consignees { get; set; }
        public int? notify1Id { get; set; }

        public List<DdlDto> notifys1 { get; set; }
        public int? notify2Id { get; set; }

        public List<DdlDto> notifys2 { get; set; }
        public int? marketerId { get; set; }

        public List<DdlDto> marketers { get; set; } 

    }

    public class GetNegotiationsDto : BaseDto
    {
        public string referenceNo { get; set; }
        public string contractorName { get; set; }
        public string placeofReceiptLocationName { get; set; }
        public string placeofDeliveryLocationName { get; set; }
        public byte state { get; set; }
        public string stateName
        {
            get
            {
                return ((NegotiationStates)state).ToString();
            }
        } 
        public string currencySymbol { get; set; }
        public double totalPayablePrice { get; set; }
    }

    public class GetNegotiationsManagementDto : ManagementBaseDto
    {
        public List<GetNegotiationsDto> getOutNegotiationsDto { get; set; }

        public GetNegotiationsManagementDto()
        {
            getOutNegotiationsDto = new List<GetNegotiationsDto>();
        }
    }

}
