using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        SwitchMenu(mainMenu);
    }

    public void SwitchMenu(GameObject uiMenu)
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
