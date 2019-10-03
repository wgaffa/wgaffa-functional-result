using System;
using System.Collections.Generic;
using System.Text;

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

        public override Result<T, E> OnBoth(Func<Result<T, E>> functor) => functor();

        public override Result<T, E> OnError(Func<E, Result<T, E>> functor) => functor(_error);

        public override Result<T, E> OnSuccess(Func<T, Result<T, E>> functor) => this;
    }
}
