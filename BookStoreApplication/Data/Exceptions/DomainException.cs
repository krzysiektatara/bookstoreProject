namespace BookStoreApplicationAPI.Data.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string ErrorMessage { get; }

        public abstract short ErrorCode { get; }

        protected DomainException(Exception? innerException)
            : base(null, innerException)
        {
        }
    }
}
