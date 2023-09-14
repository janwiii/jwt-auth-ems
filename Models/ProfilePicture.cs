using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VertexEMSBackend.Models
{
    public class ProfilePicture
    {
        [Key]
        public Guid ImageId { get; set; }
        public Guid ProfileIMG { get; set; }

        [ForeignKey("Id")]
        public Guid  Id { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
