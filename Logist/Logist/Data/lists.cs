using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Logist.Common;

namespace Logist.Data
{
    public class Lists
    {
        public int clnum { get; set; }
        public int id { get; set; }
        public string? Name { get; set; }
        public int lnum { get; set; }
    }
}
