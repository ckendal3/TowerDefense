using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// This updates the camera transform to the creator's position and rotation
/// </summary>
public class CameraToCreatorSystem : ComponentSystem
{
    private Camera camera;

    protected override void OnUpdate()
    {
        Entities.WithAll<CreatorTag>().ForEach((ref Translation position, ref Rotation rotation) =>
            {
                camera.transform.position = position.Value;
                camera.transform.rotation = rotation.Value;
            });
    }

    protected override void OnCreate()
    {
        camera = Camera.main;
    }
}
