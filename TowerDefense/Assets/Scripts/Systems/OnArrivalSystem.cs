using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//TODO: Convert to JobComponentSystem
public class OnArrivalSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref Translation position, ref GoToPosition toPosition) =>
        {
            float distance = math.distance(position.Value, toPosition.Value);

            // within error check
            if(distance < .1f)
            {
                EntityManager.RemoveComponent<GoToPosition>(entity);
                EntityManager.SetComponentData(entity, new Speed { Value = 0f });
            }

        });
    }
}
