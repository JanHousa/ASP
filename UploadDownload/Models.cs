using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UploadDownload
{
    public class StoredFile
    {
        [Key]
        public Guid Id { get; set; }
        public string OriginalName { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadedAt { get; set; }
        public byte[] Data { get; set; } 
    }



}
