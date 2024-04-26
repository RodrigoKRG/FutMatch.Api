using FutMatch.Domain.Common.Extensions;
using System.Runtime.Serialization;

namespace FutMatch.Domain.Common.Exceptions;

[Serializable]
public class EntityNotFoundException : Exception, IBusinessException
{
    public EntityNotFoundException(ExceptionInfo exceptionInfo) : base(exceptionInfo.ToMessage())
    {
    }

    public EntityNotFoundException(IEnumerable<ExceptionInfo> exceptionInfos)
        : base(exceptionInfos.ToMessage())
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}