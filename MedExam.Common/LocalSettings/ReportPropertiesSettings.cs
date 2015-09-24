using System.Collections.Generic;
using System.Linq;
using MedExam.Common.Interfaces;

namespace MedExam.Common.LocalSettings
{
    public class ReportPropertiesSettings
    {
        public ReportPropertiesSettings(IEnumerable<IReportFlow> reports)
        {
            ReportSettings = reports.Select(r => new ReportSetting
            {
                Type = r.GetType().Name,
                Title = r.Title,
                Template = r.Template
            }).ToArray();
        }

        public ReportPropertiesSettings()
        {
        }

        public ReportSetting[] ReportSettings { get; set; }
    }
}