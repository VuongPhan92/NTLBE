using System;
using System.Linq;

namespace Core.Sort
{
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }

    public interface ISortCriteria<T>
    {
        SortDirection Direction { get; set; }

        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, Boolean useThenBy);
    }
}