namespace BOC.StateMachine
{
    public class UnitIdleState : UnitState
    {
        public UnitStateId GetId()
        {
            return UnitStateId.Idle;
        }
        
        public void Enter(AIUnit _unit)
        {
            _unit.unitMovement.WalkingPressed = false;
            _unit.unitMovement.movementSpeed = 0;
        }
        
        public void Update(AIUnit _unit)
        {
        }
        
        public void Exit(AIUnit _unit)
        {
        }
    }

}
