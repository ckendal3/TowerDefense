using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class FindTargetSystem : JobComponentSystem
{
    private EntityQuery Query;

    [ExcludeComponent(typeof(LookAtTarget))]
    [BurstCompile]
    public struct FindTargetJob : IJobForEachWithEntity<Translation, FindTarget>
    {
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<Translation> enemyTranslations;

        [DeallocateOnJobCompletion]
        [ReadOnly]
        public NativeArray<Entity> badEntities;

        [WriteOnly] public NativeQueue<Entity>.ParallelWriter entityQueue;
        [WriteOnly] public NativeQueue<LookAtTarget>.ParallelWriter lookAtQueue;


        public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref FindTarget findTarget)
        {
            Entity targetEntity = badEntities[0];
            float closest = math.distance(enemyTranslations[0].Value, translation.Value);
            float nextDistance;

            for (int i = 0; i < enemyTranslations.Length; i++)
            {
                nextDistance = math.distance(enemyTranslations[0].Value, translation.Value);

                if (closest > nextDistance)
                {
                    targetEntity = badEntities[i];
                    closest = nextDistance;
                }
            }

            if(closest < findTarget.Range)
            {
                entityQueue.Enqueue(entity);
                lookAtQueue.Enqueue(new LookAtTarget { Entity = targetEntity });
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var addQueue = new NativeQueue<Entity>();
        var lookAtQueue = new NativeQueue<LookAtTarget>();

        JobHandle findJob = new FindTargetJob
        {
            enemyTranslations = Query.ToComponentDataArray<Translation>(Allocator.TempJob),
            badEntities = Query.ToEntityArray(Allocator.TempJob),
            entityQueue = addQueue.AsParallelWriter(),
            lookAtQueue = lookAtQueue.AsParallelWriter()
        }.Schedule(this, inputDeps);
        findJob.Complete();
        
        while(addQueue.Count > 0)
        {
            EntityManager.AddComponentData(addQueue.Dequeue(), lookAtQueue.Dequeue());
        }

        return findJob;
    }

    protected override void OnCreate()
    {
        Query = GetEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[] { ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<BadFactionTag>() }
        });
    }
}
