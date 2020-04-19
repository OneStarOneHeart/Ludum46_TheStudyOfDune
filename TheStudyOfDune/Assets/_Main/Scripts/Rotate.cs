using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public enum Style
    {
        Basic,
        SpasticPingPong
    }

    public Style Type;
    public Vector3 Vec1;
    public Vector3 Vec2;
    public float Speed;
    public float MaxSpeed;

    bool Direction;
    float Timer;
    float TotalTime;

    Transform _T;

    private void Awake()
    {
        _T = transform;
    }




    // Update is called once per frame
    void Update()
    {
        switch(Type)
        {
            case Style.Basic:
                {
                    _T.localEulerAngles += (Vec1 * Speed * Time.deltaTime);
                }
                break;
            case Style.SpasticPingPong:
                {
                    Timer -= Time.deltaTime;
                    if(Timer < 0)
                    {
                        Timer = Random.Range(Speed, MaxSpeed);
                        TotalTime = Timer;
                        Direction = !Direction;
                    }
                    //So this is wrong, but actually makes the effect look jerky, which is good in this case.
                    float Percent = Mathf.Lerp(0, TotalTime, Timer);

                    if (Direction)
                    {
                        _T.localEulerAngles = AngleLerp(_T.localEulerAngles, Vec1, Percent);
                    }
                    else
                    {
                        _T.localEulerAngles = AngleLerp(_T.localEulerAngles, Vec2, Percent);
                    }
                }
                break;
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
