using UnityEngine;

public class HighScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore").ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
