using System.ComponentModel.DataAnnotations.Schema;

namespace AutomobileRentalManagementAPI.Domain.Entities.Base
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid NavigationId { get; set; }

        // You can add other to rastreability ex: createAt, createBy, isActive (logical delete), etc...
    }
}
