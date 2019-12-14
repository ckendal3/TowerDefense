// Clump is a way to describe each spawn of a group of enemies inside of a wave
public struct WaveData
{
    public float Interval; // how often to spawn
    public int ClumpCount; // how many to spawn at once

    public int MaxCount; // max to spawn
    public int MaxAlive; // max that can be alive <- might not be needed here <- probably shouldn't be

    public int InitialCount; // initial amount to spawn
}