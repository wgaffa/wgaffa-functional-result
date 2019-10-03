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

        public abstract Result<T, E> OnSuccess(Func<T, Result<T, E>> functor);
        public abstract Result<T, E> OnError(Func<E, Result<T, E>> functor);
        public abstract Result<T, E> OnBoth(Func<Result<T, E>> functor);
    }
}
