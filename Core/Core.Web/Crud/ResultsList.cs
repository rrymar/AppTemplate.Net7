using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Web.Crud
{
    public class ResultsList<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; } = new List<T>();

        public ResultsList(List<T> items, int totalCount)
        {
            TotalCount = totalCount;
            Items = items;
        }

        public ResultsList()
        {
        }
    }
}
