using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpeedBuffObstacle : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            print("oyuncuya deydi");
            StartCoroutine(GameManager.Instance.MovementBuff());
        }
    }




    // print(speedBuff + " Buff süresi doldu");





    /* while(mSettings.SpeedBuff > 500)
     {
         mSettings.SpeedBuff -= 500;

         yield return new WaitForEndOfFrame();
     }*/

}

