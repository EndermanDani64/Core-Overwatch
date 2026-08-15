using UnityEngine;
using TMPro;
using FMODUnity;

public class ScoreManager : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.GetInt("HighScore");
    }

    public void WorkshiftEnd(string workshift)
    {
        scoreWorkshiftTextExtra.text = $"{workshift} workshift ended! Score awarded: +{ValueStorage.SCORE_WORKSHIFT_END}";
        scoreWorkshiftTextAnimator.Play("WorkshiftEndText", 0, 0f);
        score += ValueStorage.SCORE_WORKSHIFT_END;
        soundEventEmmiter.Play();
    }

    /// <summary>
    /// Checks for all avalible score points that can be added by scoring rules.
    /// </summary>
    public void CheckPossibleScores()
    {
        if (SupplyDeposit.supplyedValue > 60 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += ValueStorage.SCORE_COOLANTSUPPLYLEVEL_ADD;
        }
        else if (SupplyDeposit.supplyedValue < 60 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome -= ValueStorage.SCORE_COOLANTSUPPLYLEVEL_SUBTRACT;
        }

        //Debug.Log($"500 < TempController.temp && TempController.temp > 1000 = {500 < TempController.temp && TempController.temp > 1000}");

        if (500 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 1000 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += 2;
        }
        else if (1000 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 1350 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += 6;
        }
        else if (1350 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 1800 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += 3;
        }
        else if (1800 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 2000 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += 2;
        }
        else if (2000 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 3000 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += 1;
        }   
        else if (3350 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 3600 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += -3;
        }
        else if (3600 < ReactorManager.ReactorData.Temperature && ReactorManager.ReactorData.Temperature < 4000 && ReactorManager.ReactorData.IsOnline && !OverallEvents.IsMeltdown)
        {
            _scoreIncome += -6;
        }

        score += _scoreIncome;

        if (_scoreIncome > 0)
        {
            scoreTextExtra.color = Color.green;
            scoreTextExtra.text = $"+{_scoreIncome}";
            scoreTextAnimator.Play("ScoreAddAnimation", 0, 0f);
        }
        else if (_scoreIncome < 0)
        {
            scoreTextExtra.color = Color.red;
            scoreTextExtra.text = $"{_scoreIncome}";
            scoreTextAnimator.Play("ScoreAddAnimation", 0, 0f);
        }

        if (_scoreIncome != _lastScoreIncome)
        {
            scoreIncome = _scoreIncome;
        }

        _scoreIncome = 0;
        scoreText.text = $"Score: {score}";
    }

    // values
    [SerializeField] public int score = 0;
    private int _scoreIncome;

    public int scoreIncome;
    private int _lastScoreIncome;

    // visual
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text scoreTextExtra;
    [SerializeField] public Animator scoreTextAnimator;
    [SerializeField] public TMP_Text scoreWorkshiftTextExtra;
    [SerializeField] public Animator scoreWorkshiftTextAnimator;

    // script references
    [SerializeField] StudioEventEmitter soundEventEmmiter;
    [SerializeField] public AudioClip workshift_end_funny_Audio;
}
