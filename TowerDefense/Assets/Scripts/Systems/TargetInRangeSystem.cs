using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

// TODO: Fix how targeting works
public class TargetInRangeSystem : JobComponentSystem
{
    public EntityQuery Query;

    [BurstCompile]
    public struct TargetInRangeJob : IJobForEach<LookAtTarget, FindTarget, Translation>
    {
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<Entity> lookAtEntities;

        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<Translation> lookAtPositions;

        public void Execute(ref LookAtTarget lookAt, [ReadOnly] ref FindTarget findTarget, [ReadOnly] ref Translation position)
        {
            if(lookAt.Entity != Entity.Null)
            {
                float distance;
                for (int i = 0; i < lookAtEntities.Length; i++)
                {

                    if (lookAtEntities[i].Index == lookAt.Entity.Index)
                    {
                        distance = math.distance(lookAtPositions[i].Value, position.Value);

                        // if not in range set no entity
                        if (distance > findTarget.Range)
                        {
                            lookAt.Entity = Entity.Null;
                        }
                    }
                }
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle inRangeJob = new TargetInRangeJob
        {
            lookAtEntities = Query.ToEntityArray(Allocator.TempJob),
            lookAtPositions = Query.ToComponentDataArray<Translation>(Allocator.TempJob)
        }.Schedule(this, inputDeps);

        return inRangeJob;
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
