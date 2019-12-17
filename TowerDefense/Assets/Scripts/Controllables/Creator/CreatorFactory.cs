using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using WDIB.Components;

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
                ComponentType.ReadOnly<CreatorTag>(), ComponentType.ReadWrite<OwnerID>()
            });
    }

    public static void CreateCreator(float3 position, quaternion rotation, uint ID)
    {
        Entity entity;

        // create the entity to clone from
        entity = EntityManager.CreateEntity(Archetype);

        SetComponents(entity, position, rotation, ID);
    }

    private static void SetComponents(Entity entity, float3 spawnPos, quaternion spawnRot, uint ownerID)
    {
        #if UNITY_EDITOR
        EntityManager.SetName(entity, "Creator");
        #endif

        //Generic Components
        EntityManager.SetComponentData(entity, new Translation { Value = spawnPos });
        EntityManager.SetComponentData(entity, new Rotation { Value = spawnRot });
        EntityManager.SetComponentData(entity, new OwnerID { Value = ownerID });
    }
}