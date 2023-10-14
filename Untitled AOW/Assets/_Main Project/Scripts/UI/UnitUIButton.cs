using UnityEngine;
using UnityEngine.EventSystems;
namespace BOC.UI
{
    public class UnitUIButton : ButtonCustomizedBased, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] int unitIndex = 0;

        protected override void Start()
        {
            base.Start();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter();
            InGameUIManager.Instance.PopulateUnitInformation(unitIndex);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit();
            InGameUIManager.Instance.PopulateUnitInformation(-1);
        }

    }
}

