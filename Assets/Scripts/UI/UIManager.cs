using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        //[SerializeField] private GameObject enemyKilledAchievementPanel;
       // [SerializeField] private TextMeshProUGUI enemyKilledAchievemenText;

        protected override void Awake()
        {
            base.Awake();
        }

        //showing popups on unlocked achievements
        async public void PopUpAchievement(string achievement)
        {
            bulletAchivementPanel.SetActive(true);
            //enemyKilledAchievementPanel.SetActive(true);
            bulletAchiementText.text = "Achievement Unlocked : " + achievement;
           // enemyKilledAchievemenText.text= "Achievement Unlocked : " + achievement;
            await new WaitForSeconds(3f);
            bulletAchivementPanel.SetActive(false);
            //enemyKilledAchievementPanel.SetActive(false);
        }
    }
}