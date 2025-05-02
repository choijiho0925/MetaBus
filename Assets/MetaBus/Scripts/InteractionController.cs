using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InteractionController : BaseInteractionHandler
{
    [SerializeField] private Tilemap floor;
    [SerializeField] private Tilemap backDesign;
    [SerializeField] private Tilemap foreDesign;
    [SerializeField] private GameObject enterGamePanel;
    [SerializeField] private GameObject gameDescriptionPanel;
    [SerializeField] private GameObject pressF;

    bool isIn = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isIn == true)
        {
            gameDescriptionPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            floor.color = new Color(1, 1, 1, 0.78f);
            backDesign.color = new Color(1, 1, 1, 0.78f);
            foreDesign.color = new Color(1, 1, 1, 0.78f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            enterGamePanel.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Npc"))
        {
            isIn = true;
            pressF.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            floor.color = new Color(1, 1, 1, 1);
            backDesign.color = new Color(1, 1, 1, 1);
            foreDesign.color = new Color(1, 1, 1, 1);
        }

        if (collision.gameObject.CompareTag("Npc"))
        {
            isIn = false;
            gameDescriptionPanel.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Ladder"))
        {
            if(enterGamePanel != null)
            enterGamePanel.SetActive(false);
        }
    }
}
