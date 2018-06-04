using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommitmentsDataGen.Models;

namespace CommitmentsDataGen.Helpers
{
    public static class DataHelper
    {
        private static List<string> _firstNames;
        private static List<string> FirstNames => _firstNames ?? (_firstNames = FileHelper.Get("FirstNames"));
        private static List<string> _lastNames;
        private static List<string> LastNames => _lastNames ?? (_lastNames = FileHelper.Get("LastNames"));

        private static readonly List<string> _generatedULNs;
        private static readonly List<TrainingCourse> _trainingCourses;
        private static List<TrainingCourse> TrainingCourses => _trainingCourses ?? FileHelper.GetTrainingCourses("TrainingCourses");

        static DataHelper()
        {
            _generatedULNs = new List<string>();


            //_trainingCourses = new List<TrainingCourse>
            //{
            //    new TrainingCourse { Id = "500-2-1", Title = "Property Services: Sale of Residential Property, Level: 3"},
            //    new TrainingCourse {Id = "1", Title = "Network engineer" },
            //};
        }

        public static string GetRandomFirstName()
        {
            return FirstNames.RandomElement().Trim();
        }

        public static string GetRandomLastName()
        {
            return LastNames.RandomElement().Trim();
        }


        public static string GetRandomUniqueULN()
        {
            while (true)
            {
                var uln = GenerateULN();
                if (uln.Length != 10) continue;
                if (!_generatedULNs.Contains(uln))
                {
                    _generatedULNs.Add(uln);
                    return uln;
                }
            }
        }

        private static string GenerateULN()
        {
            var result = new StringBuilder();

            var firstdigit = (RandomHelper.GetRandomNumber(8) + 1).ToString();
            result.Append(firstdigit);

            for (var c = 0; c < 8; c++)
            {
                var digit = RandomHelper.GetRandomNumber(9).ToString();
                result.Append(digit);
            }

            var checkdigit = CalculateUlnChecksum(result.ToString());

            return result + checkdigit;
        }

        private static string CalculateUlnChecksum(string partialUln)
        {
            var ulnCheckArray = partialUln.ToCharArray()
                                    .Select(c => long.Parse(c.ToString()))
                                    .ToList();

            var multiplier = 10;
            long sumOfDigits = 0;
            for (var i = 0; i < 9; i++)
            {
                sumOfDigits += ulnCheckArray[i] * multiplier;
                multiplier--;
            }

            var remainder = (505 - sumOfDigits);

            var checkDigit = remainder % 11;

            return checkDigit.ToString();
        }

        public static DateTime GetRandomDateOfBirth()
        {
            var year = 1960 + RandomHelper.GetRandomNumber(40);
            var month = RandomHelper.GetRandomNumber(11) + 1;
            var day = RandomHelper.GetRandomNumber(27) + 1;

            return new DateTime(year, month, day);
        }

        public static DateTime GetRandomStartDate()
        {
            var result = DateTime.Now.AddMonths(-6);
            result = result.AddMonths(RandomHelper.GetRandomNumber(12));
            return new DateTime(result.Year, result.Month, 1);
        }

        public static DateTime GetRandomStartDateInFuture()
        {
            var result = DateTime.Now.AddMonths(1);
            result = result.AddMonths(RandomHelper.GetRandomNumber(6));
            return new DateTime(result.Year, result.Month, 1);
        }

        public static DateTime GetRandomStartDateInPast()
        {
            var result = DateTime.Now;
            result = result.AddMonths(-RandomHelper.GetRandomNumber(6));
            return new DateTime(result.Year, result.Month, 1);
        }


        public static DateTime? GetRandomEndDate(DateTime? startDate)
        {
            if (!startDate.HasValue)
            {
                return null;
            }

            var duration = RandomHelper.GetRandomNumber(3) + 9;
            return startDate.Value.AddMonths(duration);
        }


        public static TrainingCourse GetRandomTrainingCourse(TrainingProgrammeTypeFilter filter = TrainingProgrammeTypeFilter.All)
        {
            var courses = TrainingCourses;

            switch (filter)
            {
                case TrainingProgrammeTypeFilter.Standard:
                    courses = TrainingCourses.FindAll(x => x.IsStandard);
                    break;
                case TrainingProgrammeTypeFilter.Framework:
                    courses = TrainingCourses.FindAll(x => !x.IsStandard);
                    break;
                default:
                    break;
            }

            return courses.RandomElement();
        }

    }
}
