using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace JustWatch.Selenium.Utils
{
    public class RandomSelector
    {
        private readonly RandomNumberGenerator _numberGenerator; 

        public RandomSelector()
        {
            _numberGenerator = RandomNumberGenerator.Create();
        }

        public T Select<T>(IEnumerable<T> elements)
        {
            var bytes = new byte[sizeof(int)];
            _numberGenerator.GetBytes(bytes);
            var randomInteger = BitConverter.ToInt32(bytes, 0);
            var randomDouble = Math.Abs(randomInteger) / (double)int.MaxValue;

            var count = elements.Count();
            if (count == 0)
                throw new Exception("Collection should contain elements");

            var randomIndex = (int)Math.Floor(count * randomDouble);
            return elements.ElementAt(randomIndex);
        }
    }
}
