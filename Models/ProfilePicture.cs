using System.ComponentModel.DataAnnotations.Schema;

namespace VertexEMSBackend.Models
{
    public class ProfilePicture
    {
        public Guid ImageId { get; set; }
        public Guid ProfileIMG { get; set; }

        [ForeignKey("Id")]
        public string Id { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
