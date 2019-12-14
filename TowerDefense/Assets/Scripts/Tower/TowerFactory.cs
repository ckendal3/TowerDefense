using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public static class TowerFactory
{
    private static EntityManager EntityManager;
    private static EntityArchetype Archetype;
    private static TowerParameters Parameters;

    static TowerFactory()
    {
        Parameters = TowerParameters.Instance;

        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        //TODO: Implement EntityArechtype for towers
        Archetype = EntityManager.CreateArchetype(new ComponentType[]
            { 
                ComponentType.ReadWrite<LocalToWorld>(), ComponentType.ReadWrite<Translation>(), ComponentType.ReadWrite<Rotation>(),
                ComponentType.ReadWrite<Targetting>()
            });
    }

    public static void CreateTower(int ID, float3 position, quaternion rotation)
    {
        // TODO: Create an EGO pair
        // Gameobject -> Rendering, Collision
        // Entity -> Rotation, Position, Weapons, LookAt

        GameObject go = Object.Instantiate(Parameters.GetTowerPrefab(ID));
        Entity entity = EntityManager.CreateEntity(Archetype);

        EntityManager.AddSharedComponentData(entity, new RenderMesh
        {
            mesh = go.GetComponent<MeshFilter>().sharedMesh,
            material = go.GetComponent<MeshRenderer>().sharedMaterial
        });

#if UNITY_EDITOR
        go.name = "Tower";
        EntityManager.SetName(entity, "Tower");
#endif

        SetEntityComponents(entity, position, rotation);

        SetGameObjectComponents(go, position, rotation);

        SetHybridComponents(entity, go);

        Object.Destroy(go);
    }

    private static void SetEntityComponents(Entity entity, float3 position, quaternion rotation)
    {
        EntityManager.SetComponentData(entity, new Translation { Value = position });
        EntityManager.SetComponentData(entity, new Rotation { Value = rotation });
        EntityManager.SetComponentData(entity, new Targetting { Entity = Entity.Null, Range = 20f, Type = TrackingType.LookAt });
    }

    private static void SetGameObjectComponents(GameObject go, float3 position, quaternion rotation)
    {
        go.transform.position = position;
        go.transform.rotation = rotation;
    }

    private static void SetHybridComponents(Entity entity, GameObject go)
    {
        //TODO: Implement EGO connection here
    }
}
