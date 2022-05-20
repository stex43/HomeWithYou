using System;

namespace HomeWithYou.API.Infrastructure
{
    public sealed class NotFoundException : Exception
    {
        public string Target { get; }

        public Guid ResourceId { get; }

        public NotFoundException(string target, Guid resourceId) 
            : base($"The resource id={resourceId} of {target} not found.")
        {
            this.Target = target;
            this.ResourceId = resourceId;
        }
    }
}