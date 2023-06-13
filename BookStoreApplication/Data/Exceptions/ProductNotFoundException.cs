using BookStoreApplicationAPI.Data.Entities;
using System.Runtime.CompilerServices;

namespace BookStoreApplicationAPI.Data.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public int? _productId { get; }

        public override string ErrorMessage { get; }

        public override short ErrorCode => 404;

        public EntityNotFoundException(string message, Exception? innerException = null)
            : base(innerException)
        {
            ErrorMessage = message;
        }

        public EntityNotFoundException(int productId, Type type, Exception? innerException = null)
            : base(innerException)
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);            
            defaultInterpolatedStringHandler.AppendLiteral($"{type.Name} with Id = ");
            defaultInterpolatedStringHandler.AppendFormatted(productId);
            defaultInterpolatedStringHandler.AppendLiteral(" not found.");
            ErrorMessage = defaultInterpolatedStringHandler.ToStringAndClear();
        }
    }
}
