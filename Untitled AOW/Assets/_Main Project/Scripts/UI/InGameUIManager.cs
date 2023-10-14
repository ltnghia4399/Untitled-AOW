using BOC.Spawner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BOC.UI
{
    public class InGameUIManager : MonoBehaviour
    {
        private static InGameUIManager instance;

        [SerializeField] TextMeshProUGUI unitInformation;
        [SerializeField] Image trainingProgress;
        [SerializeField] TextMeshProUGUI[] unitTrainingListUI;

        public static InGameUIManager Instance { get => instance; set => instance = value; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PopulateUnitInformation(int _index)
        {
            var level = UnitSpawnerManager.Instance.GetUnitAtCurrentLevel();

            switch (_index)
            {
                case 0:
                    string infantryName = level.infantry.description;
                    string infantryCost = level.infantry.unitCost.ToString();
                    unitInformation.SetText($"{infantryName}\n$ {infantryCost}");
                    break;
                case 1:
                    string rangerName = level.ranger.description;
                    string rangerCost = level.ranger.unitCost.ToString();
                    unitInformation.SetText($"{rangerName}\n$ {rangerCost}");
                    break;
                case 2:
                    string tankName = level.tank.description;
                    string tankCost = level.tank.unitCost.ToString();
                    unitInformation.SetText($"{tankName}\n$ {tankCost}");
                    break;
                default:
                    unitInformation.SetText($"..\n$ 00");
                    break;
            }
        }

        float fillAmount = 0f;
        void HandleProgressFillAmount()
        {
            fillAmount = 1 - UnitSpawnerManager.Instance.TrainingTimeRemained / UnitSpawnerManager.Instance.TrainingTimeAtStart;
            trainingProgress.fillAmount = fillAmount;
            if (UnitSpawnerManager.Instance.TrainingTimeAtStart == 0f)
            {
                trainingProgress.fillAmount = 0f;
            }
        }

        void HandleUnitTrainingList()
        {
            for (int i = 0; i < UnitSpawnerManager.Instance.CharacterSOs.Count; i++)
            {
                unitTrainingListUI[i].SetText($"{UnitSpawnerManager.Instance.CharacterSOs[i].unitName}");
            }

            for (int i = UnitSpawnerManager.Instance.CharacterSOs.Count; i < unitTrainingListUI.Length; i++)
            {
                unitTrainingListUI[i].SetText("Slot");
            }
        }

        private void Update()
        {
            HandleProgressFillAmount();
            HandleUnitTrainingList();
        }

    }

}
