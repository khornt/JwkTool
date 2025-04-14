using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwkTool2.Models.Dto
{
    public class JwkResponse
    {
        public string? DigDirJwkString { get; set; }
        public bool? Success { get; set; }
        public string? ErrorCode { get; set; }
    }
}
