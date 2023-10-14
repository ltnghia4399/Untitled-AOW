using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BOC.SO;
using BOC.StateMachine;
using BOC.UnitProperties;
namespace BOC.Spawner
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] int currentLevel = 0;
        [SerializeField] LevelSO[] levels;

        [SerializeField] Transform enemySpawnPoint;

        private void Start()
        {
            InvokeRepeating("SpawnRandomEnemy", 2, 5);
        }


        void SpawnRandomEnemy()
        {
            int randomIndex = Random.Range(0, 3);

            switch (randomIndex)
            {
                case 0:
                    CharacterSO e_Infantry = Instantiate(levels[currentLevel].infantry);
                    GameObject go = Instantiate(e_Infantry.prefab);
                    go.transform.SetPositionAndRotation(enemySpawnPoint.position, enemySpawnPoint.localRotation);
                    var unit = go.GetComponent<AIUnit>();
                    unit.unitConfig = e_Infantry;
                    unit.isEnemy = true;
                    unit.Type = AIUnit.AIType.PLAYER2;
                    break;
                case 1:
                    CharacterSO e_Ranger = Instantiate(levels[currentLevel].ranger);
                    GameObject go1 = Instantiate(e_Ranger.prefab);
                    go1.transform.SetPositionAndRotation(enemySpawnPoint.position, enemySpawnPoint.localRotation);
                    var unit1 = go1.GetComponent<AIUnit>();
                    unit1.unitConfig = e_Ranger;
                    unit1.isEnemy = true;
                    unit1.Type = AIUnit.AIType.PLAYER2;
                    break;
                case 2:
                    CharacterSO e_Tank = Instantiate(levels[currentLevel].tank);
                    GameObject go2 = Instantiate(e_Tank.prefab);
                    go2.transform.SetPositionAndRotation(enemySpawnPoint.position, enemySpawnPoint.localRotation);
                    var unit2 = go2.GetComponent<AIUnit>();
                    unit2.unitConfig = e_Tank;
                    unit2.isEnemy = true;
                    unit2.Type = AIUnit.AIType.PLAYER2;
                    break;
                default:
                    break;
            }
        }
    }

}
