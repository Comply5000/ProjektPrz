using API.Common.Enums;

namespace API.Common.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public EntryStatus EntryStatus { get; set; }
}