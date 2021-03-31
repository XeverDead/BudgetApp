using System;

namespace Common.Entities
{
    public interface IAppEntity : ICloneable
    {
        long Id { get; }
    }
}
