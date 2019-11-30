using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class FindTargetSystem : JobComponentSystem
{
    private EntityQuery Query;

    [BurstCompile]
    public struct FindTargetJob : IJobForEachWithEntity<Translation, FindTarget, LookAtTarget>
    {
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<Translation> enemyTranslations;

        [DeallocateOnJobCompletion]
        [ReadOnly]
        public NativeArray<Entity> badEntities;

        public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref FindTarget findTarget, ref LookAtTarget lookAt)
        {
            // only find closest if don't currently have one
            if(lookAt.Entity == Entity.Null)
            {
                Entity targetEntity = badEntities[0];
                float closest = math.distance(enemyTranslations[0].Value, translation.Value);
                float nextDistance;

                for (int i = 0; i < enemyTranslations.Length; i++)
                {
                    nextDistance = math.distance(enemyTranslations[i].Value, translation.Value);

                    // if within range and it is closwer
                    if(nextDistance <= findTarget.Range && nextDistance < closest)
                    {
                        targetEntity = badEntities[i];
                        closest = nextDistance;

                        lookAt.Entity = targetEntity;
                    }
                }              
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle findJob = new FindTargetJob
        {
            enemyTranslations = Query.ToComponentDataArray<Translation>(Allocator.TempJob),
            badEntities = Query.ToEntityArray(Allocator.TempJob)
        }.Schedule(this, inputDeps);

        return findJob;
    }

    protected override void OnCreate()
    {
        Query = GetEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[] { ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<BadFactionTag>(), ComponentType.ReadOnly<TrackableTag>() }
        });
    }

}
