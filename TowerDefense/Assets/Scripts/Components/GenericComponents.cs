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

public struct InputState : IComponentData
{
    public float3 MousePosition;
    
    public bool PrimaryAction;
    public bool SecondaryAction;
}