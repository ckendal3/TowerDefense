using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SineMovementSystem : JobComponentSystem
{
    [BurstCompile]
    public struct SineMovementJob : IJobForEach<Translation, Rotation, SineMovement>
    {
        public float deltaTime;
        public float time;

        public void Execute(ref Translation position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref SineMovement sine)
        {
            float3 newPos = position.Value;

            newPos.y += (math.sin(time) * deltaTime) * sine.Value;

            position.Value = newPos;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle movementJob = new SineMovementJob
        {
            deltaTime = Time.DeltaTime,
            time = Time.time
        }.Schedule(this, inputDeps);

        return movementJob;
    }
}
