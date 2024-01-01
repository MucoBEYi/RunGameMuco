using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Movement Type", menuName = "Movement Type")]
public class MovementSettings : ScriptableObject
{

    public float swipeSpeed, swipeSpeedLimit, smoothSwipe, xLimitFriction;

    public float speed, speedBuff, limitSpeed;

    //limit speed yap

}
