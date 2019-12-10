public interface IGameType
{
    void Initialize();
    void StartRound();
    void EndRound();
    void IncreaseScore(int amount);
    void DecreaseScore(int amount);
}
