namespace MedExam.Patient.Reports.Views
{
    /// <summary>
    /// Interaction logic for DirectionInImmunologyLaboratoryReportView.xaml
    /// </summary>
    public partial class DirectionInImmunologyLaboratoryReportView
    {
        public DirectionInImmunologyLaboratoryReportView()
        {
            InitializeComponent();

            //LoadData(data);
        }

        /*private void LoadData(PatientReportDto[] data)
        {
            DataContext = data;

            foreach (var patient in data)
            {
                var tableRow = new TableRow();
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(patient.FirstName))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(patient.LastName))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(patient.Gender))));
                PatientsGroup.Rows.Add(tableRow);
            }
        }*/
    }
}
