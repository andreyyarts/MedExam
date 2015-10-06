using System;

namespace MedExam.Patient.dto
{
    public class PatientDto
    {
        public long Id { get; set; }
        public PersonNameDto PersonName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public PolicyDto PolicyDto { get; set; }
        public Gender Gender { get; set; }
        public OrganizationDto Organization { get; set; }
    }
}