using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image FadeOutImage;
    public float FadeTimer;


    public bool TestFadeOut;
    public bool TestFadeIn;

    bool FadeActive;
    float TrackedTime;

    bool IsFadeIn;
    GameObject ReturnMessage;


    private void Awake()
    {
        SetColorAlpha(1);
    }

    private void Start()
    {
        FadeIn(null);
    }

    public void FadeOut(GameObject SendReturn)
    {
        ReturnMessage = SendReturn;
        IsFadeIn = false;
        TrackedTime = FadeTimer;
        FadeOutImage.enabled = true;
        SetColorAlpha(0);
        FadeActive = true;
    }
    public void FadeIn(GameObject SendReturn)
    {
        ReturnMessage = SendReturn;
        IsFadeIn = true;
        TrackedTime = FadeTimer;
        FadeOutImage.enabled = true;
        SetColorAlpha(1);
        FadeActive = true;
    }

    private void Update()
    {
        if (FadeActive)
        {
            TrackedTime -= Time.deltaTime;
            if (TrackedTime < 0)
            {
                if (IsFadeIn) SetColorAlpha(0);
                else SetColorAlpha(1);
                if (ReturnMessage != null) ReturnMessage.SendMessage("FadeComplete");
                FadeActive = false;
            }
            else
            {
                float Percent = TrackedTime / FadeTimer;
                if (!IsFadeIn) Percent = 1 - Percent;
                SetColorAlpha(Percent);
            }
        }
        if(TestFadeIn)
        {
            TestFadeIn = false;
            FadeIn(null);
        }
        if (TestFadeOut)
        {
            TestFadeOut = false;
            FadeOut(null);
        }
    }

    void SetColorAlpha(float Value)
    {
        Color C = FadeOutImage.color;
        C.a = Value;
        FadeOutImage.color = C;
    }
}
