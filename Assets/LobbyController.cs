using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private string gameScene;

    //private void Awake()
    //{
    //    buttonPlay = GetComponent<Button>();
    //    buttonQuit = GetComponent<Button>();
    //}

    private void Start()
    {
        buttonPlay.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(gameScene);
    }
}
