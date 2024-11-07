namespace Application.Extensions;

static partial class Extensions
{
	/// <summary>Paginates the elements of an <see cref="IQueryable{TSource}"/> based on the specified page number and page size.</summary>
	/// <typeparam name="TSource">The type of the elements of the source.</typeparam>
	/// <param name="source">The source <see cref="IQueryable{TSource}"/> to paginate.</param>
	/// <param name="page">The page number to retrieve. Must be greater than or equal to 1.</param>
	/// <param name="pageSize">The number of elements to return per page.  Must be greater than or equal to 1.</param>
	/// <returns>An <see cref="IQueryable{TSource}"/> of elements from the specified page.</returns>
	public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize) => source.Skip((page - 1) * pageSize).Take(pageSize);

	/// <summary>Paginates the elements of an <see cref="IEnumerable{TSource}"/> based on the specified page number and page size.</summary>
	/// <typeparam name="TSource">The type of the elements of the source.</typeparam>
	/// <param name="source">The source <see cref="IEnumerable{TSource}"/> to paginate.</param>
	/// <param name="page">The page number to retrieve. Must be greater than or equal to 1.</param>
	/// <param name="pageSize">The number of elements to return per page.  Must be greater than or equal to 1.</param>
	/// <returns>An <see cref="IEnumerable{TSource}"/> of elements from the specified page.</returns>
	public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize) => source.Skip((page - 1) * pageSize).Take(pageSize);
}
