using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public static int score = 0;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text scoreTextExtra;
    [SerializeField] public Animator scoreTextAnimator;
    [SerializeField] public TMP_Text scoreWorkshiftTextExtra;
    [SerializeField] public Animator scoreWorkshiftTextAnimator;

    [SerializeField] public TempController tempController;
    [SerializeField] AudioSource source;
    [SerializeField] public AudioClip workshift_end_funny_Audio;
    void Start()
    {
        //PlayerPrefs.GetInt("HighScore");
    }

    public void WorkshiftEnd(string workshift)
    {
        scoreWorkshiftTextExtra.text = $"{workshift} workshift ended! Score awarded: +{ValueStorage.SCORE_WORKSHIFT_END}";
        scoreWorkshiftTextAnimator.Play("WorkshiftEndText", 0, 0f);
        score += ValueStorage.SCORE_WORKSHIFT_END;
        source.PlayOneShot(workshift_end_funny_Audio);
    }

    private int scoreAdd;

    /// <summary>
    /// Checks for all avalible score points that can be added by scoring rules.
    /// </summary>
    public void CheckPossibleScores()
    {
        if (SupplyDeposit.supplyedValue > 60 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd += ValueStorage.SCORE_COOLANTSUPPLYLEVEL_ADD;
        }
        else if (SupplyDeposit.supplyedValue < 60 && tempController.isOnline && !tempController.isMeltdown)
        {
            scoreAdd -= ValueStorage.SCORE_COOLANTSUPPLYLEVEL_SUBTRACT;
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
        if (scoreAdd > 0)
        {
            scoreTextExtra.color = Color.green;
            scoreTextExtra.text = $"+{scoreAdd}";
            scoreTextAnimator.Play("ScoreAddAnimation", 0, 0f);
        }
        else if (scoreAdd < 0)
        {
            scoreTextExtra.color = Color.red;
            scoreTextExtra.text = $"{scoreAdd}";
            scoreTextAnimator.Play("ScoreAddAnimation", 0, 0f);
        }

        scoreAdd = 0;
        scoreText.text = $"Score: {score}";
    }
}
