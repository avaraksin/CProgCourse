using System.ComponentModel.DataAnnotations;
using Logist.Data.Usr;
namespace Logist.Data
{
    public class ClientTab
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Pwd { get; set; }
        public string? ClientName { get; set; }
        public string? Additional { get; set; }
        public string? Cmnt { get; set; }

        public List<Users>? users { get; set; }
    }
}
