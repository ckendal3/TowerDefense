using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveSystem : JobComponentSystem
{
    [BurstCompile]
    [ExcludeComponent(typeof(GoToPosition))]
    public struct MoveForwardJob : IJobForEach<Translation, Rotation, Speed>
    {
        [ReadOnly] public float DeltaTime;

        public void Execute(ref Translation position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Speed speed)
        {
            float3 newPos = position.Value + (math.forward(rotation.Value) * (speed.Value * DeltaTime));

            position.Value = newPos;
        }
    }

    [BurstCompile]
    public struct MoveToPositionJob : IJobForEach<Translation, Rotation, Speed, GoToPosition>
    {
        [ReadOnly] public float DeltaTime;

        public void Execute(ref Translation position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Speed speed, [ReadOnly] ref GoToPosition toPosition)
        {
            float3 diff = math.normalize(toPosition.Value - position.Value);
            quaternion direction = quaternion.LookRotation(diff, math.up());

            float3 newPos = position.Value + (math.forward(direction) * (speed.Value * DeltaTime));

            position.Value = newPos;
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle forwardJob = new MoveForwardJob
        {
            DeltaTime = Time.DeltaTime
        }.Schedule(this, inputDeps);

        JobHandle toPosition = new MoveToPositionJob
        {
            DeltaTime = Time.DeltaTime
        }.Schedule(this, forwardJob);


        return toPosition;
    }
}
