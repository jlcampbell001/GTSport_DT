using System;

namespace GTSport_DT.Cars
{
    public class CarCategory
    {
        public static readonly Category Empty = new Category("", "");

        public static readonly Category GR1 = new Category("GR.1", "13GR1");

        public static readonly Category GR3 = new Category("GR.3", "12GR3");

        public static readonly Category GR4 = new Category("GR.4", "11GR4");

        public static readonly Category GRB = new Category("GR.B", "14GRB");

        public static readonly Category GRX = new Category("GR.X", "15GRX");

        public static readonly Category Max = new Category("ZZZZZ", "ZZZZZZ");

        public static readonly Category N100 = new Category("N100", "01N100");

        public static readonly Category N1000 = new Category("N1000", "10N1000");

        public static readonly Category N200 = new Category("N200", "02N200");

        public static readonly Category N300 = new Category("N300", "03N300");

        public static readonly Category N400 = new Category("N400", "04N400");

        public static readonly Category N500 = new Category("N500", "05N500");

        public static readonly Category N600 = new Category("N600", "06N600");

        public static readonly Category N700 = new Category("N700", "07N700");

        public static readonly Category N800 = new Category("N800", "08N800");

        public static readonly Category N900 = new Category("N900", "09N900");

        public static Category[] categories = {Empty, N100, N200, N300, N400, N500, N600, N700, N800, N900, N1000, GR4, GR3, GR1, GRB, GRX };

        public static Category GetCategoryByDBValue(string dbValue)
        {
            Category foundCategory = Empty;

            foreach(Category category in categories)
            {
                if (category.DBValue == dbValue)
                {
                    foundCategory = category;
                    break;
                }
            }

            return foundCategory;
        }

        public static Category GetCategoryByDescription(string description)
        {
            Category foundCategory = Empty;

            foreach (Category category in categories)
            {
                if (category.Description == description)
                {
                    foundCategory = category;
                    break;
                }
            }

            return foundCategory;
        }

        public class Category : IComparable
        {
            public string DBValue = "";
            public string Description = "";

            public Category()
            {
            }

            public Category(string description, string dBValue)
            {
                Description = description ?? throw new ArgumentNullException(nameof(description));
                DBValue = dBValue ?? throw new ArgumentNullException(nameof(dBValue));
            }

            public int CompareTo(object obj)
            {
                int compareResult = 1;
                if (obj != null)
                {

                    Category otherCategory = obj as Category;

                    if (otherCategory != null)
                    {
                        compareResult = this.DBValue.CompareTo(otherCategory.DBValue);
                    }
                    else
                    {
                        throw new ArgumentException("Object is not a car category.");
                    }
                }

                return compareResult;
            }

            public override string ToString()
            {
                return Description;
            }
        }
    }
}