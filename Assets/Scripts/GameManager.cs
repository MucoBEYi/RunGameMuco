using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] MovementSettings mSettings;

    [SerializeField] Movement movement;

    [SerializeField] private Animator animator;

    public bool GameOverBool { get; private set; }

    public int CoinInt { get; private set; }

    public float speedBuff;


    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        movement = FindAnyObjectByType<Movement>();
    }


    public void RunnerAnimation()
    {
        animator.SetBool("IsRunner", true);
    }

    private void FinishGame()
    {
        animator.SetBool("IsFýnýsh", true);
    }

    public void GameOver()
    {
        GameOverBool = true;

        animator.SetBool("IsRunner", false);

        print("oyun bitti");
    }

    public void Coin()
    {
        CoinInt++;

        SoundManager.Instance.CoinEffect();

    }

    public IEnumerator MovementBuff()
    {
        
       if (movement.moveZ <= (mSettings.limitSpeed + mSettings.speed))
        {
            
            //SENÝN GÝBÝ HATANIN.........
            speedBuff += mSettings.speedBuff;


            print(speedBuff + " Buff alýndý");


            yield return new WaitForSeconds(2.5f);

            speedBuff -= mSettings.speedBuff;
        }
        else
        {
            print("becerdin");
            yield break;
        }

    }

    public void CoinText()
    {

    }

}
