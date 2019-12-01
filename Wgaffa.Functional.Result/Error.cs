using System;

namespace Wgaffa.Functional
{
    public class Error<T, E> : Result<T, E>
    {
        private readonly E _error;

        public Error(E error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            _error = error;
        }

        public override Result<T1, E> Map<T1>(Func<T, T1> map)
        {
            return new Error<T1, E>(_error);
        }

        public override Result<T, E> OnBoth(Action functor)
        {
            functor();
            return this;
        }

        public override Result<T, E> OnBoth(Func<Result<T, E>> functor) => functor();

        public override Result<T, E> OnError(Action<E> functor)
        {
            functor(_error);
            return this;
        }

        public override Result<T, E1> OnError<E1>(Func<E, Result<T, E1>> functor) => functor(_error);

        public override Result<T, E> OnSuccess(Action<T> functor) => this;

        public override Result<T1, E> OnSuccess<T1>(Func<T, Result<T1, E>> functor) => new Error<T1, E>(_error);
    }

    public class Error : Result
    {
        public override Result OnBoth(Action functor)
        {
            functor();
            return this;
        }

        public override Result OnBoth(Func<Result> functor) => functor();

        public override Result OnError(Action functor)
        {
            functor();
            return this;
        }

        public override Result OnError(Func<Result> functor) => functor();

        public override Result OnSuccess(Action functor) => this;

        public override Result OnSuccess(Func<Result> functor) => this;
    }
}
