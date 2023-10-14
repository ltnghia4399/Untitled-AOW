namespace BOC.StateMachine
{
    public class UnitStateMachine
    {
        public UnitState[] states;
        public AIUnit unit;
        public UnitStateId currentState;

        public UnitStateMachine(AIUnit _unit)
        {
            this.unit = _unit;
            int numState = System.Enum.GetNames(typeof(UnitStateId)).Length;
            states = new UnitState[numState];
        }
        
        public void RegisterState(UnitState _state)
        {
            int index = (int)_state.GetId();
            states[index] = _state; 
        }

        public UnitState GetState(UnitStateId _stateId)
        {
            int index = (int)_stateId;
            return states[index];
        }

        public void Update()
        {
            GetState(currentState)?.Update(unit);
        }

        public void ChangeState(UnitStateId newState)
        {
            GetState(currentState)?.Exit(unit);
            currentState = newState;
            GetState(currentState)?.Enter(unit);
        }
    }
}
