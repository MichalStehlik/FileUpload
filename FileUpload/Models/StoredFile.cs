using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.Models
{
    public class StoredFile
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UploaderId")]
        public IdentityUser Uploader { get; set; }
        [Required]
        public string UploaderId { get; set; }
        [Required]
        public DateTime Uploaded { get; set; }
        [Required]
        public string OriginalName { get; set; }
        [Required]
        public string ContentType { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
