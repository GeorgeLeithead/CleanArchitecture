namespace Infrastructure.Converters;

using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

/// <summary>Split string converter</summary>
/// <param name="delimiter">Delimiter to use to split.</param>
public abstract class SplitStringConverter(char delimiter) : ValueConverter<ReadOnlyCollection<string>, string>(
	v => string.Join(delimiter.ToString(), v),
	v => v.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries).AsReadOnly())
{
}
