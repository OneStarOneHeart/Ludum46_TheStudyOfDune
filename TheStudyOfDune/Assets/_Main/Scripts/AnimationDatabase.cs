using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDatabase : MonoBehaviour
{
    [System.Serializable]
    public class AnimationState
    {
        public string AnimationName;
        [Header("State Values")]
        public int StateNumber;
        public int SubStateNumber;
    }


    public AnimationState[] States;



    public static AnimationDatabase AnimDatabase;
    private void Awake() { AnimDatabase = this; }




    public void SetAnimationState(Animator Anim, string NewAnimationName, string CurrentAnimState)
    {
        AnimationState AnimState = null;
        int Length = States.Length;
        for (int i = 0; i < Length; i++)
        {
            if (States[i].AnimationName == NewAnimationName)
            {
                AnimState = States[i];
                break;
            }
        }

        Anim.SetInteger("Main", AnimState.StateNumber);
        Anim.SetInteger("Sub", AnimState.SubStateNumber);
    }
}
