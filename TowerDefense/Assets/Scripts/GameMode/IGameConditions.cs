using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameConditions
{
    void Update();
    void Initialize();
    bool StartRound();
    void StartGame();
    bool EndRound();
    void EndGame();
}
