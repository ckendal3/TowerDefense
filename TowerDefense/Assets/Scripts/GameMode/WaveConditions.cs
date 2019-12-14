using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConditions : IGameConditions
{
    private WaveData data;
    private float timeElapsed;

    public void Initialize()
    {
        // TODO: Set time for interval
        // timeElapsed = data.Interval;
    }

    public void Update()
    {
        if (!(timeElapsed > 0))
        {
            // TODO: Spawn Enemies based on wavedata

            // TODO: Set time for interval
            // timeElapsed = data.Interval;
        }
    }

    public bool StartRound()
    {
        // TODO: Start spawning enemies
        return true;
    }

    public bool EndRound()
    {
        return true;
    }

    public void StartGame()
    {
        // TODO: Create the creator player
    }

    public void EndGame()
    {
        // TODO: Save stuff?
    }
}
