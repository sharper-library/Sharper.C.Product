using System;

namespace Sharper.C.Data
{
    public struct And<A, B>
      : IEquatable<And<A, B>>
    {
        public A Fst { get; }
        public B Snd { get; }

        internal And(A a, B b)
        {
            Fst = a;
            Snd = b;
        }

        public And<C, B> MapFst<C>(Func<A, C> f)
        =>  And.Mk(f(Fst), Snd);

        public And<A, C> MapSnd<C>(Func<B, C> f)
        =>  And.Mk(Fst, f(Snd));

        public And<C, D> BiMap<C, D>(Func<A, C> f, Func<B, D> g)
        =>  new And<C, D>(f(Fst), g(Snd));

        public And<C, B> ApplyFst<C>(And<Func<A, C>, B> f, Func<B, B, B> plus)
        =>  And.Mk(f.Fst(Fst), plus(Snd, f.Snd));

        public And<A, C> ApplySnd<C>(And<A, Func<B, C>> f, Func<A, A, A> plus)
        =>  And.Mk(plus(Fst, f.Fst), f.Snd(Snd));

        public And<C, D> BiApply<C, D>(And<Func<A, C>, Func<B, D>> f)
        =>  And.Mk(f.Fst(Fst), f.Snd(Snd));

        public And<And<A, C>, B> ZipFst<C>(And<C, B> cb, Func<B, B, B> plus)
        =>  And.Mk(And.Mk(Fst, cb.Fst), plus(Snd, cb.Snd));

        public And<A, And<B, C>> ZipSnd<C>(And<A, C> ac, Func<A, A, A> plus)
        =>  And.Mk(plus(Fst, ac.Fst), And.Mk(Snd, ac.Snd));

        public And<And<A, C>, And<B, D>> BiZip<C, D>(And<C, D> cd)
        =>  And.Mk(And.Mk(Fst, cd.Fst), And.Mk(Snd, cd.Snd));

        public C Cata<C>(Func<A, B, C> f)
        =>  f(Fst, Snd);

        public And<B, A> Swap
        =>  And.Mk(Snd, Fst);

        public And<C, B> Select<C>(Func<A, C> f)
        =>  MapFst(f);

        public And<And<A, B>, C> _<C>(C c)
        =>  And.Mk(this, c);

        public bool Equals(And<A, B> x)
        =>  Equals(Fst, x.Fst) && Equals(Snd, x.Snd);

        public override bool Equals(object obj)
        =>  obj is And<A, B> && Equals((And<A, B>)obj);

        public override string ToString()
        =>  $"And({Fst},{Snd})";

        public override int GetHashCode()
        {   unchecked
            {   var x = 0;
                x = (x * 397) ^ Fst.GetHashCode();
                x = (x * 397) ^ Snd.GetHashCode();
                return x;
            };
        }

        public static bool operator ==(And<A, B> x, And<A, B> y)
        =>  x.Equals(y);

        public static bool operator !=(And<A, B> x, And<A, B> y)
        =>  !x.Equals(y);
    }

    public static class And
    {
        public static And<A, B> Mk<A, B>(A a, B b)
        =>  new And<A, B>(a, b);

        public static Z Args<A, B, Z>(this And<A, B> x, Func<A, B, Z> f)
        =>  x.Cata(f);

        public static Z Args<A, B, C, Z>
          ( this And<And<A, B>, C> x
          , Func<A, B, C, Z> f
          )
        =>  f(x.Fst.Fst, x.Fst.Snd, x.Snd);

        public static Z Args<A, B, C, D, Z>
          ( this And<And<And<A, B>, C>, D> x
          , Func<A, B, C, D, Z> f
          )
        =>  f(x.Fst.Fst.Fst, x.Fst.Fst.Snd, x.Fst.Snd, x.Snd);

        public static Z Args<A, B, C, D, E, Z>
          ( this And<And<And<And<A, B>, C>, D>, E> x
          , Func<A, B, C, D, E, Z> f
          )
        =>  f
              ( x.Fst.Fst.Fst.Fst
              , x.Fst.Fst.Fst.Snd
              , x.Fst.Fst.Snd
              , x.Fst.Snd
              , x.Snd
              );

        public static Z Args<A, B, C, D, E, F, Z>
          ( this And<And<And<And<And<A, B>, C>, D>, E>, F> x
          , Func<A, B, C, D, E, F, Z> f
          )
        =>  f
              ( x.Fst.Fst.Fst.Fst.Fst
              , x.Fst.Fst.Fst.Fst.Snd
              , x.Fst.Fst.Fst.Snd
              , x.Fst.Fst.Snd
              , x.Fst.Snd
              , x.Snd
              );

        public static Z Args<A, B, C, D, E, F, G, Z>
          ( this And<And<And<And<And<And<A, B>, C>, D>, E>, F>, G> x
          , Func<A, B, C, D, E, F, G, Z> f
          )
        =>  f
              ( x.Fst.Fst.Fst.Fst.Fst.Fst
              , x.Fst.Fst.Fst.Fst.Fst.Snd
              , x.Fst.Fst.Fst.Fst.Snd
              , x.Fst.Fst.Fst.Snd
              , x.Fst.Fst.Snd
              , x.Fst.Snd
              , x.Snd
              );
    }
}
