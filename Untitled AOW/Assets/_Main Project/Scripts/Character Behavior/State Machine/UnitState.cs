namespace BOC.StateMachine
{
    public enum UnitStateId
    {
        Idle,
        Walk,
        Attack,
        Die
    }

    public interface UnitState
    {
        void Enter(AIUnit _unit);
        void Update(AIUnit _unit);
        void Exit(AIUnit _unit);
        UnitStateId GetId();
    }
}
