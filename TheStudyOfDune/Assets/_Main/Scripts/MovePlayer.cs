using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public PlayerRefs Refs;
    public Footsteps FootstepsRef;
    public Rigidbody Rigid;
    public float MoveSpeed;
    public float TurnSpeed;


    [Header("Fake Root Motion?")]
    public Transform Pelvis;
    public float HeightLimit;

    [Header("Healthbar")]
    public float FillSpeed;
    public float DrainSpeed;
    public float HealthbarSetPoint = 0.05f;


    Transform _T;
    Transform PositionHelper;
    [HideInInspector] public float CurrentMoveSpeed;
    [HideInInspector] public float CurrentTurnSpeed;
    bool RecentlyMoving;


    public bool IsFilling;
    public bool IsDraining;

    private void Awake()
    {
        _T = transform;
        PositionHelper = new GameObject().transform;
        PositionHelper.SetParent(_T);
    }

    private void Update()
    {
        if(IsFilling)
        {
           Transform T = Refs.HealthBarScaler;
            Vector3 Scale = T.localScale;
            Scale.y += FillSpeed * Time.deltaTime;
            if (Scale.y > HealthbarSetPoint)
            {
                Scale.y = HealthbarSetPoint;
                IsFilling = false;
            }
            T.localScale = Scale;
        }
        if (IsDraining)
        {
            Transform T = Refs.HealthBarScaler;
            Vector3 Scale = T.localScale;
            Scale.y -= DrainSpeed * Time.deltaTime;
            if (Scale.y <= 0)
            {
                //GameOver
            }
            T.localScale = Scale;
        }
    }

    private void FixedUpdate()
    {
        float Hor = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");

        CurrentTurnSpeed = Hor * TurnSpeed;
        Vector3 Rot = Rigid.rotation.eulerAngles;
        Rot.y += CurrentTurnSpeed;
        Rigid.rotation = Quaternion.Euler(Rot);

        CurrentMoveSpeed = Vert * MoveSpeed;
        PositionHelper.localPosition = new Vector3(0, 0, CurrentMoveSpeed);

        //Fake Root Motion
        if (Mathf.Abs(Pelvis.localPosition.y) < HeightLimit)
        {
            Rigid.position = PositionHelper.position;
            if (!RecentlyMoving)
            {
                RecentlyMoving = true;
                FootstepsRef.QueueFootStep();
            }
        }
        else
        {
            if (RecentlyMoving)
            {
                RecentlyMoving = false;
            }
        }
    }

    public void SetNextHeathAmount(bool IsAdd, float Value)
    {
        IsFilling = IsAdd;
        HealthbarSetPoint += Value;
        if (HealthbarSetPoint > 1) HealthbarSetPoint = 1;
        if(HealthbarSetPoint < 0) { HealthbarSetPoint = 0; }
    }
}
