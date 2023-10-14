using BOC.SO;
using System.Collections.Generic;
using UnityEngine;
using BOC.StateMachine;

namespace BOC.Spawner
{
    public class UnitSpawnerManager : MonoBehaviour
    {
        private static UnitSpawnerManager instance;

        [SerializeField] Transform playerUnitSpawnPoint;

        [SerializeField] int listLimit = 5;
        [SerializeField] int levelLimit = 5;
        [SerializeField] Transform HQSpawnPoint;
        [SerializeField] LevelSO[] levels;
        [SerializeField] int levelIndex = 0;
        [SerializeField] List<CharacterSO> characterSOs = new List<CharacterSO>();

        float trainingTimeRemained = 0f;
        float trainingTimeAtStart = 0f;

        GameObject currentHQ;

        CharacterSO trainingUnit;

        public static UnitSpawnerManager Instance { get => instance; set => instance = value; }

        public float TrainingTimeRemained { get => trainingTimeRemained; private set => trainingTimeRemained = value; }
        public float TrainingTimeAtStart { get => trainingTimeAtStart; private set => trainingTimeAtStart = value;}

        public List<CharacterSO> CharacterSOs { get => characterSOs; private set => characterSOs = value; }

        public CharacterSO CharacterSO { get => trainingUnit; private set => trainingUnit = value; }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            currentHQ = Instantiate(levels[levelIndex].HQPrefab, HQSpawnPoint);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddUnitToList(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddUnitToList(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddUnitToList(2);
            }

            SpawnUnit();
        }

        public void AddUnitToList(int _unitIndex)
        {
            if (characterSOs.Count <= listLimit - 1)
            {
                switch (_unitIndex)
                {
                    case 0:
                        CharacterSO infantryChar = (CharacterSO)levels[levelIndex].infantry;
                        characterSOs.Add(infantryChar);
                        print($"Added: <b>{infantryChar.unitName}</b> Traning list: <b>{characterSOs.Count}</b>");
                        break;
                    case 1:
                        CharacterSO rangerChar = (CharacterSO)levels[levelIndex].ranger;
                        characterSOs.Add(rangerChar);
                        print($"Added: <b>{rangerChar.unitName}</b> Traning list: <b>{characterSOs.Count}</b>");
                        break;
                    case 2:
                        CharacterSO tankChar = (CharacterSO)levels[levelIndex].tank;
                        characterSOs.Add(tankChar);
                        print($"Added: <b>{tankChar.unitName}</b> Traning list: <b>{characterSOs.Count}</b>");
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                Debug.LogWarning("<b>You reached maximum limit. Please wait for training</b>");
            }
        }

        public LevelSO GetUnitAtCurrentLevel()
        {
            return levels[levelIndex];
        }

        void SpawnUnit()
        {
            //If no one in training slot and list contain unit to train . Assign to training slot then remove to list
            if (trainingUnit == null && characterSOs.Count > 0)
            {
                trainingUnit = (CharacterSO)characterSOs[0];
                trainingTimeRemained = trainingUnit.trainingTime;
                trainingTimeAtStart = trainingUnit.trainingTime;
            }
            
            //If already assign tranining slot then start training count down. After tranining remove unit from training slot.
            if (trainingUnit != null && trainingTimeRemained > 0)
            {
                trainingTimeRemained -= Time.deltaTime;
                //print($"Training: <b>{trainingUnit.unitName}</b> Traning time: <b>{trainingTimeRemained:0.00}</b>");
                if (trainingTimeRemained < 0)
                {
                    GameObject go = Instantiate(trainingUnit.prefab);
                    go.transform.SetPositionAndRotation(playerUnitSpawnPoint.position, playerUnitSpawnPoint.rotation);
                    go.GetComponent<AIUnit>().unitConfig = trainingUnit;
                    trainingTimeAtStart = 0f;
                    characterSOs.RemoveAt(0);
                    trainingUnit = null;
                }
            }
        }



        public void EvolveToNextAge()
        {
            if (currentHQ != null)
            {
                Destroy(currentHQ);
                currentHQ = null;
                if (levelIndex <= levelLimit - 1)
                {
                    levelIndex++;
                    currentHQ = Instantiate(levels[levelIndex].HQPrefab, HQSpawnPoint);
                }
            }

        }
    }

}
