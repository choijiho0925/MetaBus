using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Tilemap floor;
    [SerializeField] private Tilemap backDesign;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            floor.color = new Color(1, 1, 1, 0.78f);
            backDesign.color = new Color(1, 1, 1, 0.78f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F");
            }
        }

    }
}
