namespace HottAer.Domain;

public sealed class Device
{
    public Guid Id { get; }

    public string DisplayName { get; }

    public DateTimeOffset FirstSeenAt { get; }

    public DateTimeOffset LastSeenAt { get; }

    public Device(
        Guid id,
        string displayName,
        DateTimeOffset firstSeenAt,
        DateTimeOffset lastSeenAt)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "Device ID cannot be empty.",
                nameof(id));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        if (lastSeenAt < firstSeenAt)
        {
            throw new ArgumentException(
                "Last seen time cannot be earlier than first seen time.",
                nameof(lastSeenAt));
        }

        Id = id;
        DisplayName = displayName;
        FirstSeenAt = firstSeenAt;
        LastSeenAt = lastSeenAt;
    }

    public Device WithLastSeenAt(DateTimeOffset lastSeenAt)
    {
        if (lastSeenAt < LastSeenAt)
        {
            throw new ArgumentException(
                "Last seen time cannot move backwards.",
                nameof(lastSeenAt));
        }

        return new Device(
            Id,
            DisplayName,
            FirstSeenAt,
            lastSeenAt);
    }
}
