using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    interface IReportGenerator
    {
        double CalculateCompletionPercentage(User user);
        string GenerateSummaryReport(User user);
        void ExportReport(string report, string fileName);
        void ExportTaskList(User user, string fileName);
    }
}
