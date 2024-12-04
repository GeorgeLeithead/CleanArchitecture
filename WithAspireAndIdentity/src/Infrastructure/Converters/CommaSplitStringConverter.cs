namespace Infrastructure.Converters;
/// <summary>Split string converter, using a comma.</summary>
public class CommaSplitStringConverter : SplitStringConverter
{
	/// <summary>Constructor.</summary>
	public CommaSplitStringConverter() : base(',')
	{
	}
}