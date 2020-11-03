using System;

namespace Server.Models.Entities
{
    public interface ICreateTimeStampedModel
    {
        DateTime CreatedAt { get; set; }
    }
}