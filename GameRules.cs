using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public GameObject[] rightSwitchSpawns = new GameObject[11];
    public GameObject[] leftSwitchSpawns = new GameObject[11];

    public GameObject rightSwitchPrefab;
    GameObject rightClone1;
    GameObject rightClone2;
    GameObject rightClone3;

    public GameObject leftSwitchPrefab;
    GameObject leftClone1;
    GameObject leftClone2;
    GameObject leftClone3;

    PlayerMovement playerMovement;
    
    void Awake(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rightSwitchSpawns =  GameObject.FindGameObjectsWithTag("RightSpawns");
        leftSwitchSpawns = GameObject.FindGameObjectsWithTag("LeftSpawns");
    }
    
    public void RandomLeft()
    {   int leftRandom1 = Random.Range(0, leftSwitchSpawns.Length -1);
        Vector2 leftOne = new Vector2(leftSwitchSpawns[leftRandom1].transform.position.x,leftSwitchSpawns[leftRandom1].transform.position.y);
        leftClone1 = Instantiate(leftSwitchPrefab, new Vector2(leftOne.x,leftOne.y), Quaternion.identity);

        int leftRandom2 = Random.Range(0, leftSwitchSpawns.Length -1);
        if(playerMovement.currentScore < 10){
            while(leftRandom2 == leftRandom1)
            {
                leftRandom2 = Random.Range(0, leftSwitchSpawns.Length -1);
            }
            Vector2 leftTwo = new Vector2(leftSwitchSpawns[leftRandom2].transform.position.x,leftSwitchSpawns[leftRandom2].transform.position.y);
            leftClone2 = Instantiate(leftSwitchPrefab, new Vector2(leftTwo.x,leftTwo.y), Quaternion.identity);
        }
        int leftRandom3 = Random.Range(0, leftSwitchSpawns.Length -1);
        if(playerMovement.currentScore < 5){
            while(leftRandom3 == leftRandom1 || leftRandom3 == leftRandom2)
            {
                leftRandom3 = Random.Range(0, leftSwitchSpawns.Length -1);
            }
            Vector2 leftThree = new Vector2(leftSwitchSpawns[leftRandom3].transform.position.x,leftSwitchSpawns[leftRandom3].transform.position.y);
            leftClone3 = Instantiate(leftSwitchPrefab, new Vector2(leftThree.x,leftThree.y), Quaternion.identity);
        }
        
    }

    public void RandomRight()
    {
        int rightRandom1 = Random.Range(0, rightSwitchSpawns.Length -1);
        Vector2 rightOne = new Vector2(rightSwitchSpawns[rightRandom1].transform.position.x,rightSwitchSpawns[rightRandom1].transform.position.y);
        rightClone1 = Instantiate(rightSwitchPrefab, new Vector2(rightOne.x,rightOne.y), Quaternion.Euler(0,180,0));

        int rightRandom2 = Random.Range(0, rightSwitchSpawns.Length -1);
        if(playerMovement.currentScore < 10){
            while (rightRandom2 == rightRandom1)
            {
                rightRandom2 = Random.Range(0, rightSwitchSpawns.Length -1);
            }
            Vector2 rightTwo = new Vector2(rightSwitchSpawns[rightRandom2].transform.position.x,rightSwitchSpawns[rightRandom2].transform.position.y);
            rightClone2 = Instantiate(rightSwitchPrefab, new Vector2(rightTwo.x,rightTwo.y), Quaternion.Euler(0,180,0));

        }
        int rightRandom3 = Random.Range(0, rightSwitchSpawns.Length -1);
        if(playerMovement.currentScore < 5){
            while (rightRandom3 == rightRandom1 || rightRandom3 == rightRandom2)
            {
                rightRandom3 = Random.Range(0, rightSwitchSpawns.Length -1);
            }
            Vector2 rightThree = new Vector2(rightSwitchSpawns[rightRandom3].transform.position.x,rightSwitchSpawns[rightRandom3].transform.position.y);
            rightClone3 = Instantiate(rightSwitchPrefab, new Vector2(rightThree.x,rightThree.y), Quaternion.Euler(0,180,0));
        }
        
    }

    public void DestroyRightSwitches(){
        Debug.Log("Right Switches Destroyed");
        Destroy(rightClone1);
        Destroy(rightClone2);
        Destroy(rightClone3);
    }

    public void DestroyLeftSwitches(){
        Debug.Log("Left Switches Destroyed");
        Destroy(leftClone1);
        Destroy(leftClone2);
        Destroy(leftClone3);
    }
}
