﻿using System;

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
    }
}
