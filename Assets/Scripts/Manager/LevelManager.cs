using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool[] levelOpen;

    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButtonParent;

    private string nameLevel;
    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i]) return;

            nameLevel = "Level_" + (i - 1);

            GameObject newButton = Instantiate(levelButton, levelButtonParent);
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadScene(nameLevel));
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i - 1);
        }
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
