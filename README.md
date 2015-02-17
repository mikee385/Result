# Result 

`Result` is intended to be a replacement for exceptions and `out` parameters to use as the return value for methods in .NET. Although exceptions are usually the most common mechanism used to indicate when a method cannot deliver a suitable result, it is sometimes convenient to use a more transparent means through the return value of a method. While the .NET Framework encourages the use of `TryGet...` methods that return a boolean and use `out` parameters for the meaningful results, it is possible (and easy) to misuse these by failing to check the return value and continuing to use the result (which was likely default-initialized).

This project provides 2 abstract classes that can be used as the return type of methods:

- `Result`: Used to indicate success or failure without providing a return value. This is equivalent to a `void` method that throws an exception on failure.
- `Result<T>`: Used to indicate success or failure while also providing a return value on success. This is equivalent to a method with a return value that throws an exception on failure or a `TryGet...` method with an `out` parameter.

`Result` has the following three properties:

- `IsSuccess` is `true` if the method returned successfully and `false` otherwise.
- `IsFail` is `true` if the method returned an error and `false` otherwise.
- `Error` contains an `IError` object with additional details about the error that occurred when `IsFail` is `true` (and `IsSuccess` is `false`). Attempting to access this property when `IsFail` is `false` (and `IsSuccess` is `true`) will result in an exception being thrown.

Both `IsSuccess` and `IsFail` are provided, even though they are redundant, in order to allow the most meaningful semantics within the caller of this method. For example, if the caller wants to exit early after receiving a successful result, it makes more sense to use `if (result.IsSuccess) { ... }`. On the other hand, if the caller wants to exit early after receiving a failure, it makes more sense to use `if (result.IsFail) { ... }`.

`Result<T>` has the same properties as `Result` with the same behaviors, along with the following additional property:

- `Value` contains the return value of the method when `IsSuccess` is `true` (and `IsFail` is `false`). Attempting to access this property when `IsSuccess` is `false` (and `IsFail` is `true`) will result in an exception being thrown.  

Each of these have helper classes to provide the desired behavior: `Success` and `Fail` for `Result` and `Success<T>` and `Fail<T>` for `Result<T>`. However, generally, these classes won't be used directly. `Result` provides static helper methods to generate them:

- `Result.Success()` returns a `Success` object (actually a singleton `Success` object, since there is no unique state within this class).
- `Result.Success(T value)` returns a `Success<T>` object that wraps the given value.
- `Result.Fail()` returns a `Fail` object (again, actually a singleton `Fail` object, since there is no unique state within this class).
- `Result.Fail(string message)` returns a `Fail` object containing the given error message.
- `Result.Fail(Exception exception)` returns a `Fail` object containing the given exception.

For convenience, an implicit conversion is provided to convert a `Fail` object to a `Result<T>` for any `T`. This allows the type of the return value to be ignored when indicating an error.

## Example Usage of `Result`

	static Result IsPositive(double value)
	{
		if (value < 0.0)
			return Result.Fail();

		return Result.Success();
	}

## Example Usage of `Result<T>`

	static Result<double> Sqrt(double value)
	{
		if (value < 0.0)
			return Result.Fail("Value cannot be negative.");

		return Result.Success(Math.Sqrt(value));
	}

Note that the type parameter for the `Success<T>` object is automatically inferred from the argument provided to the `Result.Success` call. `Result.Fail` returns a `Fail` object, which is implicitly converted to a `Result<double>` object so it can be returned by the method.

## License

Result is distributed under the terms of the MIT license.

See LICENSE.md for details.
