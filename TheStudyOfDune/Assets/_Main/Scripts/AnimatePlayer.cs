using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    public Animator Anim;
    public MovePlayer PlayerRef;

    string CurrentAnimString;

    private void Update()
    {
        if(PlayerRef.CurrentMoveSpeed == 0)
        {
            if (PlayerRef.CurrentTurnSpeed == 0) SetAnimString("Idle");
            else SetAnimString("Idle Turn");
        }
        else
            SetAnimString("Walk");
    }
    void SetAnimString(string New)
    {
        if(New != CurrentAnimString)
            AnimationDatabase.AnimDatabase.SetAnimationState(Anim, New, CurrentAnimString);
    }
}
