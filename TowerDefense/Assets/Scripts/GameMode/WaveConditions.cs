public class WaveConditions : IGameConditions
{
    private WaveData data;
    private float timeElapsed;

    public void Initialize(WaveData waveData)
    {
        data = waveData;

        timeElapsed = data.Interval;
    }

    public void Update()
    {
        if (!(timeElapsed > 0))
        {
            // TODO: Spawn Enemies based on wavedata

            timeElapsed = data.Interval;
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
