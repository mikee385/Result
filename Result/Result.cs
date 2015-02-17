using System;

namespace Result
{
    #region Error Types

    public interface IError
    {
        string Message { get; }
    }

    public class EmptyError : IError
    {
        public string Message
        {
            get { return null; }
        }
    }

    public class StandardError : IError
    {
        private readonly string _message;

        public StandardError(string message)
        {
            _message = message;
        }

        public string Message
        {
            get { return _message; }
        }
    }

    public class ExceptionError : IError
    {
        private readonly Exception _exception;

        public ExceptionError(Exception exception)
        {
            _exception = exception;
        }

        public string Message
        {
            get { return _exception != null ? _exception.Message : null; }
        }

        public Exception Exception
        {
            get { return _exception; }
        }
    }

    #endregion

    #region Non-Generic Result

    public abstract class Result
    {
        public abstract bool IsSuccess { get; }
        public abstract bool IsFail { get; }
        public abstract IError Error { get; }

        private static readonly Success _success = new Success();
        private static readonly Fail _fail = new Fail();

        public static Result Success()
        {
            return _success;
        }

        public static Result<T> Success<T>(T value)
        {
            return new Success<T>(value);
        }

        public static Result Fail()
        {
            return _fail;
        }

        public static Result Fail(IError error)
        {
            return new Fail(error);
        }

        public static Result Fail(string message)
        {
            return new Fail(new StandardError(message));
        }

        public static Result Fail(Exception exception)
        {
            return new Fail(new ExceptionError(exception));
        }
    }

    public class Success : Result
    {
        public override bool IsSuccess
        {
            get { return true; }
        }

        public override bool IsFail
        {
            get { return false; }
        }

        public override IError Error
        {
            get { throw new InvalidOperationException("Result is Success. It does not have an Error."); }
        }
    }

    public class Fail : Result
    {
        private static readonly EmptyError _emptyError = new EmptyError();

        private readonly IError _error;

        public Fail()
        {
            _error = _emptyError;
        }

        public Fail(IError error)
        {
            _error = error ?? _emptyError;
        }

        public override bool IsSuccess
        {
            get { return false; }
        }

        public override bool IsFail
        {
            get { return true; }
        }

        public override IError Error
        {
            get { return _error; }
        }
    }

    #endregion

    #region Generic Result

    public abstract class Result<T> : Result
    {
        public abstract T Value { get; }

        // This implicit conversion seems to be a bad idea because null will
        // never be implicitly converted to a Result with null for the Value.
        // Instead, the Result object will be null, which should never happen.
        //public static implicit operator Result<T>(T value)
        //{
        //    return new Success<T>(value);
        //}

        public static implicit operator Result<T>(Fail result)
        {
            return new Fail<T>(result != null ? result.Error : null);
        }
    }

    public class Success<T> : Result<T>
    {
        private readonly T _value;

        public Success(T value)
        {
            _value = value;
        }

        public override bool IsSuccess
        {
            get { return true; }
        }

        public override bool IsFail
        {
            get { return false; }
        }

        public override T Value
        {
            get { return _value; }
        }

        public override IError Error
        {
            get { throw new InvalidOperationException("Result is Success. It does not have an Error."); }
        }
    }

    public class Fail<T> : Result<T>
    {
        private static readonly EmptyError _emptyError = new EmptyError();

        private readonly IError _error;

        public Fail()
        {
            _error = _emptyError;
        }

        public Fail(IError error)
        {
            _error = error ?? _emptyError;
        }

        public override bool IsSuccess
        {
            get { return false; }
        }

        public override bool IsFail
        {
            get { return true; }
        }

        public override T Value
        {
            get { throw new InvalidOperationException("Result is Fail. It does not have a Value."); }
        }

        public override IError Error
        {
            get { return _error; }
        }
    }

    #endregion
}
