using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

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

            newPos.x += (math.sin(time) * deltaTime) * sine.X;
            newPos.y += (math.sin(time) * deltaTime) * sine.Y;
            newPos.z += (math.sin(time) * deltaTime) * sine.Z;

            position.Value = newPos;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle movementJob = new SineMovementJob
        {
            deltaTime = Time.DeltaTime,
            time = (float) Time.ElapsedTime
        }.Schedule(this, inputDeps);

        return movementJob;
    }
}
