using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities.Contract
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
