using System.ComponentModel;

namespace MedExam.Patient.dto
{
    public class PersonNameDto
    {
        [Description("Фамилия")]
        public string LastName { get; set; }
        [Description("Имя и Отчество")]
        public string FirstNameAndMiddleName { get; set; }
    }
}