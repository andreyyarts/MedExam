using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using MedExam.Common.Interfaces;

namespace MedExam.Common
{
    public abstract class ReportFlow : IReportFlow
    {
        public abstract string Title { get; }

        public abstract int CountInWidth { get; }

        public abstract int CountInHeight { get; }

        public virtual ReportFlowViewBase Report
        {
            get
            {
                ReportFlowViewBase view;
                using (var stream = File.OpenRead(string.Format("Templates/{0}.xaml", GetType().Name)))
                {
                    view = (ReportFlowViewBase)XamlReader.Load(stream);
                    view.Title = Title;
                    stream.Close();
                }

                return view;
            }
        }

        public abstract IEnumerable<object> Datas { get; }

        public abstract void SetItems(long[] itemIds);
    }
}