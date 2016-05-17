using System.Linq;
using FsCheck;
using Sharper.C.Data;

namespace Sharper.C.Testing.Arbitraries
{
    public static class ProductArbitrariesModule
    {
        public static Arbitrary<And<A, B>> AnyAnd<A, B>
          ( Arbitrary<A> arbA
          , Arbitrary<B> arbB
          )
        =>  Arb.From
              ( from a in arbA.Generator
                from b in arbB.Generator
                select And.Mk(a, b)
              , x =>
                    ( from a in new[] {x.Fst}.Concat(arbA.Shrinker(x.Fst))
                      from b in new[] {x.Snd}.Concat(arbB.Shrinker(x.Snd))
                      select And.Mk(a, b)
                    )
                    .Skip(1)
              );
    }
}
