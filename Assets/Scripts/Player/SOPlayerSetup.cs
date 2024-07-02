using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public Animator player;

    public SOString playerName;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float runSpeed;
    public float forceJump = 2;
    

    [Header("Animation")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration;
    public Ease ease = Ease.OutBack;

    [Header("Animation Animator")]
    public string boolRun = "Run";
    public string triggerKill = "Death";
    public float durationScale = .1f;
}
