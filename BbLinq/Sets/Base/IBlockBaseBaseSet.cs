namespace BlockBase.BBLinq.Sets.Base
{
    public interface IBlockBaseBaseSet<out TResult> where TResult : IBlockBaseBaseSet<TResult>
    {
        public TResult Encrypt();
    }
}
