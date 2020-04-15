using System;

namespace Wgaffa.Functional
{
    public class Success<T, E> : Result<T, E>
    {
        private readonly T _value;

        public Success(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public override Result<T1, E> Map<T1>(Func<T, T1> map)
        {
            return new Success<T1, E>(map(_value));
        }

        public override Result<T, E> OnBoth(Action functor)
        {
            functor();
            return this;
        }

        public override Result<T, E> OnBoth(Func<Result<T, E>> functor) => functor();

        public override Result<T, E> OnError(Action<E> functor) => this;

        public override Result<T, E1> OnError<E1>(Func<E, Result<T, E1>> functor) => new Success<T, E1>(_value);

        public override Result<T, E> OnSuccess(Action<T> functor)
        {
            functor(_value);
            return this;
        }

        public override Result<T1, E> OnSuccess<T1>(Func<T, Result<T1, E>> functor) => functor(_value);

        public override T1 Reduce<T1>(Func<T, T1> ifSuccess, Func<E, T1> _) => ifSuccess(_value);
    }

    public class Success : Result
    {
        public override Result OnBoth(Action functor)
        {
            functor();
            return this;
        }

        public override Result OnBoth(Func<Result> functor) => functor();

        public override Result OnError(Action functor) => this;

        public override Result OnError(Func<Result> functor) => this;

        public override Result OnSuccess(Action functor)
        {
            functor();
            return this;
        }

        public override Result OnSuccess(Func<Result> functor) => functor();

        public override T Reduce<T>(Func<T> ifSuccess, Func<T> ifError) => ifSuccess();
    }

    public class Success<T> : Result<T>
    {
        private T _value;

        public Success(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public override Result<T> OnBoth(Action functor)
        {
            functor();
            return this;
        }

        public override Result<T> OnBoth(Func<Result<T>> functor) => functor();

        public override Result<T> OnError(Action functor) => this;

        public override Result<T> OnError(Func<Result<T>> functor) => this;

        public override Result<T> OnSuccess(Action<T> functor)
        {
            functor(_value);
            return this;
        }

        public override Result<T> OnSuccess(Func<T, Result<T>> functor) => functor(_value);

        public override T1 Reduce<T1>(Func<T, T1> ifSuccess, Func<T1> ifError) => ifSuccess(_value);
    }
}
