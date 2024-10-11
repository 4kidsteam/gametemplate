using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math.Game001
{
    public class Math001Owl : Math001SpineAnimationController
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

