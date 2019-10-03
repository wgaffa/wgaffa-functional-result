using System;
using System.Collections.Generic;
using System.Text;

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

        public override Result<T, E> OnBoth(Func<Result<T, E>> functor) => functor();

        public override Result<T, E> OnError(Func<E, Result<T, E>> functor) => this;

        public override Result<T, E> OnSuccess(Func<T, Result<T, E>> functor) => functor(_value);
    }
}
