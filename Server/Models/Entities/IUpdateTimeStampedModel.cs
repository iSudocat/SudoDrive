using System;

namespace Server.Models.Entities
{
    public interface IUpdateTimeStampedModel
    {
        DateTime UpdatedAt { get; set; }
    }
}