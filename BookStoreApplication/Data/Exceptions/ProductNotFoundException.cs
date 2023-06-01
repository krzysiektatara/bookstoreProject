using System.Runtime.CompilerServices;

namespace BookStoreApplicationAPI.Data.Exceptions
{
    public class ProductNotFoundException : DomainException
    {
        public int? _productId { get; }

        public override string ErrorMessage { get; }

        public override short ErrorCode => 404;

        public ProductNotFoundException(string message, Exception? innerException = null)
            : base(innerException)
        {
            ErrorMessage = message;
        }

        public ProductNotFoundException(int productId, Exception? innerException = null)
            : base(innerException)
        {
            _productId = productId;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
            defaultInterpolatedStringHandler.AppendLiteral("Product with Id = ");
            defaultInterpolatedStringHandler.AppendFormatted(_productId);
            defaultInterpolatedStringHandler.AppendLiteral(" not found.");
            ErrorMessage = defaultInterpolatedStringHandler.ToStringAndClear();
        }
    }
}
