using MTFS.Business.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;


namespace MTFS.DAL.Mapping
{
    public class NegotiationMap : EntityTypeConfiguration<Negotiation>
    {
        public NegotiationMap()
        {
            ToTable("Negotiation", "SalesMarketing");

            Property(p => p.referenceNo).HasMaxLength(8).HasColumnType("Char").IsRequired().HasColumnAnnotation(
                        IndexAnnotation.AnnotationName,
                            new IndexAnnotation(
                            new IndexAttribute("IX_referenceNo", 1) { IsUnique = true }));

            Property(p => p.goodsDesc).HasMaxLength(500);

            HasRequired(e => e.contractor)
               .WithMany(e => e.negotiationContractors)
               .HasForeignKey(e => e.contractorId)
               .WillCascadeOnDelete(false)
               ;

            HasOptional(e => e.shipper)
            .WithMany(e => e.negotiationShippers)
            .HasForeignKey(e => e.shipperId)
            .WillCascadeOnDelete(false)
            ;

            HasOptional(e => e.consignee)
          .WithMany(e => e.negotiationConsignees)
          .HasForeignKey(e => e.consigneeId)
          .WillCascadeOnDelete(false)
          ;


            HasOptional(e => e.notify1)
              .WithMany(e => e.negotiationNotify1s)
              .HasForeignKey(e => e.notify1Id)
              .WillCascadeOnDelete(false)
          ;

            HasOptional(e => e.notify2)
               .WithMany(e => e.negotiationNotify2s)
               .HasForeignKey(e => e.notify2Id)
               .WillCascadeOnDelete(false)
           ;

            HasOptional(e => e.marketer)
              .WithMany(e => e.negotiationMarketers)
              .HasForeignKey(e => e.marketerId)
              .WillCascadeOnDelete(false)
              ;

            // HasRequired(e => e.currency)
            // .WithMany(e => e.negotiations)
            // .HasForeignKey(e => e.currencyId)
            //.WillCascadeOnDelete(false)
            // ;

            HasRequired(e => e.placeofDeliveryLocation)
                .WithMany(e => e.asPlaceofDeliveryNegotiations)
                .HasForeignKey(e => e.placeofDeliveryLocationId)
                .WillCascadeOnDelete(false);
            ;

            HasRequired(e => e.placeofReceiptLocation)
              .WithMany(e => e.asPlaceofReceiptNegotiations)
              .HasForeignKey(e => e.placeofReceiptLocationId)
                .WillCascadeOnDelete(false);
            ;


        }
    }
}
