using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public static int score = 0;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TempController tempController;
    void Start()
    {
        //PlayerPrefs.GetInt("HighScore");
    }
    private int scoreAdd;
    public void CheckPossibleScores()
    {
        if (SupplyDeposit.supplyedValue > 60 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += ValueStorage.COOLANTSUPPLYLEVEL_SCORE_ADD;
        }
        else if (SupplyDeposit.supplyedValue < 60 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd -= ValueStorage.COOLANTSUPPLYLEVEL_SCORE_SUBTRACT;
        }

        //Debug.Log($"500 < TempController.temp && TempController.temp > 1000 = {500 < TempController.temp && TempController.temp > 1000}");

        if (500 < TempController.temp && TempController.temp < 1000 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += 2;
        }
        else if (1000 < TempController.temp && TempController.temp < 1350 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += 6;
        }
        else if (1350 < TempController.temp && TempController.temp < 1800 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += 3;
        }
        else if (1800 < TempController.temp && TempController.temp < 2000 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += 2;
        }
        else if (2000 < TempController.temp && TempController.temp < 3000 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += 1;
        }   
        else if (3350 < TempController.temp && TempController.temp < 3600 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += -3;
        }
        else if (3600 < TempController.temp && TempController.temp < 4000 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += -6;
        }
        score += scoreAdd;
        Debug.Log($"scoreAdd = {scoreAdd}");
        scoreAdd = 0;
        scoreText.text = $"Score: {score}";
    }
}
