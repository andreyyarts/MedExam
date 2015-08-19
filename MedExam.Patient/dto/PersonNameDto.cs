namespace MedExam.Patient.dto
{
    public class PersonNameDto
    {
        private string[] _names;

        public string LastName { get; set; }
        public string FirstNameAndMiddleName { get; set; }

        public string FirstName
        {
            get { return Names.Length > 0 ? Names[0] : ""; }
        }

        public string MiddleName
        {
            get { return Names.Length > 1 ? Names[1] : ""; }
        }

        public string FullName
        {
            get { return string.Concat(LastName, " ", FirstNameAndMiddleName); }
        }

        private string[] Names
        {
            get
            {
                return _names ?? (_names = !string.IsNullOrEmpty(FirstNameAndMiddleName)
                    ? FirstNameAndMiddleName.Split(' ', '.', ',')
                    : new string[0]);
            }
        }
    }
}