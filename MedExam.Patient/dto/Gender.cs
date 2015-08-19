namespace MedExam.Patient.dto
{
    public enum Gender
    {
        Male,
        Female
    }

    public static class GenderExt
    {
        public static string Text(this Gender gender)
        {
            return gender == Gender.Female ? "Ж" : "М";
        }
    }
}