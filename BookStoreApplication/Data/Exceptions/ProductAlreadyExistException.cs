using System.Runtime.CompilerServices;

namespace BookStoreApplicationAPI.Data.Exceptions
{
    public class ProductAlreadyExistException : DomainException
    {
        public string? _productName { get; }

        public override string ErrorMessage { get; }

        public override short ErrorCode => 409;


        public ProductAlreadyExistException(string productName, Exception? innerException = null)
            : base(innerException)
        {
            _productName = productName;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
            defaultInterpolatedStringHandler.AppendLiteral($"product with name ");
            defaultInterpolatedStringHandler.AppendFormatted(_productName);
            defaultInterpolatedStringHandler.AppendLiteral(" already exist.");
            ErrorMessage = defaultInterpolatedStringHandler.ToStringAndClear();
        }
    }
}
