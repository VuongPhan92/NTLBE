using System;
using System.Linq;

namespace Core.Sort
{
    //-----------------------------------------------------------------------
    /// <summary>
    /// Represents a sort expression where a property of the sequence item is sorted upon.
    /// Useful to avoid case statement when doing "simple" sorts by "simple" property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortCriteria<T> : ISortCriteria<T> where T : class
    {
        //-----------------------------------------------------------------------
        public String Name { get; set; }

        //-----------------------------------------------------------------------
        public SortDirection Direction { get; set; }

        //-----------------------------------------------------------------------
        public SortCriteria()
        {
            this.Direction = SortDirection.Ascending;
        }

        //-----------------------------------------------------------------------
        public SortCriteria(String name, SortDirection direction)
            : base()
        {
            this.Name = name;
            this.Direction = direction;
        }

        //-----------------------------------------------------------------------

        public IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, bool useThenBy)
        {
            IOrderedQueryable<T> result = null;
            var sortOrder = this.Direction == SortDirection.Descending;
            result = !useThenBy ? query.OrderBy(Name, sortOrder) : query.ThenBy(Name, sortOrder);
            return result;
        }
    }
}