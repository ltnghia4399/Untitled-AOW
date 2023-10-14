namespace BOC.StateMachine
{
    public class UnitAttackState : UnitState
    {
        public UnitStateId GetId()
        {
            return UnitStateId.Attack;
        }

        public void Enter(AIUnit _unit)
        {
            _unit.unitMovement.AttackPressed = true;
        }

        public void Update(AIUnit _unit)
        {
        }

        public void Exit(AIUnit _unit)
        {
            _unit.unitMovement.AttackPressed = false;
        }
    }

}
