using System.ComponentModel.DataAnnotations;

namespace UserAuthentication.Entities
{
    public class BaseEntity
    {
        [Key]
        public required Guid Id { get; set; }
    }
}
