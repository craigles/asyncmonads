using System;

namespace AsyncMonads
{
    public class Maybe<T>
    {
        private readonly bool _hasItem;
        private readonly T _item;

        public Maybe() => _hasItem = false;

        public Maybe(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _item = item;
            _hasItem = true;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            if (_hasItem)
                return new Maybe<TResult>(selector(_item));

            return new Maybe<TResult>();
        }

        public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            if (_hasItem)
                return selector(_item);

            return new Maybe<TResult>();
        }

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
        {
            if (nothing == null)
                throw new ArgumentNullException(nameof(nothing));
            if (just == null)
                throw new ArgumentNullException(nameof(just));

            return _hasItem ? just(_item) : nothing;
        }

        public static Maybe<int> TryParseInt(string candidate)
        {
            if (int.TryParse(candidate, out int i))
                return new Maybe<int>(i);

            return new Maybe<int>();
        }

        public static Maybe<DateTime> TryParseDate(string candidate)
        {
            if (DateTime.TryParse(candidate, out DateTime date))
                return new Maybe<DateTime>(date);

            return new Maybe<DateTime>();
        }
    }
}