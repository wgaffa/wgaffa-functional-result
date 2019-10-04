using NUnit.Framework;

namespace Wgaffa.Functional.Result.Tests
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void Ok_ShouldCreateSuccess()
        {
            var ok = Result<bool, string>.Ok(true);

            Assert.That(ok, Is.TypeOf<Success<bool, string>>());
        }

        [Test]
        public void Error_ShouldCreateError()
        {
            var error = Result<int, string>.Error("error message");

            Assert.That(error, Is.TypeOf<Error<int, string>>());
        }

        [Test]
        public void Return_ShouldCreateSuccess_GivenInt()
        {
            var success = Result<int, string>.Return(42);

            Assert.That(success, Is.TypeOf<Success<int, string>>());
        }

        [Test]
        public void Return_ShouldCreateError_GivenString()
        {
            var success = Result<int, string>.Return("some error occured");

            Assert.That(success, Is.TypeOf<Error<int, string>>());
        }

        [Test]
        public void Return_ShouldCreateSuccess_GivenTruePredicate()
        {
            var success = Result<int, string>.Return(() => true, () => 42, () => "error message");

            Assert.That(success, Is.TypeOf<Success<int, string>>());
        }

        [Test]
        public void Return_ShouldCreateError_GivenFalsePredicate()
        {
            var success = Result<int, string>.Return(() => false, () => 42, () => "error message");

            Assert.That(success, Is.TypeOf<Error<int, string>>());
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool OnSuccess_ShouldRunBasedOnResult(bool failure)
        {
            var hasRun = false;

            var result = Result<int, string>.Return(() => failure, () => 42, () => "some error message")
                .OnSuccess(x =>
                {
                    hasRun = true;
                    return Result<int, string>.Ok(x);
                });

            return hasRun;
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = true)]
        public bool OnError_ShouldRunBasedOnResult(bool failure)
        {
            var hasRun = false;

            var result = Result<int, string>.Return(() => failure, () => 42, () => "some error message")
                .OnError(x =>
                {
                    hasRun = true;
                    return Result<int, string>.Error(x);
                });

            return hasRun;
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool OnBoth_ShouldRunBasedOnResult(bool failure)
        {
            var hasRun = false;

            var result = Result<int, string>.Return(() => failure, () => 42, () => "some error message")
                .OnBoth(() =>
                {
                    hasRun = true;
                    return Result<int, string>.Ok(3);
                });

            return hasRun;
        }
    }
}
