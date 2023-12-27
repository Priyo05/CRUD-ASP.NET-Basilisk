namespace Basilisk.Presentation.Web.ViewModels;
public class ProductDetailsViewModel
{
    public long Id { get; set; }
    public ProductViewModel? ProductViewModel { get; set; }
    public List<YearlyReportViewModel> YearlyReports { get; set;}
    public List<MonthlyReportViewModel> MonthlyReports { get; set;}

}

