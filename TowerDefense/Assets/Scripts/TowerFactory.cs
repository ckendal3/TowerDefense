using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public static class TowerFactory
{
    private static EntityManager EntityManager;
    private static EntityArchetype Archetype;

    static TowerFactory()
    {
        EntityManager = World.Active.EntityManager;

        //TODO: Implement EntityArechtype for towers
        Archetype = EntityManager.CreateArchetype(new ComponentType[]
            { 
                ComponentType.ReadWrite<LocalToWorld>(), ComponentType.ReadWrite<Translation>(), ComponentType.ReadWrite<Rotation>()
            });

    }

    public static void CreateTower(float3 position, quaternion rotation)
    {
        // TODO: Create an EGO pair
        // Gameobject -> Rendering, Collision
        // Entity -> Rotation, Position, Weapons, LookAt

        GameObject go = new GameObject();
        Entity entity = EntityManager.CreateEntity(Archetype);

#if UNITY_EDITOR
        go.name = "Tower";
        EntityManager.SetName(entity, "Tower");
#endif

        SetEntityComponents(entity, position, rotation);

        SetGameObjectComponents(go, position, rotation);

        SetHybridComponents(entity, go);
    }

    private static void SetEntityComponents(Entity entity, float3 position, quaternion rotation)
    {
        EntityManager.SetComponentData(entity, new Translation { Value = position });
        EntityManager.SetComponentData(entity, new Rotation { Value = rotation });
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
