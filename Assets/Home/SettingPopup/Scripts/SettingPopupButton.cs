using SliBoxEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SliBox.Popup
{
    public class SettingPopupButton : ButtonClick
    {
        [SerializeField] GameObject controlObj;
        public void ClosePopup()
        {
            Destroy(controlObj);
            //controlObj.SetActive(false);
        }
    }

    enum ButtonHandlerType
    {
        PointClick,
        PointDown,
        PointUp,
    }
    public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [HideInInspector] public Vector2 stampScale;
        [SerializeField] ButtonHandlerType buttonHandlerType;
        [SerializeField] bool buttonDownSound;
        [SerializeField] UnityEvent buttonClick;

        public virtual void Awake()
        {
            stampScale = transform.localScale;
        }
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            transform.localScale = stampScale;
            if (buttonHandlerType == ButtonHandlerType.PointClick) buttonClick.Invoke();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = stampScale * 0.95f;
            if (buttonDownSound && AudioManager.Inst != null) AudioManager.Inst.Play("button_down");
            if (buttonHandlerType == ButtonHandlerType.PointDown)
            {
                buttonClick.Invoke();
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = stampScale;
            if (buttonHandlerType == ButtonHandlerType.PointUp) buttonClick.Invoke();
        }

    }

}