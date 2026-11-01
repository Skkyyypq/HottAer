
using HottAer.Domain;

namespace HottAer.UnitTests;

public class DeviceTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesDevice()
    {
        var id = Guid.NewGuid();
        var displayName = "iPhone";
        var firstSeenAt = DateTimeOffset.UtcNow;
        var lastSeenAt = firstSeenAt.AddMinutes(1);

        var device = new Device(
            id,
            displayName,
            firstSeenAt,
            lastSeenAt);

        Assert.Equal(id, device.Id);
        Assert.Equal(displayName, device.DisplayName);
        Assert.Equal(firstSeenAt, device.FirstSeenAt);
        Assert.Equal(lastSeenAt, device.LastSeenAt);
    }

    [Fact]
    public void Constructor_WithEmptyId_ThrowsArgumentException()
    {
        var now = DateTimeOffset.UtcNow;

        Assert.Throws<ArgumentException>(() =>
            new Device(Guid.Empty, "iPhone", now, now));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Constructor_WithInvalidDisplayName_ThrowsArgumentException(
        string displayName)
    {
        var now = DateTimeOffset.UtcNow;

        Assert.Throws<ArgumentException>(() =>
            new Device(Guid.NewGuid(), displayName, now, now));
    }

    [Fact]
    public void Constructor_WithLastSeenBeforeFirstSeen_ThrowsArgumentException()
    {
        var now = DateTimeOffset.UtcNow;

        Assert.Throws<ArgumentException>(() =>
            new Device(
                Guid.NewGuid(),
                "iPhone",
                now,
                now.AddMinutes(-1)));
    }

    [Fact]
    public void WithLastSeenAt_ReturnsNewDevice()
    {
        var firstSeenAt = DateTimeOffset.UtcNow;
        var lastSeenAt = firstSeenAt.AddMinutes(1);

        var device = new Device(
            Guid.NewGuid(),
            "iPhone",
            firstSeenAt,
            lastSeenAt);

        var newLastSeenAt = lastSeenAt.AddMinutes(1);

        var updatedDevice = device.WithLastSeenAt(newLastSeenAt);

        Assert.NotSame(device, updatedDevice);
        Assert.Equal(lastSeenAt, device.LastSeenAt);
        Assert.Equal(newLastSeenAt, updatedDevice.LastSeenAt);
    }

    [Fact]
    public void WithLastSeenAt_WithEarlierTimestamp_ThrowsArgumentException()
    {
        var now = DateTimeOffset.UtcNow;

        var device = new Device(
            Guid.NewGuid(),
            "iPhone",
            now,
            now.AddMinutes(2));

        Assert.Throws<ArgumentException>(() =>
        {
            device.WithLastSeenAt(now.AddMinutes(1));
        });
    }
}
