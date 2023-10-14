using UnityEngine;

namespace BOC.SO
{
    [CreateAssetMenu(menuName = ("LevelSO/Level"))]
    public class LevelSO : ScriptableObject
    {
        public GameObject HQPrefab;
        public CharacterSO infantry;
        public CharacterSO ranger;
        public CharacterSO tank;
    }
}
