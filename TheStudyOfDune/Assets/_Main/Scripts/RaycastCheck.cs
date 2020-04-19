using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    Transform _T;
    Transform PositionHelper;

    public Transform MovingZOject;
    public float MaxDistance;
    public LayerMask Mask;
    public float LerpSpeed;


    private void Awake()
    {
        _T = transform;
        PositionHelper = new GameObject().transform;
        PositionHelper.SetParent(_T);
        PositionHelper.localPosition = new Vector3(0, 0, MaxDistance);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_T.position, _T.TransformDirection(Vector3.forward), out hit, MaxDistance, Mask))
        {
            MovingZOject.position = Vector3.Lerp(MovingZOject.position, hit.point, LerpSpeed * Time.deltaTime);
        }
        else
            MovingZOject.position = Vector3.Lerp(MovingZOject.position, PositionHelper.position, LerpSpeed * Time.deltaTime);
    }
}
