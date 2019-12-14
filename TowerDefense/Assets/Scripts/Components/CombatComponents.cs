using Unity.Entities;

public struct SineMovement : IComponentData
{
    public float X;
    public float Y;
    public float Z;
}

/// ******************** TAGS ****************************

public struct Faction : IComponentData
{
    public FactionType Type;
}

public struct BadFactionTag : IComponentData { }

public enum FactionType
{
    Player,
    Bad,
    Neutral
}
