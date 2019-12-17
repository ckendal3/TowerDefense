using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// TODO: Have controllable parameters
// TODO: Cache archetypes somewhere to save on creation????
public static class CreatorFactory
{
    private static EntityManager EntityManager;
    private static EntityArchetype Archetype;
    //private static TowerParameters Parameters;

    static CreatorFactory()
    {
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        //TODO: Implement EntityArechtype for creator
        Archetype = EntityManager.CreateArchetype(new ComponentType[]
            {
                ComponentType.ReadWrite<LocalToWorld>(), ComponentType.ReadWrite<Translation>(), ComponentType.ReadWrite<Rotation>(),
                ComponentType.ReadOnly<CreatorTag>()
            });
    }

    public static void CreateCreator(int ID, float3 position, quaternion rotation)
    {

    }
}
