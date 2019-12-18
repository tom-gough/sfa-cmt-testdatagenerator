using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Helpers
{
    public static class TextHelper
    {
        private static List<string> _loremIpsumText;
        private static List<string> LoremIpsumText => _loremIpsumText ?? (_loremIpsumText = FileHelper.Get("LoremIpsum"));

        public static string GetRandomText(int words)
        {
            var wordCount = 0;
            var wordPointer = GetStartingPoint();
            
            var result = new StringBuilder();

            while (wordCount < words)
            {
                var nextWord = LoremIpsumText[wordPointer];

                if (nextWord != "." & nextWord != ",")
                {
                    result.Append(" ");
                }

                result.Append(nextWord);


                wordCount++;
                wordPointer++;
                if (wordPointer >= LoremIpsumText.Count)
                {
                    wordPointer = 0;
                }
            }


            return result.ToString();
        }

        private static int GetStartingPoint()
        {
            var r = LoremIpsumText.RandomElement();
            var i = LoremIpsumText.IndexOf(r);

            var word = LoremIpsumText[i];
            while (word != ".")
            {
                i++;
                if (i >= LoremIpsumText.Count)
                {
                    i = 0;
                }

                word = LoremIpsumText[i];
            }

            //Go to next word
            i++;
            if (i >= LoremIpsumText.Count)
            {
                i = 0;
            }


            return i;
        }
    }
}
