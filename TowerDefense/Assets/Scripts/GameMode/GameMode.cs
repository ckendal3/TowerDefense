using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    private IGameConditions conditions;
    private MatchState state;

    public void Initialize(IGameConditions gameConditions, MatchState matchState)
    {
        conditions = gameConditions;
        state = matchState;
    }

    public void UpdateState(MatchState matchState)
    {
        state = matchState;
    }

    public void Update()
    {
        if(state == MatchState.InProgress)
        {
            conditions.Update();
        }
    }

    public void StartRound()
    {
        if (conditions.StartRound() == true)
        {
            // TODO: Display menu and stuff
        }
    }

    public void EndRound()
    {
        if (conditions.EndRound() == true)
        {
            // TODO: Display menu and stuff
        }
    }

    public void StarGame()
    {
        conditions.StartGame();
    }

    public void EndGame()
    {
        conditions.EndGame();

    }
}
