﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.DTOs
{
    public class FileResponse
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        public byte[] FileContent { get; set; } = Array.Empty<byte>();
    }
}
