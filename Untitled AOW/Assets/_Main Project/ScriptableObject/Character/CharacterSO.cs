using UnityEngine;

namespace BOC.SO
{
    [CreateAssetMenu(menuName =("CharacterSO/Character"))]
    public class CharacterSO : ScriptableObject
    {
        public enum UnitType { INFANTRY, RANGER, TANK};
        public UnitType unitType = UnitType.INFANTRY;
        public string unitName;
        public string description;
        public Sprite avatar;
        public float health = 5f;
        public float moveSpeed = 5f;
        public float attackRange = 2f;
        public float attackRangeLimit = 0.5f;
        public float attackPoint = 2f;
        public float attackRate = 2f;
        public float trainingTime = 1f;
        public int unitCost = 2;
        public GameObject prefab;
    }

}
