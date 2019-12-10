using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class LookAtSystem : JobComponentSystem
{
    private EntityQuery Query;

    [BurstCompile]
    public struct LookAtJob : IJobForEach<Rotation, Translation, LookAtTarget>
    {
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<Entity> lookAtEntities;
        
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<Translation> lookAtPositions;

        public void Execute([WriteOnly] ref Rotation rotation, [ReadOnly] ref Translation position, [ReadOnly] ref LookAtTarget lookAt)
        {
            for (int i = 0; i < lookAtEntities.Length; i++)
            {
                if (lookAtEntities[i].Index == lookAt.Entity.Index)
                {
                    float3 diff = math.normalize(lookAtPositions[i].Value - position.Value);

                    rotation = new Rotation { Value = quaternion.LookRotation(diff, math.up()) };
                }
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle lookAtHandle = new LookAtJob
        {
            lookAtEntities = Query.ToEntityArray(Allocator.TempJob),
            lookAtPositions = Query.ToComponentDataArray<Translation>(Allocator.TempJob)
        }.Schedule(this, inputDeps);

        return lookAtHandle;
    }

    protected override void OnCreate()
    {
        Query = GetEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[] 
            {
                ComponentType.ReadOnly<Entity>(), ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<TrackableTag>()
            }
        });
    }
}
