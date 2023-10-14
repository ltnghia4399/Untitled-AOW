using UnityEngine;
using UnityEngine.EventSystems;

namespace BOC.UI
{
    public class ButtonCustom : ButtonCustomizedBased, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
       
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
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit();
        }

    }
}

