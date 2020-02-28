using System;

namespace GTSport_DT.Cars
{
    /// <summary>The valid values for a car category.</summary>
    public class CarCategory
    {
        /// <summary>An empty car category.</summary>
        public static readonly Category Empty = new Category("", "");

        /// <summary>The GR1 category.</summary>
        public static readonly Category GR1 = new Category("GR.1", "14GR1");

        /// <summary>The GR2 category.</summary>
        /// Had to insert this value so I have to leave the sort start number as 13 to make it sort properly.
        public static readonly Category GR2 = new Category("GR.2", "13GR2");

        /// <summary>The GR3 category.</summary>
        public static readonly Category GR3 = new Category("GR.3", "12GR3");

        /// <summary>The GR4 category.</summary>
        public static readonly Category GR4 = new Category("GR.4", "11GR4");

        /// <summary>The GRB category.</summary>
        public static readonly Category GRB = new Category("GR.B", "15GRB");

        /// <summary>The GRX category.</summary>
        public static readonly Category GRX = new Category("GR.X", "16GRX");

        /// <summary>Determines the maximum of the parameters.</summary>
        public static readonly Category Max = new Category("ZZZZZ", "ZZZZZZ");

        /// <summary>The N100 category.</summary>
        public static readonly Category N100 = new Category("N100", "01N100");

        /// <summary>The N1000 category.</summary>
        public static readonly Category N1000 = new Category("N1000", "10N1000");

        /// <summary>The N200 category.</summary>
        public static readonly Category N200 = new Category("N200", "02N200");

        /// <summary>The N300 category.</summary>
        public static readonly Category N300 = new Category("N300", "03N300");

        /// <summary>The N400 category.</summary>
        public static readonly Category N400 = new Category("N400", "04N400");

        /// <summary>The N500 category.</summary>
        public static readonly Category N500 = new Category("N500", "05N500");

        /// <summary>The N600 category.</summary>
        public static readonly Category N600 = new Category("N600", "06N600");

        /// <summary>The N700 category.</summary>
        public static readonly Category N700 = new Category("N700", "07N700");

        /// <summary>The N800 category.</summary>
        public static readonly Category N800 = new Category("N800", "08N800");

        /// <summary>The N900 category.</summary>
        public static readonly Category N900 = new Category("N900", "09N900");

        /// <summary>This is just a place holder for the total like on statistics.</summary>
        public static readonly Category Total = new Category("Total", "ZZZZZZ");

        /// <summary>The list of categories for drop down lists.</summary>
        public static Category[] categories = { Empty, N100, N200, N300, N400, N500, N600, N700, N800, N900, N1000, GR4, GR3, GR2, GR1, GRB, GRX };

        /// <summary>Gets the category by database value.</summary>
        /// <param name="dbValue">The database value.</param>
        /// <returns>The found category if found or the Empty category if not found.</returns>
        public static Category GetCategoryByDBValue(string dbValue)
        {
            Category foundCategory = Empty;

            foreach (Category category in categories)
            {
                if (category.DBValue == dbValue)
                {
                    foundCategory = category;
                    break;
                }
            }

            return foundCategory;
        }

        /// <summary>Gets the category by description.</summary>
        /// <param name="description">The description.</param>
        /// <returns>The car category if found or the empty car category if not found.</returns>
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

        /// <summary>A car category.</summary>
        /// <seealso cref="System.IComparable"/>
        public class Category : IComparable
        {
            /// <summary>
            /// <para>The database value.</para>
            /// <para>This is the value set in the database and is sortable.</para>
            /// </summary>
            public string DBValue = "";

            /// <summary>The description seen by users.</summary>
            public string Description = "";

            /// <summary>Initializes a new instance of the <see cref="Category"/> class.</summary>
            public Category()
            {
            }

            /// <summary>Initializes a new instance of the <see cref="Category"/> class.</summary>
            /// <param name="description">The description.</param>
            /// <param name="dBValue">The database value.</param>
            /// <exception cref="ArgumentNullException">description or dBValue</exception>
            public Category(string description, string dBValue)
            {
                Description = description ?? throw new ArgumentNullException(nameof(description));
                DBValue = dBValue ?? throw new ArgumentNullException(nameof(dBValue));
            }

            /// <summary>
            /// Compares the current instance with another object of the same type and returns an
            /// integer that indicates whether the current instance precedes, follows, or occurs in
            /// the same position in the sort order as the other object.
            /// </summary>
            /// <param name="obj">An object to compare with this instance.</param>
            /// <returns>
            /// A value that indicates the relative order of the objects being compared. The return
            /// value has these meanings: Value Meaning Less than zero This instance precedes
            /// <paramref name="obj"/> in the sort order. Zero This instance occurs in the same
            /// position in the sort order as <paramref name="obj"/>. Greater than zero This
            /// instance follows <paramref name="obj"/> in the sort order.
            /// </returns>
            /// <exception cref="ArgumentException">Object is not a car category.</exception>
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

            /// <summary>Converts to string.</summary>
            /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
            public override string ToString()
            {
                return Description;
            }
        }
    }
}