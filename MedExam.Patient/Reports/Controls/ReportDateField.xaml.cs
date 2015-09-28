using System;
using System.Windows;

namespace MedExam.Patient.Reports.Controls
{
    /// <summary>
    /// Interaction logic for ReportField.xaml
    /// </summary>
    public partial class ReportDateField
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(DateTime?), typeof(ReportDateField), new PropertyMetadata(default(DateTime?)));

        public ReportDateField()
        {
            InitializeComponent();
        }

        public DateTime? Value
        {
            get { return (DateTime?) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
