using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class LookAtSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithoutBurst().ForEach((ref Rotation rotation, in Translation position, in LookAtTarget lookAt) =>
        {
            float3 lookAtPos = EntityManager.GetComponentData<Translation>(lookAt.Entity).Value;
            float3 diff = math.normalize(lookAtPos - position.Value);

            rotation = new Rotation { Value = quaternion.LookRotation(diff, math.up()) };
        }).Run();

        return inputDeps;
    }
}
