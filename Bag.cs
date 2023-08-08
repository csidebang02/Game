using System;
using System.Collections.Generic;

namespace ScrabbleGame
{
    class Bag
    {
        private string letters;

        public Bag(string initialLetters)
        {
            letters = initialLetters;
        }

        public string DrawLetters(int count)
        {
            if (count > letters.Length)
            {
                count = letters.Length;
            }

            string drawnLetters = letters.Substring(0, count);
            letters = letters.Remove(0, count);

            return drawnLetters;
        }

        public void RefillLetters(string discardedLetters)
        {
            letters += discardedLetters;
        }
    }
}
