using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Spawner spawner;
    private Vector2 screenBounds;

    public GameObject title;

    public GameObject playerPrefab;
    private GameObject player;
    private bool gameStarted = false;

    public GameObject splash;

    void Start()
    {
        spawner.active = true;
        title.SetActive(true);
        splash.SetActive(false);
    }

    private void Awake(){
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void ResetGame(){
        spawner.active = true;
        title.SetActive(false);
        splash.SetActive(false);

        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameStarted){
            if (Input.anyKeyDown){
                ResetGame();
            }
        }else{
            if (!player){
                OnPlayerKilled();
            }
        }
        if (Input.anyKeyDown){
            spawner.active = true;
            title.SetActive(false);
        }

        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombObject in nextBomb){
            if (bombObject.transform.position.y < (-screenBounds.y - 12)){
                Destroy(bombObject);
            }
        }
    }

    void OnPlayerKilled(){
        spawner.active = false;
        gameStarted = false;

        splash.SetActive(true);
    }
}
