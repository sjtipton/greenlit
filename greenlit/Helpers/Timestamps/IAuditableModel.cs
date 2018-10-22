using System;

namespace greenlit.Helpers.Timestamps
{
    public interface IAuditableModel
    {
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; set; }
    }
}
