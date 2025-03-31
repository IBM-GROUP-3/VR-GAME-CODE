using System.Collections;
using UnityEngine;

public class QuestionObject : MonoBehaviour
{
    public GameObject[] answerButtons; // Assign answer buttons in Inspector
    public int correctAnswerIndex; // Index of the correct answer in the array
    public float sinkSpeed = 1.5f; // Speed at which the object sinks

    private bool isSinking = false;
    public Transform parentObject; // Reference to parent

    private void Start()
    {
        // Assign the parent object (the one that should sink)
        parentObject = transform.parent;

        // Assign button listeners dynamically
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // Prevent closure issue
            answerButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => AnswerSelected(index));
        }
    }

    void AnswerSelected(int index)
    {
        if (index == correctAnswerIndex)
        {
            // Correct answer, start sinking the parent
            isSinking = true;
            StartCoroutine(DestroyObject());
            
        }
        else
        {
            // Incorrect answer, reduce player lives
            QuizManager.Instance.ReduceLives();
        }
    }

    private void Update()
    {
        if (isSinking && parentObject != null)
        {
            parentObject.position -= new Vector3(0, sinkSpeed * Time.deltaTime, 0);

            
            if (parentObject.position.y < -5) 
            {
                Destroy(parentObject.gameObject);
            }
        }
    }

    public IEnumerator DestroyObject()
    {
        QuizManager.Instance.CompleteTask();
        yield return new WaitForSeconds(3);
    }
}
