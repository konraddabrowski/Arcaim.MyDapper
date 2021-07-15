namespace Arcaim.MyDapper.Exceptions
{
    public abstract class DapperException: System.Exception
    {
        public abstract string Code { get; }
        public abstract int StatusCode { get; }
        public override string Source { get => "Arcaim.MyDapper"; }

        public DapperException(string message) : base(message)
        {
        }
    }
}