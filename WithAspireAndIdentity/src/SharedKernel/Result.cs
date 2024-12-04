namespace SharedKernel;

/// <summary>Result class.</summary>
public class Result
{
	/// <summary>Initializes a new instance of the <see cref="Result" /> class.</summary>
	/// <param name="isSuccess">The success of the result.</param>
	/// <param name="error">Result error <see cref="Error"/>.</param>
	/// <exception cref="ArgumentException">Invalid error.</exception>
	public Result(bool isSuccess, Error error)
	{
		if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
		{
			throw new ArgumentException("Invalid error", nameof(error));
		}

		IsSuccess = isSuccess;
		Error = error;
	}

	/// <summary>Result error.</summary>
	public Error Error { get; }

	/// <summary>Result failure.</summary>
	public bool IsFailure => !IsSuccess;

	/// <summary>Result success.</summary>
	public bool IsSuccess { get; }

	/// <summary>Result failure.</summary>
	/// <param name="error">The <see cref="Error"/>.</param>
	/// <returns>A <see cref="Result"/>.</returns>
	public static Result Failure(Error error) => new(false, error);

	/// <summary>Result failure.</summary>
	/// <typeparam name="TValue">Type value.</typeparam>
	/// <param name="error">The <see cref="Error"/>.</param>
	/// <returns>A <see cref="Result{TValue}"/>.</returns>
	public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

	/// <summary>Result success.</summary>
	/// <returns>A <see cref="Result"/>.</returns>
	public static Result Success() => new(true, Error.None);

	/// <summary>Result success.</summary>
	/// <typeparam name="TValue">Type value.</typeparam>
	/// <param name="value">Result value.</param>
	/// <returns>A <see cref="Result{TValue}"/>.</returns>
	public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
}

/// <summary>Initializes a new instance of the <see cref="Result{TValue}" /> class.</summary>
/// <remarks>Result constructor.</remarks>
/// <param name="value"></param>
/// <param name="isSuccess"></param>
/// <param name="error"></param>
public class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
	readonly TValue? value = value;

	/// <summary>Value of the failure.</summary>
	[NotNull]
	public TValue Value => IsSuccess ? value! : throw new InvalidOperationException("The value of a failure result can't be accessed.");

	/// <summary>Result operator.</summary>
	/// <param name="value">Value of result.</param>
#pragma warning disable CA2225 // Operator overloads have named alternates
	public static implicit operator Result<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
#pragma warning restore CA2225 // Operator overloads have named alternates

	/// <summary>Result validation failure.</summary>
	/// <param name="error">The <see cref="Error"/>.</param>
	/// <returns>A <see cref="Result{TValue}"/>.</returns>
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static Result<TValue> ValidationFailure(Error error) => new(default, false, error);
#pragma warning restore CA1000 // Do not declare static members on generic types
}