using Unity.Entities;

/// <summary>
/// Find an entity to target with a certain range.
/// </summary>
public struct FindTargetWithinRange : IComponentData
{
    public float Value;
}

/// <summary>
/// Make the rotation of an entity, face another entity
/// </summary>
public struct LookAtTarget : IComponentData
{
    public Entity Entity;
}

/// <summary>
/// Make the owning entity move towards another entity
/// </summary>
public struct MoveTowardsTarget : IComponentData
{
    public Entity Entity;
}


/// <summary>
/// Make the owning entity move towards and look at another entity
/// </summary>
public struct FollowTarget : IComponentData
{
    public Entity Entity;
}

public struct FindTarget : IComponentData
{
    public float Range;
}

public struct SineMovement : IComponentData
{
    public float Value;
}

/// ******************** TAGS ****************************

public struct PlayerFactionTag : IComponentData { }

public struct BadFactionTag : IComponentData { }

public struct NeutralFactionTag : IComponentData { }