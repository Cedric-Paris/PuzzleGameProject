public interface IMovementProcedure
{
    void ProcessPhase(PlayerMovementController p);
    void OnMovementFinishedForCurrentFrame(PlayerMovementController p);
    void OnMovementEnding(PlayerMovementController p);
}
