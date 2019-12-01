using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wgaffa.Functional.Tests
{
    [TestFixture]
    public class OneGenericResultTests
    {
        private Result<T> ThisSucceeds<T>(T value) => value;
        private Result<T> ThisFails<T>() => Result<T>.Error();

        [Test]
        public void Ok_CreatesASuccess()
        {
            var result = Result<string>.Ok("ok");

            Assert.That(result, Is.TypeOf<Success<string>>());
        }

        [Test]
        public void Error_CreatesAError()
        {
            var result = Result<string>.Error();

            Assert.That(result, Is.TypeOf<Error<string>>());
        }

        [Test]
        public void Implicit_CreatesASuccess()
        {
            Result<string> result = "Ok";

            Assert.That(result, Is.TypeOf<Success<string>>());
        }

        #region OnSuccess
        [Test]
        public void OnSuccess_IsCalled_GivenOk()
        {
            var result = Result<string>.Ok("ok");

            var value = string.Empty;

            result.OnSuccess(s => value = s);

            Assert.That(value, Is.EqualTo("ok"));
        }

        [Test]
        public void OnSuccess_IsNotCalled_GivenError()
        {
            var result = Result<string>.Error();

            var value = string.Empty;

            result.OnSuccess(s => value = s);

            Assert.That(value, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OnSuccess_BindsToError_GivenOk()
        {
            var result = Result<string>.Ok("ok")
                .OnSuccess(s => ThisFails<string>());

            Assert.That(result, Is.TypeOf<Error<string>>());
        }

        [Test]
        public void OnSuccess_BindsToSuccess_GivenOk()
        {
            var result = Result<string>.Ok("ok")
                .OnSuccess(s => ThisSucceeds(s.ToUpper()));

            Assert.That(result, Is.TypeOf<Success<string>>());
        }
        #endregion

        #region OnError
        [Test]
        public void OnError_IsCalled_GivenError()
        {
            var result = Result<string>.Error();

            bool isCalled = false;

            result.OnError(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnError_IsNotCalled_GivenSuccess()
        {
            var result = Result<string>.Ok("ok");

            bool isCalled = false;

            result.OnError(() => isCalled = true);

            Assert.That(isCalled, Is.False);
        }

        [Test]
        public void OnError_BindsToSuccess_GivenError()
        {
            var result = Result<string>.Error()
                .OnError(() => ThisSucceeds("ok"));

            Assert.That(result, Is.TypeOf<Success<string>>());
        }

        [Test]
        public void OnError_BindsToError_GivenError()
        {
            var result = Result<string>.Error()
                .OnError(() => ThisFails<string>());

            Assert.That(result, Is.TypeOf<Error<string>>());
        }
        #endregion

        #region OnBoth
        [Test]
        public void OnBoth_IsCalled_GivenError()
        {
            var result = Result<string>.Error();

            bool isCalled = false;

            result.OnBoth(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnBoth_IsCalled_GivenSuccess()
        {
            var result = Result<string>.Ok("ok");

            bool isCalled = false;

            result.OnBoth(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnBoth_BindsToSuccess_GivenError()
        {
            var result = Result<string>.Error()
                .OnBoth(() => ThisSucceeds("ok"));

            Assert.That(result, Is.TypeOf<Success<string>>());
        }

        [Test]
        public void OnBoth_BindsToError_GivenError()
        {
            var result = Result<string>.Error()
                .OnBoth(() => ThisFails<string>());

            Assert.That(result, Is.TypeOf<Error<string>>());
        }

        [Test]
        public void OnBoth_BindsToError_GivenSuccess()
        {
            var result = Result<string>.Ok("ok")
                .OnBoth(() => ThisFails<string>());

            Assert.That(result, Is.TypeOf<Error<string>>());
        }

        [Test]
        public void OnBoth_BindsToSuccess_GivenSuccess()
        {
            var result = Result<string>.Ok("ok")
                .OnBoth(() => ThisSucceeds("super"));

            Assert.That(result, Is.TypeOf<Success<string>>());
        }
        #endregion    
    }
}
