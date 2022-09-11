namespace Logist.Data.MainData
{
    public class LCust
    {
        public int id { get; set; }
        public int yr { get; set; }
        public int isdel { get; set; }
        public int? product { get; set; }
        public string? lot { get; set; }
        public int? supplier { get; set; }
        public int? Client { get; set; }
        public int? splace { get; set; }
        public int? warehouse { get; set; }
        public int? luser { get; set; }
        public int? delivery { get; set; }
        public int? lparam1 { get; set; }
        public int? lparam2 { get; set; }
        public int? lparam3 { get; set; }
        public double? weightp { get; set; }
        public double? cntp { get; set; }
        public double? weight { get; set; }
        public double? cnt { get; set; }
        public double? weight_w { get; set; }
        public double? cnt_w { get; set; }
        public double? valplan { get; set; }
        public double? valfact { get; set; }
        public DateTime? ETD_plan { get; set; }
        public DateTime? ETD_FACT { get; set; }
        public DateTime? ETA_plan { get; set; }
        public DateTime? ETA_fact { get; set; }
        public string? transi { get; set; }
        public double? vallogist { get; set; }
        
        public double? pay1_sum { get; set; }
        public string? pay1_bill { get; set; }
        public DateTime? pay1_dplan { get; set; }
        public DateTime? pay1_dfact { get; set; }
        
        public double? pay2_sum { get; set; }
        public string? pay2_bill { get; set; }
        public DateTime? pay2_dplan { get; set; }
        public DateTime? pay2_dfact { get; set; }

        public double? pay3_sum { get; set; }
        public string? pay3_bill { get; set; }
        public DateTime? pay3_dplan { get; set; }
        public DateTime? pay3_dfact { get; set; }
        
        public string? docs { get; set; }
        public DateTime? doc_dplan { get; set; }
        public DateTime? doc_dfact { get; set; }
        public DateTime? dparam1 { get; set; }   
        public DateTime? dparam2 { get; set; }
        public double? fparam1 { get; set; }
        public double? fparam2 { get; set; }
        public string? sparam1 { get; set; }
        public string? sparam2 { get; set; }
        public string? cmnt { get; set; }
        public string? lcmnt { get; set; }
        public DateTime? chdate { get; set; }
        public int? cuser { get; set; }
        public int clnum { get; set; }
    }
}
