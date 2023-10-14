using UnityEngine;
using BOC.StateMachine;

namespace BOC.UnitProperties
{
    public class UnitHealth : MonoBehaviour
    {
        [SerializeField] AIUnit unit;
        [Space(10)]
        [SerializeField] float health = 0f;


        bool isDead = false;

        private void Start()
        {
            unit = GetComponent<AIUnit>();
            health = unit.unitConfig.health;
        }

        public void TakeDamage(float _damageToTake)
        {
            if (isDead)
                return;

            if (health <= 0f)
            {
                isDead = true;
                Die();
            }

            health -= _damageToTake;
        }

        void Die()
        {
            unit.unitStateMachine.ChangeState(UnitStateId.Die);
            print($"{this.gameObject.name} activaed ragdoll");
        }

        public void DestroyOnDie()
        {
            Destroy(gameObject, 5f);
        }
    }

}
