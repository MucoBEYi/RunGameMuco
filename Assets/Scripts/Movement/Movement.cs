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


        //koþma animasyonunu çaðýrýr
        GameManager.Instance.RunnerAnimation();
    }
    private void Update()
    {
        //anlýk týklamalarý görmesi için updatede baþlatýyorum
        SwipeMechanics();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    //swipe iþlemleri
    void SwipeMechanics()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //týklama evresi
            if (touch.phase == TouchPhase.Began)
            {
                //touch pozisyonunu deðiþkene aktarýr
                beforeSwipe = touch.position.x;
            }

            //hareket evresi
            else if (touch.phase == TouchPhase.Moved)
            {
                //pozisyon güncellemesi
                posUpdateSwipe = (touch.position.x - beforeSwipe) * Time.fixedDeltaTime;

                //kaydýrma sýnýrý
                swipe = Mathf.Clamp(posUpdateSwipe, -mSettings.swipeSpeedLimit, mSettings.swipeSpeedLimit);

                //touch pozisyonunu deðiþkene bu else if çalýþtýðý sürece aktarýr.
                beforeSwipe = touch.position.x;
            }

            //hareket sonrasý hareket ettirmeme evresi
            else if (touch.phase == TouchPhase.Stationary)
            {
                //pürüzsüz(smooth) kaydýrma
                StartCoroutine(SmoothSwipe());

            }
            //týklamayý býrakma evresi
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
            //ek önlem
            StartCoroutine(XLimit());

        }

        //yöntem 1(güncellenmesi gerekiyor bu haliyle stabil çalýþacaðýný sanmýyorum)
        //rb.MovePosition(new Vector3(rb.position.x + posUpdateSwipe * mSettings.swipeSpeed, 0, rb.position.z + mSettings.speed * Time.fixedDeltaTime));

        if (GameManager.Instance.GameOverBool == false)
        {
            //yöntem 2
            //z pozisyonu tanýmý
            moveZ = (GameManager.Instance.speedBuff + mSettings.speed) * Time.fixedDeltaTime;
            //hareket komutu
            rb.velocity = new Vector3(swipe * mSettings.swipeSpeed, 0, moveZ);
        }
        else
        {
            rb.velocity = Vector3.zero;

            //buna sanýrým gerek yok
            rb.AddForce(Vector3.zero);

        }
    }

    //x limitini aþarsa ek önlem
    IEnumerator XLimit()
    {
        //if de ayný çalýþýyor?(aslýnda cevabý biliyorum fakat doðru mu emin olmalýyým)
        while (rb.position.x > 5.25f || rb.position.x < -5.25f)
        {
            //x pozisyonu 0 yapmaya zorlar
            rb.position = new Vector3(Mathf.Lerp(rb.position.x, 0, Time.fixedDeltaTime * mSettings.xLimitFriction), 0, rb.position.z);


            yield return new WaitForEndOfFrame();
        }
    }

    //pürüzsüz kaydýrma
    IEnumerator SmoothSwipe()
    {
        swipe = Mathf.Lerp(swipe, 0, Time.fixedDeltaTime * mSettings.smoothSwipe);

        yield return new WaitForEndOfFrame();
    }
}


// animator.SetBool("IsRunner", false);
// animator.SetBool("IsRunner", true);