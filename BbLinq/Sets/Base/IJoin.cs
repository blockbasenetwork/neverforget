﻿namespace BlockBase.BBLinq.Sets.Base
{
    public interface IJoin<TA>
    {
        public IJoin<TA, TB> Join<TB>();
    }
    public interface IJoin<TA, TB> : IQueryableJoinSet<TA, TB>
    {
        public IJoin<TA, TB, TC> Join<TC>();
    }
    public interface IJoin<TA, TB, TC> : IQueryableJoinSet<TA, TB, TC>
    {
        public IJoin<TA, TB, TC, TD> Join<TD>();
    }
    public interface IJoin<TA, TB, TC, TD> : IQueryableJoinSet<TA, TB, TC, TD>
    {
        public IJoin<TA, TB, TC, TD, TE> Join<TE>();
    }
    public interface IJoin<TA, TB, TC, TD, TE> : IQueryableJoinSet<TA, TB, TC, TD, TE>
    {
        public IJoin<TA, TB, TC, TD, TE, TF> Join<TF>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG> Join<TG>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH> Join<TH>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI> Join<TI>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Join<TJ>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Join<TK>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Join<TL>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Join<TM>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Join<TN>();
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Join<TO>();
    }

    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>
    {
    }
}
