using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSpawn : MonoBehaviour
{
    //spawnlanacak objeler
    [SerializeField] GameObject[] spawnObject;


    //objeler aras�ndaki mesafe
    [SerializeField] float zDistance;

    //rasgele pozisyon aral���                                          //�rnek say�
    [SerializeField] float randomMinX, randomMaxX;                      //min -2, max 2


    //ba�lang�� ve biti� pozisyonu(e�er x y z eksenin tamam�n� kontrol etmek istiyorsan�z vekt�r olmal�)
    [SerializeField] float beginning, ending;


    private void Start()
    {
        //mesafe hesaplama
        int distanceCount = (int)((ending - beginning) / zDistance);



        //mesafe sonucu say�s� kadar d�ng� tekrarlan�r   ek bilgi: distanceCount yerine direkt 100 yazarak 100 defa d�ng�n�n ger�ekle�mesini sa�layabiliriz
        for (int i = 0; i < distanceCount; i++)
        {
            //objeler aras� mesafe hesaplama             ek bilgi: yukar�daki 'mesafe hesaplama' ile alakas� yok
            float _zPosObject = (i * zDistance) + beginning;


            //objenin konumunu hesaplama 1
            Vector3 _spawnPos1 = new Vector3(-4.5f, 1, _zPosObject);
            //obje klonlama
            Instantiate(spawnObject[Random.Range(0, spawnObject.Length)], _spawnPos1, Quaternion.identity);               //quaterion.identity b�t�n rotasyonu 0 yap�yor, yani elinizle d�nd�rd���n�z objenin rotasyonu an�nda 0 olur


            //objenin konumunu hesaplama 2
            Vector3 _spawnPos2 = new Vector3(Random.Range(randomMinX, randomMaxX), 1, _zPosObject);
            //obje klonlama
            Instantiate(spawnObject[Random.Range(0, spawnObject.Length)], _spawnPos2, Quaternion.identity);


            //objenin konumunu hesaplama 3
            Vector3 _spawnPos3 = new Vector3(4.5f, 1, _zPosObject);
            //obje klonlama
            Instantiate(spawnObject[Random.Range(0, spawnObject.Length)], _spawnPos3, Quaternion.identity);
        }
    }
}