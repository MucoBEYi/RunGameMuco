using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("player"))
        { 

            GameManager.Instance.GameOver();
            print("�arpma i�lemi ba�ar�l�");


            // print("alt�n " + GameManager.Instance.coin);
        }
    }

    

}
