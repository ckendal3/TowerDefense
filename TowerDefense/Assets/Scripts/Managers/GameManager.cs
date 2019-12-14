using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static IGameType GameType { private set; get; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        GameType = new WaveGameType();

        GameType.Initialize();
    }

    private static void Initialize(IGameType gameType)
    {
        GameType = gameType;

        GameType.Initialize();
    }
}
