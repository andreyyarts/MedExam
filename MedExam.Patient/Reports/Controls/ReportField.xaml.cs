using System.Windows;

namespace MedExam.Patient.Reports.Controls
{
    /// <summary>
    /// Interaction logic for ReportField.xaml
    /// </summary>
    public partial class ReportField
    {
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping),
            typeof(ReportField), new FrameworkPropertyMetadata(TextWrapping.NoWrap,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), IsValidTextWrap);

        public ReportField()
        {
            InitializeComponent();
        }

        public TextWrapping TextWrapping
        {
            get
            {
                return (TextWrapping)GetValue(TextWrappingProperty);
            }
            set
            {
                SetValue(TextWrappingProperty, value);
            }
        }

        private static bool IsValidTextWrap(object o)
        {
            var textWrapping = (TextWrapping)o;
            switch (textWrapping)
            {
                case TextWrapping.NoWrap:
                    return true;
                default:
                    return textWrapping == TextWrapping.Wrap;
            }
        }
    }
}
