using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Outscal.BattleTank
{
    /// <summary>
    /// this class handles the Ui part of game
    /// all the popups and achivements will display on game scene
    /// </summary>
    public class UIManager : MonoGenericSingletone<UIManager>
    {
        [SerializeField] private GameObject bulletAchivementPanel;
        [SerializeField] private TextMeshProUGUI bulletAchiementText;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Button buttonMenu;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonRestartOnLose;
        [SerializeField] private Button buttonMenuOnLose;
        [SerializeField] private string loadCurrentScene;
        [SerializeField] private string loadLobbyScene;
        [SerializeField] private TextMeshProUGUI HealthText;
        [SerializeField] private TextMeshProUGUI ScoreText;
        public TankModel tankModel;
        private int currentScore;

        private Scene scene;

        //[SerializeField] private GameObject enemyKilledAchievementPanel;
        // [SerializeField] private TextMeshProUGUI enemyKilledAchievemenText;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            currentScore = 0;
            ScoreText.text = "Score:" + currentScore.ToString();
            scene = SceneManager.GetActiveScene();
            //UpdateHealthText(tankModel.GetHealth());
            buttonRestart.onClick.AddListener(LoadCurrentScene);
            buttonRestartOnLose.onClick.AddListener(LoadCurrentScene);
            buttonMenu.onClick.AddListener(LoadLobbyScene);
            buttonMenuOnLose.onClick.AddListener(LoadLobbyScene);
        }

        //showing popups on unlocked achievements
        async public void PopUpAchievement(string achievement)
        {
            bulletAchivementPanel.SetActive(true);
            UpdateScoreText();
            //enemyKilledAchievementPanel.SetActive(true);
            bulletAchiementText.text = "Achievement Unlocked : " + achievement;
           // enemyKilledAchievemenText.text= "Achievement Unlocked : " + achievement;
            await new WaitForSeconds(3f);
            bulletAchivementPanel.SetActive(false);
            //enemyKilledAchievementPanel.SetActive(false);
        }

        public void OnButtonClickPlayerWin()
        {
            SceneManager.LoadScene(1);
            winPanel.SetActive(false); 
            losePanel.SetActive(false);
        }

        public void OnButtonClickPlayerLose()
        {
            SceneManager.LoadScene(1);
            losePanel.SetActive(false);
            winPanel.SetActive(false);
        }

        public void PopUpPlayerWinPanel()
        {
            winPanel.SetActive(true);
        }

        public void PopUpPlayerLosePanel()
        {
            losePanel.SetActive(true);
        }

        public void LoadCurrentScene()
        {
            Debug.Log("sxd");
            //Time.time.Equals(0);
            SceneManager.LoadScene(loadCurrentScene);
            losePanel.SetActive(false);
            winPanel.SetActive(false);
        }

        public void LoadLobbyScene()
        {
            SceneManager.LoadScene(loadLobbyScene);
            losePanel.SetActive(false);
            winPanel.SetActive(false);
        }

        public void UpdateScoreText(int scoreMultiplier = 1)
        {
            int finalScore = (currentScore + 10) * scoreMultiplier;
            currentScore = finalScore;
            ScoreText.text = "Score: " + finalScore.ToString();
        }

        public void UpdateHealthText(float currentHealth)
        {
            if (currentHealth < 0) currentHealth = 0;
            HealthText.text = "Health: " + currentHealth.ToString();
        }

        public void ResetScore()
        {
            currentScore = 0;
            ScoreText.text = "Score: " + currentScore.ToString();
        }
    }
}