using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("Door")]
    public GameObject doorManager;
    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public bool roomLeft, roomRight, roomUp, roomDown;
    public int doorNumber;

    [Header("Position")]
    public Text text;
    public int stepToStart;

    [Header("Enemy")]
    public GameObject enemyManager;
    public bool complete;
    public bool end;

    [Header("UI")]
    public WinMenu winMenu;

    // GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);

        if (stepToStart != 0)
        {
            doorManager.SetActive(false);
            enemyManager.SetActive(false);
        }

        winMenu = GameObject.Find("Canvas").GetComponent<WinMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            doorManager.SetActive(false);
            if (end == true)
            {
                winMenu.Pause();
            }
        }
    }

    // Update is called once per frame
    public void UpdateRoom(float xOffset, float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 10));

        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
            doorManager.SetActive(true);
            enemyManager.SetActive(true);
        }
    }
}
