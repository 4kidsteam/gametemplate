using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SliBox.SpineAnim
{
    public class SpineAnimationController : MonoBehaviour
    {
        #region Inspector Editor

        public int AnimIndexGUIPopup(string label, int index)
        {
            int returnIndex = 0;
#if UNITY_EDITOR
            UnityEditor.EditorGUILayout.Space();
            returnIndex = UnityEditor.EditorGUILayout.Popup(label + " " + GetDuration(index) + "S", index, AnimationNames);
#endif
            return returnIndex;
        }

        public virtual void EnableOnInspector()
        {
            PopulateAnimationNames();
        }

        public virtual void UpdateOnInspector()
        {

        }

        #endregion

        #region Get Animation State
        SkeletonAnimation _skeletonAnimation;

        public virtual SkeletonAnimation SkeletonAnimation
        {
            get
            {
                if (_skeletonAnimation == null)
                {
                    _skeletonAnimation = GetComponent<SkeletonAnimation>();
                }
                return _skeletonAnimation;
            }
        }

        SkeletonGraphic _skeletonGraphic;

        public virtual SkeletonGraphic SkeletonGraphic
        {
            get
            {
                if (_skeletonGraphic == null)
                {
                    _skeletonGraphic = GetComponent<SkeletonGraphic>();
                }
                return (_skeletonGraphic);
            }
        }

        SkeletonDataAsset SkeletonDataAsset
        {
            get
            {
                if (_skeletonAnimation != null) return _skeletonAnimation.SkeletonDataAsset;
                else if (_skeletonGraphic != null) return _skeletonGraphic.SkeletonDataAsset;
                else if (SkeletonAnimation != null) return SkeletonAnimation.skeletonDataAsset;
                else if (SkeletonGraphic != null) return SkeletonGraphic.skeletonDataAsset;
                else return null;
            }
        }

        Spine.AnimationState AnimationState
        {
            get
            {
                if (SkeletonAnimation != null)
                {
                    return SkeletonAnimation.AnimationState;
                }
                else if (SkeletonGraphic != null)
                {
                    return SkeletonGraphic.AnimationState;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Anim Names

        private string[] _animationNames;

        public string[] AnimationNames
        {
            get
            {
                if (_animationNames == null || _animationNames.Length == 0)
                {
                    PopulateAnimationNames();
                }
                return _animationNames;
            }
        }

        private void PopulateAnimationNames()
        {
            if (SkeletonDataAsset == null)
            {
                _animationNames = new string[] { "None" };
            }
            else
            {
                _animationNames = GetAnimationNamesFromSkeletonData();
            }
        }

        private string[] GetAnimationNamesFromSkeletonData()
        {
            Spine.SkeletonData skeletonData = SkeletonDataAsset.GetSkeletonData(false);
            return skeletonData.Animations.Items.Select(a => a.Name).ToArray();
        }
        #endregion

        public float GetDuration(int animationIndex)
        {
            return SkeletonDataAsset.GetSkeletonData(true).Animations.Items[animationIndex].Duration;
        }

        public float GetDuration(string animationName)
        {
            return SkeletonDataAsset.GetSkeletonData(true).FindAnimation(animationName).Duration;
        }

        public float PlayAnim(int animationIndex, bool loop = false)
        {
            return AnimationState.SetAnimation(0, SkeletonDataAsset.GetSkeletonData(true).Animations.Items[animationIndex], loop).Animation.Duration;
        }
        public float PlayAnim(int trackIndex, int animationIndex, bool loop = false)
        {
            return AnimationState.SetAnimation(trackIndex, SkeletonDataAsset.GetSkeletonData(true).Animations.Items[animationIndex], loop).Animation.Duration;
        }

        public float PlayAnim(string animName, bool loop = false)
        {
            return AnimationState.SetAnimation(0, animName, loop).Animation.Duration;
        }
        public float PlayAnim(int trackIndex, string animName, bool loop = false)
        {
            return AnimationState.SetAnimation(trackIndex, animName, loop).Animation.Duration;
        }


        public void AddAnim(int animationIndex, float delay, bool loop = false)
        {
            AnimationState.AddAnimation(0, AnimationNames[animationIndex], loop, delay);
        }

        public void AddAnim(int trackIndex, int animationIndex, float delay, bool loop = false)
        {
            AnimationState.AddAnimation(trackIndex, AnimationNames[animationIndex], loop, delay);
        }

        public void AddAnim(string animName, float delay, bool loop = false)
        {
            AnimationState.AddAnimation(0, animName, loop, delay);
        }

        public void AddAnim(int trackIndex, string animName, float delay, bool loop = false)
        {
            AnimationState.AddAnimation(trackIndex, animName, loop, delay);
        }

        public void ResetSkeleton()
        {
            SkeletonDataAsset.Clear();
        }



    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SpineAnimationController), true)]
    public class SpineAnimationControllerInspector : UnityEditor.Editor
    {
        SpineAnimationController targetClass;

        public void OnEnable()
        {
            targetClass = (SpineAnimationController)target;
            targetClass.EnableOnInspector();
        }

        private void OnDisable()
        {
            try
            {
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(targetClass.gameObject.scene);
            }
            catch { }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            targetClass.UpdateOnInspector();
        }

    }
#endif

}

