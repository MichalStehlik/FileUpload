using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.Models
{
    public class StoredFileContent
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
