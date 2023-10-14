using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BOC.UI
{
    public class ButtonCustomizedBased : MonoBehaviour
    {
        public enum OnClickEffect { ICON, TEXT, BACKGROUND };
        public enum ButtonState { ENTER, EXIT};

        [SerializeField] bool interactable = true;

        [Header("Cursor UI")]
        [SerializeField] Texture2D defaultCursorTexture;
        [SerializeField] Texture2D onHoverCursorTexture;
        [SerializeField] Texture2D onClickCursorTexture;

        [Header("Button Icon UI")]
        [SerializeField] Image icon;
        [SerializeField] Color activeIconColor = Color.gray;
        [SerializeField] Color onClickIconColor = Color.white;
        [Range(0f, 1f)]
        [SerializeField] float onHoverIconDuration = 0.2f;
        [SerializeField] Ease onHoverIconEaseIn = Ease.Unset;
        [SerializeField] Ease onHoverIconEaseOut = Ease.Unset;
        [Range(0f, 1f)]
        [Tooltip("Flasing effect duration when performed click")]
        [SerializeField] float onClickIconDuration = 0.2f;
        Color defaultIconColor;

        [Header("Button Text UI")]
        [Space(10)]
        [SerializeField] protected TextMeshProUGUI text;
        [SerializeField] Color activeTextColor = Color.gray;
        [SerializeField] Color onClickTextColor = Color.white;
        [Range(0f, 1f)]
        [SerializeField] float onHoverTextDuration = 0.2f;
        [SerializeField] Ease onHoverTextEaseIn = Ease.Unset;
        [SerializeField] Ease onHoverTextEaseOut = Ease.Unset;
        [Range(0f, 1f)]
        [Tooltip("Flasing effect duration when performed click")]
        [SerializeField] float onClickTextDuration = 0.2f;
        Color defaultTextColor;

        protected string defaultString;

        [Header("Button Background UI")]
        [Space(10)]
        [SerializeField] Image background;
        [SerializeField] Color activeBackgroundColor = Color.white;
        [SerializeField] Color onClickBackgroundColor = Color.white;
        [Range(0f, 1f)]
        [SerializeField] float onHoverBackgroundDuration = 0.2f;
        [SerializeField] Ease onHoverBackgroundEaseIn = Ease.Unset;
        [SerializeField] Ease onHoverBackgroundEaseOut = Ease.Unset;
        [Range(0f, 1f)]
        [Tooltip("Flasing effect duration when performed click")]
        [SerializeField] float onClickButtonDuration = 0.2f;
        Color defaultBackgroundColor;

        [Space(10)]
        public OnClickEffect onClickEffect = OnClickEffect.BACKGROUND;

        ButtonState state = ButtonState.ENTER;

        [Space(10)]
        [SerializeField] UnityEvent onClick;

        protected virtual void Start()
        {
            defaultBackgroundColor = background != null ? background.color : Color.white;
            defaultIconColor = icon != null ? icon.color : Color.white;
            defaultTextColor = text != null ? text.color : Color.white;
            defaultString = text != null ? text.text : "";

            
        }

        protected virtual void OnPointerClick()
        {
            if (!interactable)
                return;

            //AudioManager AM = AudioManager.Instance;
            //if (AM != null)
            //{
            //    AM.PlaySound("ButtonClick");
            //}

            state = ButtonState.EXIT;

            if (onClickCursorTexture != null)
                Cursor.SetCursor(onClickCursorTexture, Vector2.zero, CursorMode.ForceSoftware);

            switch (onClickEffect)
            {
                case OnClickEffect.ICON:
                    if (icon != null)
                        icon.DOColor(onClickIconColor, onClickIconDuration).OnComplete(() => TweeningAfterClick(icon, defaultIconColor, onClickIconDuration));
                    break;
                case OnClickEffect.TEXT:
                    if (text != null)
                        text.DOColor(onClickTextColor, onClickTextDuration).OnComplete(() => TweeningAfterClick(text, defaultTextColor, onClickTextDuration));
                    break;
                case OnClickEffect.BACKGROUND:
                    if (background != null)
                        background.DOColor(onClickBackgroundColor, onClickButtonDuration).OnComplete(() => TweeningAfterClick(background, defaultBackgroundColor, onClickButtonDuration));
                    break;
                default:
                    break;
            }

            if (text != null)
                text.color = defaultTextColor;

            if (icon != null)
                icon.color = defaultIconColor;

            if (background != null)
                background.color = defaultBackgroundColor;

            onClick?.Invoke();
        }

        void TweeningAfterClick(Image _image, Color _activeColor, float _onClickDuration)
        {
            if (_image != null)
                _image.DOColor(_activeColor, _onClickDuration);

            switch (state)
            {
                case ButtonState.ENTER:
                    if (onHoverCursorTexture != null)
                        Cursor.SetCursor(onHoverCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case ButtonState.EXIT:
                    if (defaultCursorTexture != null)
                        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                default:
                    if (defaultCursorTexture != null)
                        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware); 
                    break;
            }
            
        }
        void TweeningAfterClick(TextMeshProUGUI _text, Color _activeColor, float _onClickDuration)
        {
            if (_text != null)
                _text.DOColor(_activeColor, _onClickDuration);

            switch (state)
            {
                case ButtonState.ENTER:
                    if (onHoverCursorTexture != null)
                        Cursor.SetCursor(onHoverCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case ButtonState.EXIT:
                    if (defaultCursorTexture != null)
                        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                default:
                    if (defaultCursorTexture != null)
                        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
                    break;
            }
        }

        protected virtual void OnPointerEnter()
        {
            if (!interactable)
                return;

            //CursorManager CM = CursorManager.Instance;
            //if (CM != null)
            //{
            //    CM.SetPointerCursor();
            //}
            state = ButtonState.ENTER;

            if (icon != null)
                icon.DOColor(activeIconColor, onHoverIconDuration).SetEase(onHoverIconEaseIn);

            if (text != null)
                text.DOColor(activeTextColor, onHoverTextDuration).SetEase(onHoverTextEaseIn);

            if (background != null)
                background.DOColor(activeBackgroundColor, onHoverBackgroundDuration).SetEase(onHoverBackgroundEaseIn);

            if (onHoverCursorTexture != null)
                Cursor.SetCursor(onHoverCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            
        }

        protected virtual void OnPointerExit()
        {
            if (!interactable)
                return;

            //CursorManager CM = CursorManager.Instance;
            //if (CM != null)
            //{
            //    CM.SetDefaultCursor();
            //}

            state = ButtonState.EXIT;

            if (icon != null)
                icon.DOColor(defaultIconColor, onHoverIconDuration).SetEase(onHoverIconEaseOut); ;

            if (text != null)
                text.DOColor(defaultTextColor, onHoverTextDuration).SetEase(onHoverTextEaseOut);

            if (background != null)
                background.DOColor(defaultBackgroundColor, onHoverBackgroundDuration).SetEase(onHoverBackgroundEaseOut);

            if (defaultCursorTexture != null)
                Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }

    }

}
