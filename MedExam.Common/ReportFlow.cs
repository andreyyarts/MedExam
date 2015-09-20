using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using MedExam.Common.Interfaces;

namespace MedExam.Common
{
    public abstract class ReportFlow<T> : IReportFlow where T : IReportData
    {
        private readonly string _template;

        protected ReportFlow(string template)
        {
            _template = template;
        }

        public abstract string Title { get; }
        public abstract int CountInWidth { get; }
        public abstract int CountInHeight { get; }
        public abstract IEnumerable<T> Datas { get; }

        public ReportFlowViewBase Report
        {
            get
            {
                ReportFlowViewBase view;
                using (var stream = File.OpenRead(string.Format("Templates/{0}.xaml", _template)))
                {
                    view = (ReportFlowViewBase)XamlReader.Load(stream);
                    view.Title = Title;
                    stream.Close();
                }

                return view;
            }
        }

        public abstract void SetItems(long[] itemIds);
    }
}