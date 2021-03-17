namespace BlockBase.BBLinq.Sets.Base
{
    public interface IBlockBaseSet<T> : IJoin<T>, IFetchableSet<T>, IInsertableSet<T>, IQueryableSet<T>, IBlockBaseBaseSet<IBlockBaseSet<T>>, ISet
    {
    }
}
