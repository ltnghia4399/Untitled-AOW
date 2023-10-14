using BOC.StateMachine;
using UnityEngine;

namespace BOC.UnitProperties
{
    public class UnitInfantry : UnitMovement
    {
        protected override void Update()
        {
            base.Update();
            HandleUnitVision();
            HandleAttackAnimation();
        }

        private  void HandleUnitVision()
        {
            ray.origin = originVisionPoint.position;
            ray.direction = originVisionPoint.forward;

            if (Physics.Raycast(ray, out RaycastHit hitInfo, unit.unitConfig.attackRange, unitMask))
            {
                AIUnit facedUnit = hitInfo.collider.GetComponent<AIUnit>();

                if (facedUnit != null)
                {
                    if (facedUnit.Type != unit.Type)
                    {
                        unit.unitStateMachine.ChangeState(UnitStateId.Attack);
                        target = facedUnit.GetComponent<UnitHealth>();
                        DrawLine(originVisionPoint.position, hitInfo.point, Color.red, 0.5f);
                    }
                    else
                    {
                        unit.unitStateMachine.ChangeState(UnitStateId.Idle);
                        target = null;
                        DrawLine(originVisionPoint.position, hitInfo.point, Color.yellow, 0.5f);
                    }
                }
            }
            else
            {
                if (rotation.y == 0f)
                    DrawLine(originVisionPoint.position, originVisionPoint.position + originVisionPoint.forward * unit.unitConfig.attackRange, Color.green, 0.1f);
                else
                    DrawLine(originVisionPoint.position, originVisionPoint.position + Vector3.back * unit.unitConfig.attackRange, Color.green, 0.1f);
                target = null;
                unit.unitStateMachine.ChangeState(UnitStateId.Walk);
            }
        }

        private void HandleAttackAnimation()
        {
            isAttackAnim = anim.GetBool(isAttackHash);

            anim.SetInteger(attackIndexHash, 0);

            if (!isAttackAnim && attackPressed)
            {
                anim.SetBool(isAttackHash, true);
            }
            if (isAttackAnim && !attackPressed)
            {
                anim.SetBool(isAttackHash, false);
            }
        }
        public void Hit()
        {
            if (target != null)
            {
                print($"Hit {target.gameObject.name} from animation event");
                target.TakeDamage(unit.unitConfig.attackPoint);
            }

            Debug.LogError("Target undenfied");
        }
    }

}
