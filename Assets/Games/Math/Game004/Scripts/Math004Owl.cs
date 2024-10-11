using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math.Game004
{
    public class Math004Owl : Math004SpineAnimationController
    {
        [HideInInspector] public int IdleAnim, TalkAnim, JumpAnim;


        public virtual void Awake()
        {
            base.PlayAnim(IdleAnim, true);
        }

        public override void UpdateOnInspector()
        {
            IdleAnim = base.AnimIndexGUIPopup("Idle", IdleAnim);
            TalkAnim = base.AnimIndexGUIPopup("Talk", TalkAnim);
            JumpAnim = base.AnimIndexGUIPopup("Jump", JumpAnim);
        }

        public virtual void Talk(float duration)
        {
            base.PlayAnim(TalkAnim, true);
            base.AddAnim(IdleAnim, duration, true);
        }

        public virtual void Jump(float duration = 3)
        {
            base.PlayAnim(JumpAnim, true);
            base.AddAnim(IdleAnim, duration, true);
        }
    }
}

