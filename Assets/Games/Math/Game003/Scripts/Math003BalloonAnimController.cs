using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math.Game003
{
    public class Math003BalloonAnimController : Math003SpineAnimationController
    {
        private void Awake()
        {
            base.PlayAnim(IdleAnim, true);
        }
        public void ChangeNumber(int number)
        {
            base.PlayAnim(1, string.Format("{0}_{1}", number, Random.Range(0, 3)));
        }
    }
}

