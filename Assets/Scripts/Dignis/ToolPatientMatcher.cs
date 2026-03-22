using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ToolPatientPair
{
    public GameObject tool;
    public GameObject patient;
}

public class ToolPatientMatcher : MonoBehaviour
{
    public ToolPatientPair[] pairs;
    public float correctDistance = 1.5f;

    public Text feedbackText;

    private bool[] alreadyMatched;

    void Start()
    {
        alreadyMatched = new bool[pairs.Length];
        feedbackText.text = "";
    }

    void Update()
    {
        for (int i = 0; i < pairs.Length; i++)
        {
            if (alreadyMatched[i]) continue;

            float distance = Vector3.Distance(
                pairs[i].tool.transform.position,
                pairs[i].patient.transform.position
            );

            Debug.Log(distance);

            if (distance <= correctDistance)
            {
                StartCoroutine(ShowCorrect());
                alreadyMatched[i] = true;
            }
        }
    }

    System.Collections.IEnumerator ShowCorrect()
    {
        feedbackText.text = "Correct!";
        yield return new WaitForSeconds(2f);
        feedbackText.text = "";
    }
}