using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public MovePlayer PlayerRef;
    public Transform PositionFollow;
    public Transform RotationFollow;
    Transform _T;

    public bool UsePositionLerp;
    public float PositionLerpSpeed;
    public bool FollowAngle;
    public bool TurnIfMoving;
    public bool UseAngleLerpSpeed;
    public float AngleLerpSpeed;

    private void Awake()
    {
        _T = transform;
    }

    void Update()
    {
        float Delta = Time.deltaTime;

        if(!UsePositionLerp)
            _T.position = PositionFollow.position;
        else
            _T.position = Vector3.Lerp(_T.position, PositionFollow.position, PositionLerpSpeed * Delta);

        if(FollowAngle)
        {
            bool AllowTurn = true;
            if (TurnIfMoving && PlayerRef.CurrentMoveSpeed == 0) AllowTurn = false;

            if (AllowTurn)
            {
                if (!UseAngleLerpSpeed)
                    _T.eulerAngles = RotationFollow.eulerAngles;
                else
                    _T.eulerAngles = AngleLerp(_T.eulerAngles, RotationFollow.eulerAngles, AngleLerpSpeed * Delta);
            }
        }
    }

    Vector3 AngleLerp(Vector3 A, Vector3 B, float Value)
    {
        Vector3 Return;
        Return.x = Mathf.LerpAngle(A.x, B.x, Value);
        Return.y = Mathf.LerpAngle(A.y, B.y, Value);
        Return.z = Mathf.LerpAngle(A.z, B.z, Value);
        return Return;
    }
}
