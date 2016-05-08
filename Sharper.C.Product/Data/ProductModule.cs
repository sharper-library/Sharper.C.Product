using System;
using System.Collections.Generic;

namespace Sharper.C.Data
{

public static class ProductModule
{
    public struct And<A, B>
    {
        public A Fst { get; }
        public B Snd { get; }

        internal And(A a, B b)
        {
            Fst = a;
            Snd = b;
        }

        public And<C, B> MapFst<C>(Func<A, C> f)
        =>  MkAnd(f(Fst), Snd);

        public And<A, C> MapSnd<C>(Func<B, C> f)
        =>  MkAnd(Fst, f(Snd));

        public And<C, D> BiMap<C, D>(Func<A, C> f, Func<B, D> g)
        =>  new And<C, D>(f(Fst), g(Snd));

        public And<C, B> ApplyFst<C>(And<Func<A, C>, B> f, Func<B, B, B> plus)
        =>  MkAnd(f.Fst(Fst), plus(Snd, f.Snd));

        public And<A, C> ApplySnd<C>(And<A, Func<B, C>> f, Func<A, A, A> plus)
        =>  MkAnd(plus(Fst, f.Fst), f.Snd(Snd));

        public And<C, D> BiApply<C, D>(And<Func<A, C>, Func<B, D>> f)
        =>  MkAnd(f.Fst(Fst), f.Snd(Snd));

        public And<And<A, C>, B> ZipFst<C>(And<C, B> cb, Func<B, B, B> plus)
        =>  MkAnd(MkAnd(Fst, cb.Fst), plus(Snd, cb.Snd));

        public And<A, And<B, C>> ZipSnd<C>(And<A, C> ac, Func<A, A, A> plus)
        =>  MkAnd(plus(Fst, ac.Fst), MkAnd(Snd, ac.Snd));

        public And<And<A, C>, And<B, D>> BiZip<C, D>(And<C, D> cd)
        =>  MkAnd(MkAnd(Fst, cd.Fst), MkAnd(Snd, cd.Snd));

        public C Cata<C>(Func<A, B, C> f)
        =>  f(Fst, Snd);

        public And<C, B> Select<C>(Func<A, C> f)
        =>  MapFst(f);
    }

    public static And<A, B> MkAnd<A, B>(A a, B b)
    =>  new And<A, B>(a, b);
}

}
