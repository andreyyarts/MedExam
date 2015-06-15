using System.ComponentModel;

namespace MedExam.Patient.ViewModels
{
    public class PersonName
    {
        [Description("Фамилия")]
        public string LastName { get; set; }
        [Description("Имя")]
        public string FirstName { get; set; }
        [Description("Отчество")]
        public string MiddleName { get; set; }
    }
}