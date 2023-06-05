using System.Runtime.CompilerServices;
using System.Text;

namespace BookStoreApplicationAPI.Data.Filters
{
    public record ErrorDescription(Guid Id, string Title, short StatusCode)
    {
        [CompilerGenerated]
        protected virtual Type EqualityContract
        {
            [CompilerGenerated]
            get
            {
                return typeof(ErrorDescription);
            }
        }

        //public ErrorDescription(Guid Id, string Title, short StatusCode)
        //{
        //    this.Id = ;
        //    this.Title = Title;
        //    this.StatusCode = StatusCode;
        //    base._002Ector();
        //}

        [CompilerGenerated]
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("ErrorDescription");
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(' ');
            }

            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }

        [CompilerGenerated]
        protected virtual bool PrintMembers(StringBuilder builder)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack();
            builder.Append("Id = ");
            builder.Append(Id.ToString());
            builder.Append(", Title = ");
            builder.Append((object?)Title);
            builder.Append(", StatusCode = ");
            builder.Append(StatusCode.ToString());
            return true;
        }

        [CompilerGenerated]
        public override int GetHashCode()
        {
            return ((EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title)) * -1521134295 + EqualityComparer<short>.Default.GetHashCode(StatusCode);
        }

        [CompilerGenerated]
        public virtual bool Equals(ErrorDescription? other)
        {
            if ((object)this != other)
            {
                if ((object)other != null && EqualityContract == other!.EqualityContract && EqualityComparer<Guid>.Default.Equals(Id, other!.Id) && EqualityComparer<string>.Default.Equals(Title, other!.Title))
                {
                    return EqualityComparer<short>.Default.Equals(StatusCode, other!.StatusCode);
                }

                return false;
            }

            return true;
        }

        [CompilerGenerated]
        protected ErrorDescription(ErrorDescription original)
        {
            Id = original.Id;
            Title = original.Title;
            StatusCode = original.StatusCode;
        }
    }
}

