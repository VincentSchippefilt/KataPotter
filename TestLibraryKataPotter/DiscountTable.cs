using System.Collections.Generic;
using System.Diagnostics;

namespace TestLibraryKataPotter
{
    public static class DiscountTable
    {
        private static List<double> _discounts = new List<double>()
        {
            1,0.95,0.90,0.80,0.75
        };

        public static double GetDiscount(int numberOfBooksInTheSet)
        {
            Debug.Assert(numberOfBooksInTheSet > 0);
            Debug.Assert(numberOfBooksInTheSet < 6);
            return _discounts[numberOfBooksInTheSet - 1];
        }
    }
}