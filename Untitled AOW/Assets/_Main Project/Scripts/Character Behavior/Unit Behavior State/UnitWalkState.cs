namespace BOC.StateMachine
{
    public class UnitWalkState : UnitState
    {
        public UnitStateId GetId()
        {
            return UnitStateId.Walk;
        }
        
        public void Enter(AIUnit _unit)
        {
            _unit.unitMovement.WalkingPressed = true;
            _unit.unitMovement.movementSpeed = _unit.unitConfig.moveSpeed;
        }

        public void Update(AIUnit _unit)
        {
        }

        public void Exit(AIUnit _unit)
        {
            _unit.unitMovement.movementSpeed = 0f;
            _unit.unitMovement.WalkingPressed = false;
        }
    }

}
