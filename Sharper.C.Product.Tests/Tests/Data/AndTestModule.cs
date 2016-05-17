using System;
using System.Linq;
using Sharper.C.Data;
using Sharper.C.Testing;
using Sharper.C.Testing.Xunit;
using Sharper.C.Testing.Laws;

using static Sharper.C.Testing.Arbitraries.SystemArbitrariesModule;
using static Sharper.C.Testing.Arbitraries.ProductArbitrariesModule;

namespace Sharper.C.Product.Tests.Data
{
    public static class AndTestModule
    {
        [Invariant(MaxTest=10)]
        public static Invariant Obeys_OrderingLaws()
        =>  OrderingLaws.For
              ( (And<bool, bool> x, And<bool, bool> y) =>
                    Lt(x.Fst, y.Fst)
                    || (Eq(x.Fst, y.Fst) && LtEq(x.Snd, y.Snd))
              , (And<bool, bool> x, And<bool, bool> y) => x == y
              , AnyAnd(AnyBool, AnyBool)
              );

        [Invariant(MaxTest=10)]
        public static Invariant Obeys_HashingLaws()
        =>  HashingLaws.For(AnyAnd(AnyBool, AnyBool));

        [Invariant]
        public static Invariant Obeys_FunctorLaws()
        =>  "And Functor".All
              ( FunctorLaws.For
                  ( f => x => x.MapFst(f)
                  , (x, y) => x == y
                  , AnyAnd(AnyLong, AnyLong)
                  , AnyFunc1<long, long>(AnyLong)
                  )
              , FunctorLaws.For
                  ( f => x => x.MapSnd(f)
                  , (x, y) => x == y
                  , AnyAnd(AnyLong, AnyLong)
                  , AnyFunc1<long, long>(AnyLong)
                  )
              );

        private static bool LtEq<A>(A x, A y)
          where A : IComparable<A>
        =>  x.CompareTo(y) <= 0;

        private static bool Lt<A>(A x, A y)
          where A : IComparable<A>
        =>  x.CompareTo(y) < 0;

        private static bool Eq<A>(A x, A y)
          where A : IComparable<A>
        =>  x.CompareTo(y) == 0;
    }
}
