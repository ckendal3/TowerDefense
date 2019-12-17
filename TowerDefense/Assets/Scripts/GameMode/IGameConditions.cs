using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameConditions
{
    void Update();
    void Initialize(WaveData waveData);
    bool StartRound();
    void StartGame();
    bool EndRound();
    void EndGame();
}
