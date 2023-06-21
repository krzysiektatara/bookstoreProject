using BookStoreApplicationAPI.Data.Entities;
using System.Runtime.CompilerServices;

namespace BookStoreApplicationAPI.Data.Exceptions
{
    public class RequestedItemIsUnavailableException : DomainException
    {
        public int? _productId { get; }

        public override string ErrorMessage { get; }

        public override short ErrorCode => 400;

        public RequestedItemIsUnavailableException(string message, Exception? innerException = null)
            : base(innerException)
        {
            ErrorMessage = message;
        }

        public RequestedItemIsUnavailableException(int requestedQuantity, int availableQuantity, Exception? innerException = null)
            : base(innerException)
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);            
            defaultInterpolatedStringHandler.AppendLiteral($"available item quantity = ");
            defaultInterpolatedStringHandler.AppendFormatted(availableQuantity);
            defaultInterpolatedStringHandler.AppendLiteral($"requested item quantity = ");
            defaultInterpolatedStringHandler.AppendFormatted(requestedQuantity);
            ErrorMessage = defaultInterpolatedStringHandler.ToStringAndClear();
        }
    }
}
