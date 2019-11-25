using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class PaginatedList<T> : List<T> where T : class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        private int _totalItemsCount;
        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set => _totalItemsCount = value >= 0 ? value : 0;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => TotalItemsCount / PageSize + (TotalItemsCount % PageSize > 0 ? 1 : 0);

        /// <summary>
        /// 是否有前一页
        /// </summary>
        public bool HasPrevious => PageIndex > 0;
        /// <summary>
        /// 是否有后一页
        /// </summary>
        public bool HasNext => PageIndex < PageCount - 1;

        public PaginatedList(int pageIndex, int pageSize, int totalItemsCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }
    }
}