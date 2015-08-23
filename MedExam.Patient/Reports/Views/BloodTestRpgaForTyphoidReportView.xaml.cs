namespace MedExam.Patient.Reports.Views
{
    /// <summary>
    /// Interaction logic for BloodTestRpgaForTyphoidReportView.xaml
    /// </summary>
    public partial class BloodTestRpgaForTyphoidReportView
    {
        public BloodTestRpgaForTyphoidReportView(/*DirectionInImmunologyLaboratoryReportViewModel data*/)
        {
            InitializeComponent();

            //LoadData(data);
        }

        /*private void LoadData(DirectionInImmunologyLaboratoryReportViewModel data)
        {
            ReportSection.DataContext = data;

//            foreach (var patient in data)
//            {
//                var tableRow = new TableRow();
//                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(patient.FirstName))));
//                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(patient.LastName))));
//                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(patient.Gender))));
//                PatientsGroup.Rows.Add(tableRow);
//            }
        }*/
    }
}
