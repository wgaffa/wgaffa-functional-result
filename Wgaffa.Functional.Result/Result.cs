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
    }
}
