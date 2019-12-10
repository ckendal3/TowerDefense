using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerManager
{
    private static EntityManager EntityManager;
    public static List<Player> Players { private set; get; }

    public void Initialize()
    {
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Players = new List<Player>(1);
    }

    public bool AddPlayer(Player player)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].ID == player.ID)
            {
                Debug.LogWarning($"WARNING: Failed to add player. Player ID {player.ID} already exists.");
                return false;
            }
        }

        Players.Add(player);
        return false;
    }

    public bool RemovePlayer(int ID)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].ID == ID)
            {
                Players.RemoveAt(i);
                return true;
            }
        }

        Debug.LogWarning($"WARNING: Failed to remove player. Player ID {ID} does not exist.");
        return false;
    }

    public bool GetPlayerByID(int ID, out Player player)
    {
        player = null;
        for (int i = 0; i < Players.Count; i++)
        {
            if(Players[i].ID == ID)
            {
                player = Players[i];
                return true;
            }
        }

        return false;
    }

    public bool GiveControl(int ID, Entity entity)
    {
        if(entity == Entity.Null)
        {
            return false;
        }

        if(EntityManager.HasComponent<OwnerID>(entity))
        {
            EntityManager.SetComponentData(entity, new OwnerID { Value = ID });
        }
        else
        {
            EntityManager.AddComponentData(entity, new OwnerID { Value = ID });
        }

        return true;
    }

    public void RelinquishControl(int playerID, Entity entity)
    {
        if (entity == Entity.Null)
        {
            return;
        }

        if (EntityManager.HasComponent<OwnerID>(entity) && EntityManager.GetComponentData<OwnerID>(entity).Value == playerID)
        {
            EntityManager.RemoveComponent<OwnerID>(entity);
        }
    }
}

public class Player
{
    public int ID;

    public InputState inputState;
}

public struct InputState : IComponentData 
{

}

public struct OwnerID : IComponentData
{
    public int Value;
}
