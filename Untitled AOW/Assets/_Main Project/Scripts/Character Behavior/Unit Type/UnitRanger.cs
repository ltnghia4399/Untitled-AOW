using BOC.StateMachine;
using UnityEngine;

namespace BOC.UnitProperties
{
    public class UnitRanger : UnitMovement
    {
        bool isAttackCloseRange = false;
        protected override void Update()
        {
            base.Update();
            HandleUnitVision();
            HandleAttackAnimation();
        }

        private void HandleUnitVision()
        {
            ray.origin = originVisionPoint.position;
            ray.direction = originVisionPoint.forward;

            numberOfObjects = Physics.RaycastNonAlloc(ray, hitInfos, unit.unitConfig.attackRange, unitMask);

            if (numberOfObjects >= 2)
            {
                for (int i = 0; i < numberOfObjects; i++)
                {
                    RaycastHit hitInfo = hitInfos[i];

                    AIUnit facedUnit = hitInfo.collider.GetComponent<AIUnit>();
                    if (facedUnit != null)
                    {
                        if (/*!unit.isEnemy && facedUnit.isEnemy*/ facedUnit.Type != unit.Type)
                        {
                            unit.unitStateMachine.ChangeState(UnitStateId.Attack); //Attack mid range
                            isAttackCloseRange = false;
                            target = facedUnit.GetComponent<UnitHealth>();
                            DrawLine(originVisionPoint.position, hitInfo.point, Color.red, 0.1f);
                            break;
                        }
                        else
                        {
                            if (Vector3.Distance(transform.position, facedUnit.transform.position) <= 1.0f)
                            {
                                unit.unitStateMachine.ChangeState(UnitStateId.Idle);
                                target = null;
                                DrawLine(originVisionPoint.position, hitInfo.point, Color.blue, 0.1f);
                            }
                        }
                    }
                }
            }
            else if (numberOfObjects == 1)
            {
                AIUnit facedUnit = hitInfos[0].collider.GetComponent<AIUnit>();
                if (facedUnit != null)
                {
                    if (facedUnit.Type != unit.Type && Vector3.Distance(transform.position, facedUnit.transform.position) <= unit.unitConfig.attackRangeLimit)
                    {
                        unit.unitStateMachine.ChangeState(UnitStateId.Attack); // Attack close range
                        isAttackCloseRange = true;
                        target = facedUnit.GetComponent<UnitHealth>();
                        DrawLine(originVisionPoint.position, hitInfos[0].point, Color.red, 0.1f);
                    }
                    else
                    {
                        unit.unitStateMachine.ChangeState(UnitStateId.Walk);
                        target = null;
                        DrawLine(originVisionPoint.position, hitInfos[0].point, Color.yellow, 0.1f);
                    }
                }
            }
        }

        private void HandleAttackAnimation()
        {
            isAttackAnim = anim.GetBool(isAttackHash);

            if (isAttackCloseRange)
                anim.SetInteger(attackIndexHash, 0);
            else
                anim.SetInteger(attackIndexHash, 1);

            if (!isAttackAnim && attackPressed)
            {
                anim.SetBool(isAttackHash, true);
            }
            if (isAttackAnim && !attackPressed)
            {
                anim.SetBool(isAttackHash, false);
            }
        }

        public void Shoot()
        {
            if (target != null)
            {
                print($"Shoot {target.gameObject.name} from animation event");
                target.TakeDamage(unit.unitConfig.attackPoint);
            }

            Debug.LogError("Target undenfied");
        }
    }

}
