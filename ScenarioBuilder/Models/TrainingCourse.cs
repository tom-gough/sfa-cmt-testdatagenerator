namespace ScenarioBuilder.Models
{
    public class TrainingCourse
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string TitleRestrictedLength
        {
            get
            {
                if (Title.Length > 125)
                {
                    return Title.Substring(0, 125);
                }

                return Title;
            }
        }

        public bool IsStandard => !Id.Contains("-");

        public int TrainingType => IsStandard ? 0 : 1;
    }

}
