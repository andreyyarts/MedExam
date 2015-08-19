using System;

namespace MedExam.Patient.dto
{
    public class PolicyDto
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime? DateFrom { get; set; }
    }
}