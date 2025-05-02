using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlapPlaneBaseUI : MonoBehaviour
{
    protected FlapPlaneUIManager gameUIManager;

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
