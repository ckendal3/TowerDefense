using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Rendering;
using Unity.Mathematics;

public class EntityObject : MonoBehaviour
{
    public Transform GoToPosition;
    public float Speed;

    public float3 SineMovement = new float3(1f, 4f, 0f);

    private EntityManager EntityManager;

    private EntityArchetype Archetype;

    Entity entity;

    private void Awake()
    {
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Archetype = EntityManager.CreateArchetype(new ComponentType[] 
        {
            ComponentType.ReadWrite<LocalToWorld>(),
            ComponentType.ReadWrite<Rotation>(), ComponentType.ReadWrite<Translation>(), //ComponentType.ReadOnly<SineMovement>(),
            ComponentType.ReadWrite<BadFactionTag>(), ComponentType.ReadOnly<TrackableTag>(),
            ComponentType.ReadOnly<Health>()
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        entity = EntityManager.CreateEntity(Archetype);

        EntityManager.AddSharedComponentData(entity, new RenderMesh { mesh = GetComponent<MeshFilter>().sharedMesh, material = GetComponent<MeshRenderer>().sharedMaterial });
        EntityManager.SetComponentData(entity, new Translation { Value = transform.position });
        EntityManager.SetComponentData(entity, new Rotation { Value = transform.rotation });
        EntityManager.SetComponentData(entity, new Health { Value = 100, MaxValue = 100f });
        //EntityManager.SetComponentData(entity, new SineMovement { X = SineMovement.x, Y = SineMovement.y, Z = SineMovement.z });

        if (GoToPosition != null)
        {
            EntityManager.AddComponentData(entity, new GoToPosition { Value = GoToPosition.position });
            EntityManager.AddComponentData(entity, new Speed { Value = Speed });
        }


        Destroy(this.gameObject);
    }
}
