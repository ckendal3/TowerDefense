using UnityEngine;

public class WaveGameType : IGameType
{
    public void Initialize()
    {
        Debug.Log($"Initializing {this.GetType()}!");
    }

    public void StartRound()
    {
        Debug.Log("Start Round");
    }

    public void EndRound()
    {
        Debug.Log("End Round");
    }

    public void IncreaseScore(int amount)
    {
        Debug.Log($"Increase score by {amount}!");
    }

    public void DecreaseScore(int amount)
    {
        Debug.Log($"Decrease score by {amount}!");
    }


}
