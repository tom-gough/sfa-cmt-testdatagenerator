﻿using System.Collections.Generic;
using System.Linq;
using CommitmentsDataGen.Models;
using Newtonsoft.Json;

namespace CommitmentsDataGen
{
    public static class FileHelper
    {
        public static List<string> Get(string source)
        {
            var file = System.IO.File.ReadAllLines($"Data\\{source}.txt");
            return file.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        public static List<TrainingCourse> GetTrainingCourses(string source)
        {
            var file = System.IO.File.ReadAllText($"Data\\{source}.json");

            var result = JsonConvert.DeserializeObject< List<TrainingCourse>>(file);

            return result;
        }
    }
}
