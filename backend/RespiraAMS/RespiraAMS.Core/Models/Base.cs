using System;

namespace RespiraAMS.Core.Models;

public class Base
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTimeOffset CreatedAt { get; set; } =  DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } =  DateTimeOffset.UtcNow;
    public bool IsDeleted { get; set; } = false;
}