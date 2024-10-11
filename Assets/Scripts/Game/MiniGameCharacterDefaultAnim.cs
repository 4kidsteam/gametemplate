using SliBox.SpineAnim;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCharacterDefaultAnim : SpineAnimationController
{
    [HideInInspector] public int IdleAnim, TalkAnim, HappyAnim;


    public virtual void Awake()
    {
        base.PlayAnim(IdleAnim, true);
    }

    public override void UpdateOnInspector()
    {
        this.IdleAnim = base.AnimIndexGUIPopup("Idle", IdleAnim);
        this.TalkAnim = base.AnimIndexGUIPopup("Talk", TalkAnim);
        this.HappyAnim = base.AnimIndexGUIPopup("Happy", HappyAnim);
    }

    public virtual void Talk(float duration)
    {
        base.PlayAnim(TalkAnim, true);
        base.AddAnim(IdleAnim, duration, true);
    }

    public virtual void Happy(float duration = 3)
    {
        base.PlayAnim(HappyAnim, true);
        base.AddAnim(IdleAnim, duration, true);
    }
}
