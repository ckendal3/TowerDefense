using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

//TODO: Use new foreach from Entities .2
public class LookAtSystem : JobComponentSystem
{
    public EntityQuery Query;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        int count = Query.CalculateEntityCount();

        var lookAtArray = Query.ToComponentDataArray<LookAtTarget>(Allocator.TempJob);
        var translationArray = Query.ToComponentDataArray<Translation>(Allocator.TempJob);
        var entityArray = Query.ToEntityArray(Allocator.TempJob);

        NativeArray<Rotation> rotationArray = new NativeArray<Rotation>();
        Query.CopyFromComponentDataArray(rotationArray);

        Translation lookAtPos = new Translation { };
        for (int i = 0; i < count; i++)
        {
            if(EntityManager.HasComponent<Translation>(lookAtArray[count].Entity))
            {
                lookAtPos = EntityManager.GetComponentData<Translation>(lookAtArray[count].Entity);
                rotationArray[i] = new Rotation { Value = quaternion.LookRotation(math.normalize(lookAtPos.Value - translationArray[i].Value), math.up()) }; // lookRotation(normalize(diff))
            }
            else
            {
                EntityManager.RemoveComponent<LookAtTarget>(entityArray[i]);
            }
        }

        lookAtArray.Dispose();
        translationArray.Dispose();
        entityArray.Dispose();
        rotationArray.Dispose();

        return inputDeps;
    }

    protected override void OnCreate()
    {
        Query = GetEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[] { ComponentType.ReadOnly<LookAtTarget>(), ComponentType.ReadOnly<Rotation>(), ComponentType.ReadOnly<Translation>() }
        });
    }
}
