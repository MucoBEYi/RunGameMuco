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


    //objeler arasýndaki mesafe
    [SerializeField] float zDistance;

    //rasgele pozisyon aralýðý                                          //örnek sayý
    [SerializeField] float randomMinX, randomMaxX;                      //min -2, max 2


    //baþlangýç ve bitiþ pozisyonu(eðer x y z eksenin tamamýný kontrol etmek istiyorsanýz vektör olmalý)
    [SerializeField] float beginning, ending;


    private void Start()
    {
        //mesafe hesaplama
        int distanceCount = (int)((ending - beginning) / zDistance);



        //mesafe sonucu sayýsý kadar döngü tekrarlanýr   ek bilgi: distanceCount yerine direkt 100 yazarak 100 defa döngünün gerçekleþmesini saðlayabiliriz
        for (int i = 0; i < distanceCount; i++)
        {
            //objeler arasý mesafe hesaplama             ek bilgi: yukarýdaki 'mesafe hesaplama' ile alakasý yok
            float _zPosObject = (i * zDistance) + beginning;


            //objenin konumunu hesaplama 1
            Vector3 _spawnPos1 = new Vector3(-4.5f, 1, _zPosObject);
            //obje klonlama
            Instantiate(spawnObject[Random.Range(0, spawnObject.Length)], _spawnPos1, Quaternion.identity);               //quaterion.identity bütün rotasyonu 0 yapýyor, yani elinizle döndürdüðünüz objenin rotasyonu anýnda 0 olur


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