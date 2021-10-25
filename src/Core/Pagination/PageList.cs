using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Services.Orders.Core.Pagination
{
	public class PageList<T>
	{
		public int TotalPages { get; set; }
		public int TotalElements { get; set; }
		public bool Last { get; set; }
		public bool First { get; set; }
		public int Size { get; set; }
		public int NumberOfElements { get; set; }
		public bool Empty { get; set; }
		public IEnumerable<T> Content { get; set; }

		public PageList() {  }

		public PageList(List<T> items, int count, int page, int size)
		{
			TotalPages = (int)Math.Ceiling(count / (double)size);
			TotalElements = count;
			Last = page < TotalPages;
			First = page == 1;
			Size = size;
			NumberOfElements = items.Count();
			Empty = NumberOfElements == 0;
			Content = items;
		}
	}
}
