using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyCategoryButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{


    [SerializeField] private int _index;


    public static Action<int> OnLobbyCategoryButtonClick = delegate { };

    SkeletonGraphic _skeletonGraphic;

    public virtual SkeletonGraphic SkeletonGraphic
    {
        get
        {
            if (_skeletonGraphic == null)
            {
                _skeletonGraphic = transform.GetChild(0).GetComponent<SkeletonGraphic>();
            }
            return (_skeletonGraphic);
        }
    }

    


    public void OnPointerDown(PointerEventData eventData)
    {
        if (SkeletonGraphic == null) return;
        SkeletonGraphic.material.color = new Color32(240, 240, 240, 255);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SkeletonGraphic == null) return;
        SkeletonGraphic.material.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnLobbyCategoryButtonClick.Invoke(_index);
    }
}
