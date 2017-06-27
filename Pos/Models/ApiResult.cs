using System;
using System.Collections.Generic;

namespace Pos.Models
{
	public class Error
	{
		public int Code { get; set; }
		public string Message { get; set; }
	}

	public class ListResult<T>
	{
		public int TotalCount { get; set; }
		public IEnumerable<T> Items { get; set; }
	}

	public class ApiResult<T>
	{
		public T Result { get; set; }
		public bool Success { get; set; }
		public Error Error { get; set; }
	}
}

