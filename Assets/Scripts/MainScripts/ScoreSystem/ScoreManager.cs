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
        score += scoreAdd;
        scoreText.text = $"Score: {score}";
    }
}
