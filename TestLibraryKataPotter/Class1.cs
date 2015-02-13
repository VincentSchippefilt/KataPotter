using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLibraryKataPotter
{
    [TestClass]
    public class Class1
    {
        private ShoppingBasket _basket;

        [TestInitialize]
        public void Setup()
        {
            _basket = new ShoppingBasket();
        }

        [TestMethod]
        public void WhenCondition_ThenExpectedResult()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WhenBying0Books_TheTheCostIs0()
        {
            Assert.AreEqual(0, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBying1TimeTheBook1_TheTheCostIs8()
        {
            _basket.AddBook(BookAboutHarry.First);
            Assert.AreEqual(8, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBuying2TimesBook1_ThenTheCostIs16()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.First);
            Assert.AreEqual(16, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBuying2DifferentBooks_ThenYouGet5PercentDiscount()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            Assert.AreEqual(15.2, _basket.ComputeBestPrice());
        }


        [TestMethod]
        public void WhenBuying3DifferentBooks_ThenYouGet10PercentDiscount()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            _basket.AddBook(BookAboutHarry.Third);
            Assert.AreEqual(21.6, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBuying4DifferentBooks_ThenTouGet20PercentDiscount()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            _basket.AddBook(BookAboutHarry.Third);
            _basket.AddBook(BookAboutHarry.Fourth);
            Assert.AreEqual(25.6, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBuying5DifferentBooks_ThenTouGet25PercentDiscount()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            _basket.AddBook(BookAboutHarry.Third);
            _basket.AddBook(BookAboutHarry.Fourth);
            _basket.AddBook(BookAboutHarry.Fifth);
            Assert.AreEqual(30, _basket.ComputeBestPrice());

        }

        [TestMethod]
        public void WhenBuying2TimesBook1And1TimeBook2_ThenWeGetADiscountForTheTwoAndPayTheFullPriceForTheOne()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            Assert.AreEqual(23.2, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBuying3TimesBook1And1TimeBook2_ThenWeGetADiscountForTheTwoAndPayTheFullPriceForTheOne()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            Assert.AreEqual(31.2, _basket.ComputeBestPrice());
        }


        [TestMethod]
        public void WhenBuying2TimesBook1And2TimeBook2_ThenWeGetADiscountForTheTwoAndAsWellForTheOtherTwo()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            _basket.AddBook(BookAboutHarry.Second);
            Assert.AreEqual(30.4, _basket.ComputeBestPrice());
        }

        [TestMethod]
        public void WhenBuying8Books_ThenGetTheBiggestDiscountPossible()
        {
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.First);
            _basket.AddBook(BookAboutHarry.Second);
            _basket.AddBook(BookAboutHarry.Second);
            _basket.AddBook(BookAboutHarry.Third);
            _basket.AddBook(BookAboutHarry.Third);
            _basket.AddBook(BookAboutHarry.Fourth);
            _basket.AddBook(BookAboutHarry.Fifth);

            Assert.AreEqual(51.20, _basket.ComputeBestPrice());
        }

    }

    public class BookTypeCounter : Dictionary<BookAboutHarry, int>
    {
        public BookTypeCounter()
        {
            Add(BookAboutHarry.First, 0);
            Add(BookAboutHarry.Second, 0);
            Add(BookAboutHarry.Third, 0);
            Add(BookAboutHarry.Fourth, 0);
            Add(BookAboutHarry.Fifth, 0);
        }

        public void AddBook(BookAboutHarry bookAboutHarry)
        {
            this[bookAboutHarry]++;
        }

        public int GetMaxNumberOfSameBook()
        {
            return this.Values.Max();
        }

        public bool HasMoreSets()
        {
            return Values.Any(x => x > 0);
        }

        public int GetNumberOfBookTypes(int i)
        {
            return Values.Count(x => x >= i);
        }
    }

    public class ShoppingBasket
    {
        private List<BookAboutHarry> _bookAboutHarries = new List<BookAboutHarry>();
        private BookTypeCounter _bookTypeCounter = new BookTypeCounter();

        public double ComputeBestPrice()
        {
            GridOfBooksFactory gridOfBooksFactory = new GridOfBooksFactory(_bookTypeCounter);
            List<GridOfBooks> allGrids = gridOfBooksFactory.GetAllGrids();
            return allGrids.Min(x => x.GetPrice());

            /*
            int maxNumberOfSameBook = _bookTypeCounter.GetMaxNumberOfSameBook();
            double totalPrice = 0;
            for (int i = maxNumberOfSameBook; i > 0; i--)
            {
                int numberOfBookTypesWithICopies = _bookTypeCounter.GetNumberOfBookTypes(i);
                totalPrice = totalPrice + (8 * numberOfBookTypesWithICopies * DiscountTable.GetDiscount(numberOfBookTypesWithICopies));
            }
            return totalPrice;
            */
            /*if (AreAllBooksTheSame())
            {
                return 8 * _bookAboutHarries.Count;
            }
            else
            {
                List<SetOfBooks> setsOfBooks = new List<SetOfBooks>();
                setsOfBooks.Add(new SetOfBooks());
                setsOfBooks.Add(new SetOfBooks());


                foreach (BookAboutHarry bookAboutHarry in _bookAboutHarries)
                {
                    if (!setsOfBooks[0].Contains(bookAboutHarry))
                    {
                        setsOfBooks[0].Add(bookAboutHarry);
                    }
                    else
                    {
                        setsOfBooks[1].Add(bookAboutHarry);
                    }
                }

                double totalAmount = 0.0;

                foreach (var setOfBooks in setsOfBooks)
                {
                    totalAmount += setOfBooks.GetTotalAmount();
                }
                return totalAmount;
            }*/
        }

        private bool AreAllBooksTheSame()
        {
            return _bookAboutHarries.Distinct().Count() <= 1;
        }

        public void AddBook(BookAboutHarry bookAboutHarry)
        {
            _bookAboutHarries.Add(bookAboutHarry);
            _bookTypeCounter.AddBook(bookAboutHarry);
        }
    }

    public class GridOfBooksFactory
    {

        private BookTypeCounter _bookTypeCounter;

        public GridOfBooksFactory(BookTypeCounter bookTypeCounter)
        {
            _bookTypeCounter = bookTypeCounter;
        }

        public List<GridOfBooks> GetAllGrids()
        {
            List<GridOfBooks> gridsOfBooks = new List<GridOfBooks>();





            return gridsOfBooks;
        }
    }

    public class SetOfBooks : List<BookAboutHarry>
    {
        public double GetTotalAmount()
        {
            int numberOfBooksInTheSet = this.Count;
            return 8 * numberOfBooksInTheSet * DiscountTable.GetDiscount(numberOfBooksInTheSet);
        }
    }

    public class GridOfBooks
    {
        public List<bool> FirstBookAboutHarries { get; set; }
        public List<bool> SeconBookAboutHarries { get; set; }
        public List<bool> ThirdBookAboutHarries { get; set; }
        public List<bool> FourthBookAboutHarries { get; set; }
        public List<bool> FifthBookAboutHarries { get; set; }
        private List<List<bool>> grid;

        public GridOfBooks(int maxNumberOfSameBooks)
        {
            FirstBookAboutHarries = new List<bool>();
            SeconBookAboutHarries = new List<bool>();
            ThirdBookAboutHarries = new List<bool>();
            FourthBookAboutHarries = new List<bool>();
            FifthBookAboutHarries = new List<bool>();

            grid = new List<List<bool>>
            {
                FirstBookAboutHarries,
                SeconBookAboutHarries,
                ThirdBookAboutHarries,
                FourthBookAboutHarries,
                FifthBookAboutHarries
            };

        }

        public double GetPrice()
        {
            
            
        }
    }
}

