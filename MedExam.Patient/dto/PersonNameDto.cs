using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace MedExam.Patient.dto
{
    public class PersonNameDto
    {
        private string[] _names;
        private string _lastName;

        public string LastName
        {
            get { return ToUpperFirstWithLowerEnd(_lastName); }
            set { _lastName = value; }
        }

        public string FirstNameAndMiddleName { get; set; }

        public string FirstName
        {
            get { return Names.Length > 0 ? ToUpperFirstWithLowerEnd(Names[0]) : ""; }
        }

        public string MiddleName
        {
            get { return Names.Length > 1 ? ToUpperFirstWithLowerEnd(Names[1]) : ""; }
        }

        public string FullName
        {
            get { return string.Concat(LastName, " ", FirstName, " ", MiddleName); }
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

        private static string ToUpperFirstWithLowerEnd(string input)
        {
            switch (input.Length)
            {
                case 0:
                    return "";
                case 1:
                    return input.ToUpper();
                default:
                    return input.First().ToString().ToUpper() + input.Skip(1).SelectMany(ToLower).JoinStrings("");
            }
        }

        private static string ToLower(char ch)
        {
            return ch.ToString().ToLower();
        }
    }
}