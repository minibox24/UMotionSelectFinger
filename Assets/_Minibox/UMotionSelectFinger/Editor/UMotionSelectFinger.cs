/*
 * UMotionSelectFinger v1.1
 * discord @minibox._.
 */

using System.Collections.Generic;
using UMotionEditor.API;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class UMotionSelectFinger : EditorWindow
{
    static UMotionSelectFinger()
    {
        PoseEditor.AddButton(PoseEditor.FoldoutCategory.Selection, "Select Left Finger", "왼손 손가락 모두 선택", SelectLeftFinger);
        PoseEditor.AddButton(PoseEditor.FoldoutCategory.Selection, "Select Right Finger", "오른손 손가락 모두 선택", SelectRightFinger);
    }

    static void SelectLeftFinger()
    {
        Animator ani = GetAnimator();

        // 24~38
        for (int i = 24; i < 39; i++)
        {
            PoseEditor.SetTransformIsSelected(ani.GetBoneTransform((HumanBodyBones)i), true);
        }
    }

    static void SelectRightFinger()
    {
        Animator ani = GetAnimator();

        // 39~53
        for (int i = 39; i < 54; i++)
        {
            PoseEditor.SetTransformIsSelected(ani.GetBoneTransform((HumanBodyBones)i), true);
        }
    }

    static Animator GetAnimator()
    {
        List<Transform> transforms = new List<Transform>();
        PoseEditor.GetAllTransforms(transforms);

        foreach (Transform transform in transforms)
        {
            PoseEditor.SetTransformIsSelected(transform, false);
        }

        Animator animator = GetAnimator(transforms[0]);

        return animator == null ? throw new System.Exception("No Animator Found") : animator;
    }

    static Animator GetAnimator(Transform transform)
    {
        bool ok = transform.TryGetComponent(out Animator animator);

        if (ok && animator.avatar.isHuman)
        {
            return animator;
        }

        else if (transform.parent == null)
        {
            return null;
        }
        else
        {
            return GetAnimator(transform.parent);
        }
    }
}
