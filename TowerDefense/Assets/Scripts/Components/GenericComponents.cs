using Unity.Entities;
using Unity.Mathematics;

public struct GoToPosition : IComponentData
{
    public float3 Value;
}

public struct Speed : IComponentData
{
    public float Value;
}

public struct Health : IComponentData
{
    public float Value;
    public float MaxValue;
}
