﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayScript : MonoBehaviour
{
    public GameObject coronaGameObject;
    public Transform coronaSpawnPoint;
    const float WAIT_TIME = 1.0f;
    public MiniGameController miniGameControllerInstance;

    private int coronaCounter;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCorona(WAIT_TIME));
        coronaCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(coronaCounter >= 15){
            this.gameObject.SetActive(false);
            miniGameControllerInstance.CloseMiniGame();
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {    
            Vector2 touchPosWorld2D = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if(hitInformation.collider != null) {
                GameObject touchedObject = hitInformation.transform.gameObject;
                if(touchedObject.tag == "Corona"){
                    Destroy(touchedObject);
                    coronaCounter++;
                }
            }

        } else if(Input.GetMouseButtonDown(0)){
            Vector2 touchPosWorld2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if(hitInformation.collider != null) {
                GameObject touchedObject = hitInformation.transform.gameObject;
               if(touchedObject.tag == "Corona"){
                    Destroy(touchedObject);
                    coronaCounter++;
                }
            }
        }
    }

    IEnumerator spawnCorona(float waitTime)
    {
        while(this.gameObject.activeSelf) {
            float xValue = Random.Range(1f * (Screen.width / 4), (3 * Screen.width / 4));
            Vector3 normvalue = Camera.main.ScreenToWorldPoint(new Vector3(xValue, 0f, 0f));
            Instantiate(coronaGameObject, new Vector3(normvalue.x, coronaSpawnPoint.position.y, -10f), transform.rotation);

            yield return new WaitForSeconds(waitTime);
        }
    }
}