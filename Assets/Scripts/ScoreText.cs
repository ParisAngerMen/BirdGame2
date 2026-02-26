using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI score;

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = GameManager.instance.totalPoints.ToString();
    }
}
