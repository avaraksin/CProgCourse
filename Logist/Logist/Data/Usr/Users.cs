namespace Logist.Data.Usr
{
    public class Users
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? Shortname { get; set; }
        public string? Pass { get; set; }
        public int isdel { get; set; } = 0;
        public int tp { get; set; }
        public string? AdminUser { get; set; }
        public int? SECLEVEL { get; set; }
        public int? OnlyOWn { get; set; }
        public DateTime? CrDate { get; set; }
        public DateTime? chdate { get; set; }
        public int? cuser { get; set; }
        public int clnum { get; set; }

        //public List<Listname> listnames { get; set; }
    }
}
