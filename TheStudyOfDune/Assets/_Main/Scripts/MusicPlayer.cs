using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MusicPlayer : MonoBehaviour
{

    public AudioSource MusicPlaySource;
    public AudioSource MusicPlaySourceB;

    public bool DebugTriggerNext;
    public AudioClip[] LoopRefs;

    [Header("Music Shift to Beat Sync")]
    public float MusicBPM;
    public float WholeNoteTime;
    public int MeasuresBetweenShifts;
    [Header("Crossfade")]
    public float CrossfadeTime;

    bool CrossFadeActive;
    float CrossFadeTimer;

    bool PlayNextShift;
    bool MainIsA;
    int CurrentClipIndex;
    float TimeTillNextShift; 


    public static MusicPlayer MusicPlayerGlobalRef;
    private void Awake() { MusicPlayerGlobalRef = this; }

    private void Start()
    {
        if (!Application.isPlaying) return;
        MainIsA = true;
        MusicPlaySource.clip = LoopRefs[0];
        MusicPlaySource.Play();
        CurrentClipIndex = 1;
    }
    private void OnEnable()
    {
        //Calculates WholeNote Timeframe based On BPM.  This Track is 120, so it's hard to mess up.
        float QuarterNoteTime = (60 / MusicBPM);
        WholeNoteTime = QuarterNoteTime * 4;
    }

    private void Update()
    {
        if (!Application.isPlaying) return;

        float DeltaTime = Time.deltaTime;


        //CrossFade
        if (CrossFadeActive)
        {
            CrossFadeTimer -= DeltaTime;
            if (CrossFadeTimer < 0)
            {
                CrossFadeActive = false;
                if (!MainIsA)
                {
                    MusicPlaySource.Stop();
                    MusicPlaySourceB.volume = 1;
                }
                else
                {
                    MusicPlaySourceB.Stop();
                    MusicPlaySource.volume = 1;
                }
            }
            else
            {
                float CurrentLerpPoint = Mathf.Lerp(0, CrossfadeTime, CrossFadeTimer);
                if (!MainIsA)
                {
                    MusicPlaySource.volume = CurrentLerpPoint;
                    MusicPlaySourceB.volume = 1 - CurrentLerpPoint;
                }
                else
                {
                    MusicPlaySourceB.volume = CurrentLerpPoint;
                    MusicPlaySource.volume = 1 - CurrentLerpPoint;
                }
            }
        }

        //WholeNote Metronome;
        TimeTillNextShift -= DeltaTime;
        if(TimeTillNextShift < 0)
        {
            TimeTillNextShift = WholeNoteTime * MeasuresBetweenShifts;

            if(PlayNextShift)
            {
                PlayNextShift = false;
                CrossFadeActive = true;
                CrossFadeTimer = CrossfadeTime;

                if (!MainIsA)
                {
                    MusicPlaySourceB.volume = 0;
                    MusicPlaySourceB.Play();
                }
                else
                {
                    MusicPlaySource.volume = 0;
                    MusicPlaySource.Play();
                }
            }
        }

        //Debug Testing.
        if (DebugTriggerNext) { DebugTriggerNext = false; QueueNextClip(); }
    }

    public void QueueNextClip()
    {
        PlayNextShift = true;

        if (MainIsA)
        {
            MainIsA = false;
            MusicPlaySourceB.clip = LoopRefs[CurrentClipIndex];
            CurrentClipIndex++;
        }
        else
        {
            MainIsA = true;
            MusicPlaySource.clip = LoopRefs[CurrentClipIndex];
            CurrentClipIndex++;
        }
    }

}
