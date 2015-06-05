using System;
using System.ComponentModel;

namespace MedExam.Patient.dto
{
    public class Policy
    {
        [Description("Серия")]
        public string Series { get; set; }
        [Description("Номер")]
        public string Number { get; set; }
        [Description("Дата с")]
        public DateTime? DateFrom { get; set; }
    }
}