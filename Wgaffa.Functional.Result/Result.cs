using System;
using System.Collections.Generic;
using System.Text;

namespace Wgaffa.Functional
{
    public abstract class Result<T, E>
    {
        public static Result<T, E> Ok(T value) => new Success<T, E>(value);
        public static Result<T, E> Error(E value) => new Error<T, E>(value);
        public static Result<T, E> Return(T value) => new Success<T, E>(value);
        public static Result<T, E> Return(E value) => new Error<T, E>(value);
        public static Result<T, E> Return(Func<bool> predicate, Func<T> ifTrue, Func<E> ifFalse)
        {
            return predicate() ? Return(ifTrue()) : Return(ifFalse());
        }

        public abstract Result<T, E> OnSuccess(Action<T> functor);
        public abstract Result<T1, E> OnSuccess<T1>(Func<T, Result<T1, E>> functor);
        public abstract Result<T, E> OnError(Action<E> functor);
        public abstract Result<T, E1> OnError<E1>(Func<E, Result<T, E1>> functor);
        public abstract Result<T, E> OnBoth(Action functor);
        public abstract Result<T, E> OnBoth(Func<Result<T, E>> functor);

        public abstract Result<T1, E> Map<T1>(Func<T, T1> map);

        public static implicit operator bool(Result<T, E> result) => typeof(Success<T, E>) == result.GetType();
    }

    public abstract class Result
    {
        public static Result Ok() => new Success();
        public static Result Error() => new Error();

        public abstract Result OnSuccess(Action functor);
        public abstract Result OnSuccess(Func<Result> functor);
        public abstract Result OnError(Action functor);
        public abstract Result OnError(Func<Result> functor);
        public abstract Result OnBoth(Action functor);
        public abstract Result OnBoth(Func<Result> functor);
    }
}
