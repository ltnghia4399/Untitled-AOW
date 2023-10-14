using BOC.StateMachine;
using UnityEngine;

namespace BOC.UnitProperties
{
    [RequireComponent(typeof(Rigidbody))]
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] bool isDebug;

        [Space(10)]
        [SerializeField] protected AIUnit unit;
        [SerializeField] Rigidbody rb;
        [SerializeField] protected Animator anim;

        [SerializeField] protected Transform originVisionPoint;
        protected Ray ray;
        protected RaycastHit[] hitInfos;
        protected int numberOfObjects;
        [SerializeField] protected LayerMask unitMask;

        public float movementSpeed = 0f;

        protected Quaternion rotation;

        int isWalkingHash;
        bool isWalkingAnim;
        bool walkingPressed = true;

        protected int isAttackHash;
        protected int attackIndexHash;
        protected bool isAttackAnim;
        protected bool attackPressed = false;


        protected UnitHealth target;

        public bool WalkingPressed { get => walkingPressed; set => walkingPressed = value; }
        public bool AttackPressed { get => attackPressed; set => attackPressed = value; }
        //public Quaternion OriginVisionPoint { get => originVisionPoint.rotation; set => originVisionPoint.rotation = value; }

        private void Start()
        {
            unit = GetComponent<AIUnit>();

            anim = GetComponent<Animator>();
            isWalkingHash = anim != null ? Animator.StringToHash("isWalk") : 0;
            isAttackHash = anim != null ? Animator.StringToHash("isAttack") : 0;
            attackIndexHash = anim != null ? Animator.StringToHash("attackIndex") : 0;

            rb = GetComponent<Rigidbody>();
            rotation = transform.rotation;
            hitInfos = new RaycastHit[2];
            
        }
        
        protected virtual void Update()
        {
            HandleWalkAnimation();
            //HandleUnitVision();
            //HandleAttackAnimation();
        }
        void HandleWalkAnimation()
        {
            isWalkingAnim = anim.GetBool(isWalkingHash);

            if (!isWalkingAnim && walkingPressed)
            {
                anim.SetBool(isWalkingHash, true);
            }
            if (isWalkingAnim && !walkingPressed)
            {
                anim.SetBool(isWalkingHash, false);
            }
        }
        
        //protected bool isAttackCloseRange = false;
        //private void HandleUnitVision()
        //{
        //    ray.origin = originVisionPoint.position;
        //    ray.direction = originVisionPoint.forward;

        //    if (unit.unitConfig.unitType == SO.CharacterSO.UnitType.RANGER)
        //    {
        //        numberOfObjects = Physics.RaycastNonAlloc(ray, hitInfos, unit.unitConfig.attackRange, unitMask);

        //        if (numberOfObjects >= 2)
        //        {
        //            for (int i = 0; i < numberOfObjects; i++)
        //            {
        //                RaycastHit hitInfo = hitInfos[i];

        //                AIUnit facedUnit = hitInfo.collider.GetComponent<AIUnit>();
        //                if (facedUnit != null)
        //                {
        //                    if (/*!unit.isEnemy && facedUnit.isEnemy*/ facedUnit.Type != unit.Type)
        //                    {
        //                        unit.unitStateMachine.ChangeState(UnitStateId.Attack); //Attack mid range
        //                        isAttackCloseRange = false;
        //                        target = facedUnit.GetComponent<UnitHealth>();
        //                        DrawLine(originVisionPoint.position, hitInfo.point, Color.red, 0.1f);
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        if (Vector3.Distance(transform.position,facedUnit.transform.position) <= 1.0f)
        //                        {
        //                            unit.unitStateMachine.ChangeState(UnitStateId.Idle);
        //                            target = null;
        //                            DrawLine(originVisionPoint.position, hitInfo.point, Color.blue, 0.1f);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else if (numberOfObjects == 1)
        //        {
        //            AIUnit facedUnit = hitInfos[0].collider.GetComponent<AIUnit>();
        //            if (facedUnit != null)
        //            {
        //                if (facedUnit.Type != unit.Type && Vector3.Distance(transform.position, facedUnit.transform.position) <= unit.unitConfig.attackRangeLimit)
        //                {
        //                    unit.unitStateMachine.ChangeState(UnitStateId.Attack); // Attack close range
        //                    isAttackCloseRange = true;
        //                    target = facedUnit.GetComponent<UnitHealth>();
        //                    DrawLine(originVisionPoint.position, hitInfos[0].point, Color.red, 0.1f);
        //                }
        //                else
        //                {
        //                    unit.unitStateMachine.ChangeState(UnitStateId.Walk);
        //                    target = null;
        //                    DrawLine(originVisionPoint.position, hitInfos[0].point, Color.yellow, 0.1f);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (rotation.y == 0f)
        //                DrawLine(originVisionPoint.position, originVisionPoint.position + originVisionPoint.forward * unit.unitConfig.attackRange, Color.green, 0.1f);
        //            else
        //                DrawLine(originVisionPoint.position, originVisionPoint.position + Vector3.back * unit.unitConfig.attackRange, Color.green, 0.1f);
        //            target = null;
        //            unit.unitStateMachine.ChangeState(UnitStateId.Walk);
        //        }
        //    }
        //    else
        //    {
        //        if (Physics.Raycast(ray, out RaycastHit hitInfo, unit.unitConfig.attackRange, unitMask))
        //        {
        //            AIUnit facedUnit = hitInfo.collider.GetComponent<AIUnit>();

        //            if (facedUnit != null)
        //            {
        //                if (facedUnit.Type != unit.Type)
        //                {
        //                    unit.unitStateMachine.ChangeState(UnitStateId.Attack);
        //                    target = facedUnit.GetComponent<UnitHealth>();
        //                    DrawLine(originVisionPoint.position, hitInfo.point, Color.red, 0.5f);
        //                }
        //                else
        //                {
        //                    unit.unitStateMachine.ChangeState(UnitStateId.Idle);
        //                    target = null;
        //                    DrawLine(originVisionPoint.position, hitInfo.point, Color.yellow, 0.5f);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (rotation.y == 0f)
        //                DrawLine(originVisionPoint.position, originVisionPoint.position + originVisionPoint.forward * unit.unitConfig.attackRange, Color.green, 0.1f);
        //            else
        //                DrawLine(originVisionPoint.position, originVisionPoint.position + Vector3.back * unit.unitConfig.attackRange, Color.green, 0.1f);
        //            target = null;
        //            unit.unitStateMachine.ChangeState(UnitStateId.Walk);
        //        }
        //    }
        //}


        //private void HandleAttackAnimation()
        //{
        //    isAttackAnim = anim.GetBool(isAttackHash);

        //    switch (unit.unitConfig.unitType)
        //    {
        //        case SO.CharacterSO.UnitType.INFANTRY:
        //            anim.SetInteger(attackIndexHash, 0);
        //            break;
        //        case SO.CharacterSO.UnitType.RANGER:
        //            if (isAttackCloseRange)
        //                anim.SetInteger(attackIndexHash, 0);
        //            else
        //                anim.SetInteger(attackIndexHash, 1);
        //            break;
        //        case SO.CharacterSO.UnitType.TANK:
        //            anim.SetInteger(attackIndexHash, 2);
        //            break;
        //        default:
        //            break;
        //    }

        //    if (!isAttackAnim && attackPressed)
        //    {
        //        anim.SetBool(isAttackHash, true);
        //    }
        //    if (isAttackAnim && !attackPressed)
        //    {
        //        anim.SetBool(isAttackHash, false);
        //    }
        //}

        //public void Hit()
        //{
        //    if (target != null)
        //    {
        //        print($"Hit {target.gameObject.name} from animation event");
        //        target.TakeDamage(unit.unitConfig.attackPoint);
        //    }
        //}

        //public void Shoot()
        //{
        //    if (target != null)
        //    {
        //        print($"Shoot {target.gameObject.name} from animation event");
        //        target.TakeDamage(unit.unitConfig.attackPoint);
        //    }
        //}

        void FixedUpdate()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            if (rb != null)
            {
                if (rotation.y == 0f)
                    rb.velocity = movementSpeed * Time.deltaTime * Vector3.forward;
                else
                    rb.velocity = -movementSpeed * Time.deltaTime * Vector3.forward;
            }
        }


        protected void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            if (!isDebug)
                return;

            bool depthTest = true;
            Debug.DrawLine(start, end, color, duration, depthTest);
        }
    }

}
