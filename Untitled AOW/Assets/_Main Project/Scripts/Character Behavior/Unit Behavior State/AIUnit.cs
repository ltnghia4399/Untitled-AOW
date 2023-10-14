using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BOC.UnitProperties;
using BOC.SO;

namespace BOC.StateMachine
{
    public class AIUnit : MonoBehaviour
    {
        public enum AIType {PLAYER1, PLAYER2 }

        public AIType Type = AIType.PLAYER1;

        public UnitStateMachine unitStateMachine;
        public bool isEnemy = false;
        public CharacterSO unitConfig;
        [SerializeField] UnitStateId initialState = UnitStateId.Walk;

        public UnitMovement unitMovement;
        public UnitHealth unitHealth;
        private void Start()
        {
            unitStateMachine = new UnitStateMachine(this);
            unitStateMachine.RegisterState(new UnitWalkState());
            unitStateMachine.RegisterState(new UnitIdleState());
            unitStateMachine.RegisterState(new UnitAttackState());
            unitStateMachine.RegisterState(new UnitDieState());
            unitStateMachine.ChangeState(initialState);
        }

        private void Update()
        {
            unitStateMachine.Update();
        }

        public void OnMouseDown()
        {
            BOC.Camera.CameraController.Instance.followTaget = this.transform;
        }
    }

}
