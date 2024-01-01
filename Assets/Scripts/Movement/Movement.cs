using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] MovementSettings mSettings;

    Rigidbody rb;

    private float beforeSwipe, posUpdateSwipe, swipe;

    public float moveZ;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();


        //ko�ma animasyonunu �a��r�r
        GameManager.Instance.RunnerAnimation();
    }
    private void Update()
    {
        //anl�k t�klamalar� g�rmesi i�in updatede ba�lat�yorum
        SwipeMechanics();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    //swipe i�lemleri
    void SwipeMechanics()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //t�klama evresi
            if (touch.phase == TouchPhase.Began)
            {
                //touch pozisyonunu de�i�kene aktar�r
                beforeSwipe = touch.position.x;
            }

            //hareket evresi
            else if (touch.phase == TouchPhase.Moved)
            {
                //pozisyon g�ncellemesi
                posUpdateSwipe = (touch.position.x - beforeSwipe) * Time.fixedDeltaTime;

                //kayd�rma s�n�r�
                swipe = Mathf.Clamp(posUpdateSwipe, -mSettings.swipeSpeedLimit, mSettings.swipeSpeedLimit);

                //touch pozisyonunu de�i�kene bu else if �al��t��� s�rece aktar�r.
                beforeSwipe = touch.position.x;
            }

            //hareket sonras� hareket ettirmeme evresi
            else if (touch.phase == TouchPhase.Stationary)
            {
                //p�r�zs�z(smooth) kayd�rma
                StartCoroutine(SmoothSwipe());

            }
            //t�klamay� b�rakma evresi
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                swipe = 0;
            }
        }
    }

    void OnMove()
    {
        //x pozisyonunda limit
        if ((rb.position.x + swipe) > 5.25f || (rb.position.x + swipe) < -5.25f)
        {
            swipe = 0;
            //ek �nlem
            StartCoroutine(XLimit());

        }

        //y�ntem 1(g�ncellenmesi gerekiyor bu haliyle stabil �al��aca��n� sanm�yorum)
        //rb.MovePosition(new Vector3(rb.position.x + posUpdateSwipe * mSettings.swipeSpeed, 0, rb.position.z + mSettings.speed * Time.fixedDeltaTime));

        if (GameManager.Instance.GameOverBool == false)
        {
            //y�ntem 2
            //z pozisyonu tan�m�
            moveZ = (GameManager.Instance.speedBuff + mSettings.speed) * Time.fixedDeltaTime;
            //hareket komutu
            rb.velocity = new Vector3(swipe * mSettings.swipeSpeed, 0, moveZ);
        }
        else
        {
            rb.velocity = Vector3.zero;

            //buna san�r�m gerek yok
            rb.AddForce(Vector3.zero);

        }
    }

    //x limitini a�arsa ek �nlem
    IEnumerator XLimit()
    {
        //if de ayn� �al���yor?(asl�nda cevab� biliyorum fakat do�ru mu emin olmal�y�m)
        while (rb.position.x > 5.25f || rb.position.x < -5.25f)
        {
            //x pozisyonu 0 yapmaya zorlar
            rb.position = new Vector3(Mathf.Lerp(rb.position.x, 0, Time.fixedDeltaTime * mSettings.xLimitFriction), 0, rb.position.z);


            yield return new WaitForEndOfFrame();
        }
    }

    //p�r�zs�z kayd�rma
    IEnumerator SmoothSwipe()
    {
        swipe = Mathf.Lerp(swipe, 0, Time.fixedDeltaTime * mSettings.smoothSwipe);

        yield return new WaitForEndOfFrame();
    }
}


// animator.SetBool("IsRunner", false);
// animator.SetBool("IsRunner", true);