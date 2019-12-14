using Unity.Collections;
using Unity.Entities;

// TODO: Convert to JobComponentSystem
// TODO: Get all entities to destroy at once and batch destroy
public class DestroySystem : ComponentSystem
{
    protected override void OnUpdate()
    { 
        Entities.ForEach((Entity entity, ref Health health) =>
        {
            if (health.Value <= 0f)
            {
                EntityManager.DestroyEntity(entity);
            }
        });
    }
}
