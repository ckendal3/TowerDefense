using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Rendering;

public class EntityObject : MonoBehaviour
{
    public float SineMovement = 4f;

    private EntityManager EntityManager;

    private EntityArchetype Archetype;

    Entity entity;

    private void Awake()
    {
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Archetype = EntityManager.CreateArchetype(new ComponentType[] 
        {
            ComponentType.ReadWrite<LocalToWorld>(),
            ComponentType.ReadWrite<Rotation>(), ComponentType.ReadWrite<Translation>(),
            ComponentType.ReadWrite<BadFactionTag>(), ComponentType.ReadOnly<SineMovement>()
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        entity = EntityManager.CreateEntity(Archetype);

        EntityManager.AddSharedComponentData(entity, new RenderMesh { mesh = GetComponent<MeshFilter>().sharedMesh, material = GetComponent<MeshRenderer>().sharedMaterial });
        EntityManager.SetComponentData(entity, new Translation { Value = transform.position });
        EntityManager.SetComponentData(entity, new Rotation { Value = transform.rotation });
        EntityManager.SetComponentData(entity, new SineMovement { Value = SineMovement });

        Destroy(this.gameObject);
    }
}
