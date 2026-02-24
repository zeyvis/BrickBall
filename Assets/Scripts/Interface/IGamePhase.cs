
public interface IGamePhase
{
    float Duration { get; }
    void Enter();


    void Update();


    void Exit();
}