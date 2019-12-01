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
    public class NonGenericResultTests
    {
        private Result ThisSucceeds() => Result.Ok();
        private Result ThisFails() => Result.Error();

        [Test]
        public void Ok_CreatesASuccess()
        {
            var result = Result.Ok();

            Assert.That(result, Is.TypeOf<Success>());
        }

        [Test]
        public void Error_CreatesAError()
        {
            var result = Result.Error();

            Assert.That(result, Is.TypeOf<Error>());
        }

        #region OnSuccess
        [Test]
        public void OnSuccess_IsCalled_GivenOk()
        {
            var result = Result.Ok();

            bool isCalled = false;

            result.OnSuccess(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnSuccess_IsNotCalled_GivenError()
        {
            var result = Result.Error();

            bool isCalled = false;

            result.OnSuccess(() => isCalled = true);

            Assert.That(isCalled, Is.False);
        }

        [Test]
        public void OnSuccess_BindsToError_GivenOk()
        {
            var result = Result.Ok()
                .OnSuccess(() => ThisFails());

            Assert.That(result, Is.TypeOf<Error>());
        }

        [Test]
        public void OnSuccess_BindsToSuccess_GivenOk()
        {
            var result = Result.Ok()
                .OnSuccess(() => ThisSucceeds());

            Assert.That(result, Is.TypeOf<Success>());
        }
        #endregion

        #region OnError
        [Test]
        public void OnError_IsCalled_GivenError()
        {
            var result = Result.Error();

            bool isCalled = false;

            result.OnError(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnError_IsNotCalled_GivenSuccess()
        {
            var result = Result.Ok();

            bool isCalled = false;

            result.OnError(() => isCalled = true);

            Assert.That(isCalled, Is.False);
        }

        [Test]
        public void OnError_BindsToSuccess_GivenError()
        {
            var result = Result.Error()
                .OnError(() => ThisSucceeds());

            Assert.That(result, Is.TypeOf<Success>());
        }

        [Test]
        public void OnError_BindsToError_GivenError()
        {
            var result = Result.Error()
                .OnError(() => ThisFails());

            Assert.That(result, Is.TypeOf<Error>());
        }
        #endregion

        #region OnBoth
        [Test]
        public void OnBoth_IsCalled_GivenError()
        {
            var result = Result.Error();

            bool isCalled = false;

            result.OnBoth(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnBoth_IsCalled_GivenSuccess()
        {
            var result = Result.Ok();

            bool isCalled = false;

            result.OnBoth(() => isCalled = true);

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void OnBoth_BindsToSuccess_GivenError()
        {
            var result = Result.Error()
                .OnBoth(() => ThisSucceeds());

            Assert.That(result, Is.TypeOf<Success>());
        }

        [Test]
        public void OnBoth_BindsToError_GivenError()
        {
            var result = Result.Error()
                .OnBoth(() => ThisFails());

            Assert.That(result, Is.TypeOf<Error>());
        }

        [Test]
        public void OnBoth_BindsToError_GivenSuccess()
        {
            var result = Result.Ok()
                .OnBoth(() => ThisFails());

            Assert.That(result, Is.TypeOf<Error>());
        }

        [Test]
        public void OnBoth_BindsToSuccess_GivenSuccess()
        {
            var result = Result.Ok()
                .OnBoth(() => ThisSucceeds());

            Assert.That(result, Is.TypeOf<Success>());
        }
        #endregion
    }
}
