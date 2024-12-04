namespace Infrastructure.Converters;

using Microsoft.EntityFrameworkCore.ChangeTracking;

/// <summary>Split string comparer.</summary>
public class SplitStringComparer : ValueComparer<IEnumerable<string>>
{
	/// <summary>Constructor.</summary>
	public SplitStringComparer() : base((c1, c2) => new HashSet<string>(c1!).SetEquals(new HashSet<string>(c2!)), c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode(StringComparison.OrdinalIgnoreCase))))
	{
	}
}
