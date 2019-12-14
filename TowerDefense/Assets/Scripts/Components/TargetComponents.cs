using Unity.Entities;

/// <summary>
/// Find an entity to target with a certain range.
/// </summary>
public struct FindTargetWithinRange : IComponentData
{
    public float Value;
}

public struct Targetting : IComponentData
{
    public float Range;
    public TrackingType Type;
    public Entity Entity;
}

public struct TrackableTag : IComponentData { }

public enum TrackingType
{
    LookAt,
    MoveTowards, 
    All
}