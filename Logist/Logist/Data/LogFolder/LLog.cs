namespace Logist.Data.LogFolder
{
    public class LLog
    {
        public DateTime LogDt { get; set; }
        public int id { get; set; }
        public int? ProgUser { get; set; }
        public int? Stats { get; set; }
        public int isdel { get; set; } = 0;
        public int? oper { get; set; }
        public int? Param1 { get; set; }
        public int? Param2 { get; set; }
        public string? WinUser { get; set; }
        public string? CompName { get; set; }
        public string? param3 { get; set; }
        public string? LogTime { get; set; }
        public int clnum { get; set; }
    }
}
