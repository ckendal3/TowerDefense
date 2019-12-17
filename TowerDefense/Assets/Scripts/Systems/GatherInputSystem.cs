using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// TODO: Use axis and stuff / new input stuff for continuity such as mobile vs pc
public class GatherInputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref InputState state) =>
        {
            state.PrimaryAction = Input.GetKeyDown(KeyCode.Mouse0);
            state.SecondaryAction = Input.GetKeyDown(KeyCode.Mouse1);
        });

        Entities.ForEach((ref InputState state) =>
        {
            state.MousePosition = GetMouseToWorldPosition();
        });
    }

    private float3 GetMouseToWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return float3.zero;
    }
}
