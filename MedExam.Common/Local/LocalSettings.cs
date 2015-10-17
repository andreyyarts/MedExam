namespace MedExam.Common.Local
{
    public class LocalSettings
    {
        public LocalSettings()
        {
            OrganizationName = string.Concat("ГБУЗ ТО ", SpecialChars.Laquo, "ОКБ №1", SpecialChars.Raquo);
            DepartmentName = "ОТДЕЛЕНИЕ ПРОФОСМОТРОВ";
            MedExamDoctorName = "Куликова Л.В.";
        }

        public string OrganizationName { get; set; }
        public string DepartmentName { get; set; }
        public string MedExamDoctorName { get; set; }
    }
}
