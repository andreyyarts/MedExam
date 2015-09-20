using System.Collections.Generic;
using System.Linq;
using MedExam.Common.Interfaces;

namespace MedExam.Common.LocalSettings
{
    public class ReportPropertiesSettings
    {
        public SettingPair[] Reports { get; set; }

        public void SetReports(IEnumerable<IReportFlow> reports)
        {
            Reports = reports.Select(r => new SettingPair { Key = r.GetType().Name, Value = r.Title }).ToArray();
        }
    }
}