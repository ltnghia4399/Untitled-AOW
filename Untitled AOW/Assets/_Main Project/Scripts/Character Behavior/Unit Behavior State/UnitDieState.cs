using UnityEngine;

namespace BOC.StateMachine
{
    public class UnitDieState : UnitState
    {
        public UnitStateId GetId()
        {
            return UnitStateId.Die;
        }
        public void Enter(AIUnit _unit)
        {
            _unit.unitHealth.DestroyOnDie();
        }

        public void Update(AIUnit _unit)
        {
        }

        public void Exit(AIUnit _unit)
        {
        }
    }

}
