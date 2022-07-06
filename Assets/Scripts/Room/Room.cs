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

    [Header("Clear")]
    public bool complete;

    GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            // foreach (Transform child in gameObject.transform)
            // {
            //     if (child.CompareTag("Door"))
            //     {
            //         child.gameObject.SetActive(false);
            //     }
            // }
            doors = GameObject.FindGameObjectsWithTag("Door");
            foreach (GameObject child in doors)
            {
                child.SetActive(false);
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
        }
    }
}
