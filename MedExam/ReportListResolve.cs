using System.Linq;
using MedExam.Common.Interfaces;
using MedExam.Patient.Reports;
using Microsoft.Practices.Unity;

namespace MedExam
{
    public static class ReportListResolve
    {
        public static IReportFlow[] BuildReports(IUnityContainer container)
        {
            return new IReportFlow[]
            {
                container.Resolve<BloodTestRpgaForTyphoidReport>(),
                container.Resolve<SpecificTumorMarkerPsaReport>(),
                container.Resolve<SpecificTumorMarkerCa125Report>(),
                container.Resolve<ClinicalTrialEnterobiosisReport>(),
                container.Resolve<ClinicalTrialReport>(),
                container.Resolve<DirectionInBacteriologicalLaboratoryReport>()
            }
                .OrderBy(r => r.Title)
                .ToArray();
        }
    }
}