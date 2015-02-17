using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Tests
{
    [TestClass]
    public class ResultTests
    {
        #region IError Tests

        [TestMethod]
        public void EmptyError_Message_should_be_null()
        {
            IError error = new EmptyError();
            Assert.AreEqual(error.Message, null);
        }

        [TestMethod]
        public void StandardError_Message_should_be_string()
        {
            const string message = "error";
            IError error = new StandardError(message);
            Assert.AreEqual(error.Message, message);
        }

        [TestMethod]
        public void ExceptionError_Exception_should_be_Exception()
        {
            const string message = "exception";
            var exception = new Exception(message);
            var error = new ExceptionError(exception);
            Assert.AreEqual(error.Exception, exception);
        }

        [TestMethod]
        public void ExceptionError_Message_should_be_Exception_Message()
        {
            const string message = "exception";
            var exception = new Exception(message);
            IError error = new ExceptionError(exception);
            Assert.AreEqual(error.Message, exception.Message);
        }

        [TestMethod]
        public void ExceptionError_null_Message_should_be_null()
        {
            IError error = new ExceptionError(null);
            Assert.AreEqual(error.Message, null);
        }

        #endregion

        #region Non-Generic Success Tests

        [TestMethod]
        public void Success_IsSuccess_should_be_true()
        {
            Result result = new Success();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Success_IsFail_should_be_false()
        {
            Result result = new Success();
            Assert.IsFalse(result.IsFail);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Success_Error_should_throw_exception()
        {
            Result result = new Success();
            var error = result.Error;
        }

        #endregion

        #region Non-Generic Fail Tests

        [TestMethod]
        public void Fail_IsSuccess_should_be_false()
        {
            Result result = new Fail();
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Fail_IsFail_should_be_true()
        {
            Result result = new Fail();
            Assert.IsTrue(result.IsFail);
        }

        [TestMethod]
        public void Fail_default_Error_should_be_EmptyError()
        {
            Result result = new Fail();
            Assert.IsTrue(result.Error is EmptyError);
        }

        [TestMethod]
        public void Fail_null_Error_should_be_EmptyError()
        {
            Result result = new Fail(null);
            Assert.IsTrue(result.Error is EmptyError);
        }

        [TestMethod]
        public void Fail_EmptyError_Error_should_be_EmptyError()
        {
            IError error = new EmptyError();
            Result result = new Fail(error);
            Assert.IsTrue(result.Error is EmptyError);
        }

        [TestMethod]
        public void Fail_StandardError_Error_should_be_StandardError()
        {
            const string message = "error";
            IError error = new StandardError(message);
            Result result = new Fail(error);
            Assert.AreEqual(result.Error, error);
        }

        [TestMethod]
        public void Fail_ExceptionError_Error_should_be_ExceptionError()
        {
            const string message = "exception";
            var exception = new Exception(message);
            IError error = new ExceptionError(exception);
            Result result = new Fail(error);
            Assert.AreEqual(result.Error, error);
        }

        [TestMethod]
        public void Fail_should_be_ResultT()
        {
            Result<object> result = new Fail();
            Assert.IsTrue(result is Fail<object>);
        }

        [TestMethod]
        public void Fail_null_should_be_ResultT()
        {
            Result<object> result = (Fail)null;
            Assert.IsNotNull(result);
        }

        #endregion

        #region Generic Success Tests

        [TestMethod]
        public void SuccessT_should_be_Result()
        {
            object obj = new { };
            Result<object> result = new Success<object>(obj);
            Assert.IsTrue(result is Result);
        }

        [TestMethod]
        public void SuccessT_IsSuccess_should_be_true()
        {
            object obj = new { };
            Result<object> result = new Success<object>(obj);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void SuccessT_IsFail_should_be_false()
        {
            object obj = new { };
            Result<object> result = new Success<object>(obj);
            Assert.IsFalse(result.IsFail);
        }

        [TestMethod]
        public void SuccessT_Value_should_be_value()
        {
            object obj = new { };
            Result<object> result = new Success<object>(obj);
            Assert.AreEqual(result.Value, obj);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SuccessT_Error_should_throw_exception()
        {
            object obj = new { };
            Result<object> result = new Success<object>(obj);
            var error = result.Error;
        }

        #endregion

        #region Generic Fail Tests

        [TestMethod]
        public void FailT_should_be_Result()
        {
            Result<object> result = new Fail<object>();
            Assert.IsTrue(result is Result);
        }

        [TestMethod]
        public void FailT_IsSuccess_should_be_false()
        {
            Result<object> result = new Fail<object>();
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void FailT_IsFail_should_be_true()
        {
            Result<object> result = new Fail<object>();
            Assert.IsTrue(result.IsFail);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailT_Value_should_throw_exception()
        {
            Result<object> result = new Fail<object>();
            var value = result.Value;
        }

        [TestMethod]
        public void FailT_default_Error_should_be_EmptyError()
        {
            Result<object> result = new Fail<object>();
            Assert.IsTrue(result.Error is EmptyError);
        }

        [TestMethod]
        public void FailT_null_Error_should_be_EmptyError()
        {
            Result<object> result = new Fail<object>(null);
            Assert.IsTrue(result.Error is EmptyError);
        }

        [TestMethod]
        public void FailT_EmptyError_Error_should_be_EmptyError()
        {
            IError error = new EmptyError();
            Result<object> result = new Fail<object>(error);
            Assert.IsTrue(result.Error is EmptyError);
        }

        [TestMethod]
        public void FailT_StandardError_Error_should_be_StandardError()
        {
            const string message = "error";
            IError error = new StandardError(message);
            Result<object> result = new Fail<object>(error);
            Assert.AreEqual(result.Error, error);
        }

        [TestMethod]
        public void FailT_ExceptionError_Error_should_be_ExceptionError()
        {
            const string message = "exception";
            var exception = new Exception(message);
            IError error = new ExceptionError(exception);
            Result<object> result = new Fail<object>(error);
            Assert.AreEqual(result.Error, error);
        }
        #endregion

        #region Result Helper Method Tests

        [TestMethod]
        public void Success_Helper_should_be_Success()
        {
            Result result = Result.Success();
            Assert.IsTrue(result is Success);
        }

        [TestMethod]
        public void SuccessT_Helper_should_be_Success()
        {
            object obj = new { };
            Result<object> result = Result.Success(obj);
            Assert.IsTrue(result is Success<object>);
        }

        [TestMethod]
        public void SuccessT_Helper_Value_should_be_value()
        {
            object obj = new { };
            Result<object> result = Result.Success(obj);
            Assert.AreEqual(result.Value, obj);
        }

        [TestMethod]
        public void Fail_Helper_default_should_be_Fail()
        {
            Result result = Result.Fail();
            Assert.IsTrue(result is Fail);
        }

        [TestMethod]
        public void Fail_Helper_IError_should_be_IError()
        {
            const string message = "error";
            IError error = new StandardError(message);
            Result result = Result.Fail(error);
            Assert.AreEqual(result.Error, error);
        }

        [TestMethod]
        public void Fail_Helper_string_should_be_StandardError()
        {
            const string message = "error";
            Result result = Result.Fail(message);
            Assert.IsTrue(result.Error is StandardError);
        }

        [TestMethod]
        public void Fail_Helper_string_Message_should_be_string()
        {
            const string message = "error";
            Result result = Result.Fail(message);
            Assert.AreEqual(result.Error.Message, message);
        }

        [TestMethod]
        public void Fail_Helper_Exception_should_be_ExceptionError()
        {
            const string message = "exception";
            var exception = new Exception(message);
            Result result = Result.Fail(exception);
            Assert.IsTrue(result.Error is ExceptionError);
        }

        [TestMethod]
        public void Fail_Helper_Exception_Exception_should_be_Exception()
        {
            const string message = "exception";
            var exception = new Exception(message);
            Result result = Result.Fail(exception);
            Assert.AreEqual(((ExceptionError)result.Error).Exception, exception);
        }

        #endregion
    }
}
