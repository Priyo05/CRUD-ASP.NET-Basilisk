namespace Basilisk.Presentation.Web.ViewModels;
public class MonthlyReportViewModel
{
    public long Id { get; set; }
    public int Year { get; set; }
    public string? Month { get; set; }
    public int Sold { get; set; }
    public decimal Total { get; set; }
}

