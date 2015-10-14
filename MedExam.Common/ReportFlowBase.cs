using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using MedExam.Common.Interfaces;

namespace MedExam.Common
{
    public abstract class ReportFlowBase : IReportFlow
    {
        public abstract string Name { get; set; }
        public abstract string Title { get; set; }
        public abstract string Template { get; set; }
        public abstract int CountInWidth { get; }
        public abstract int CountInHeight { get; }
        public abstract IEnumerable<IReportData> Datas { get; }

        public ReportFlowViewBase Report
        {
            get
            {
                ReportFlowViewBase view;
                using (var stream = File.OpenRead(string.Format("Templates/{0}.xaml", Template)))
                {
                    view = (ReportFlowViewBase)XamlReader.Load(stream);
                    stream.Close();
                }

                return view;
            }
        }

        public abstract void SetItems(long[] itemIds);
    }
}