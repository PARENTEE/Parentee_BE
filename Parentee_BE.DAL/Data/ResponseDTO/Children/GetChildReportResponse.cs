namespace Parentee_BE.DAL.Data.ResponseDTO.Children;

public class ReportTimeBlock
{
    public String Message { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public class GetChildReportResponse
{
    public List<ReportTimeBlock> Feedings { get; set; }
    public List<ReportTimeBlock> SolidFood { get; set; }
    public List<ReportTimeBlock> DiaperChanges { get; set; }
    public List<ReportTimeBlock> Sleep { get; set; }
}

