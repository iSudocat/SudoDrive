using System;

namespace Server.Models.Entities
{
    public interface ITimeStampedModel
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}