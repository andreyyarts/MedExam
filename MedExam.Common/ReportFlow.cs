using System.Collections.Generic;
using MedExam.Common.Interfaces;

namespace MedExam.Common
{
    public abstract class ReportFlow<T> : ReportFlowBase where T : IReportData
    {
        protected abstract IEnumerable<T> GetDatas();

        public override IEnumerable<IReportData> Datas
        {
            get { return (IEnumerable<IReportData>)GetDatas(); }
        }
    }
}