using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    [Header("Lives")]
    public int playerLives = 3;
    public TextMeshProUGUI wristLivesText;

    [Header("Tasks")]
    public int totalTasks = 12;
    [SerializeField] public int tasksRemaining;
    public TextMeshProUGUI tasksLeftText;

    [Header("Audio")]
    public AudioClip levelCompleteClip;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DelayedInit());
    }

    private IEnumerator DelayedInit()
    {
        yield return new WaitForSeconds(0.1f); // Wait for objects to be in the scene

        tasksRemaining = totalTasks;
        UpdateTaskUI();
        UpdateLivesUI();
    }


    public void CompleteTask()
    {
        if (tasksRemaining > 0)
        {
            tasksRemaining--;
            UpdateTaskUI();
        }

        if (tasksRemaining <= 0)
        {
            Debug.Log("All tasks completed!");
            audioSource.PlayOneShot(levelCompleteClip);
        }
    }

    public void ReduceLives()
    {
        playerLives--;

        if (playerLives <= 0)
        {
            Debug.Log("Game Over! Restarting...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            UpdateLivesUI();
        }
    }

    private void UpdateLivesUI()
    {
        if (wristLivesText != null)
            wristLivesText.text = "Lives: " + playerLives;
    }
    
    private void UpdateTaskUI()
    {
        if (tasksLeftText != null)
            tasksLeftText.text = "Tasks Left: " + tasksRemaining;
    }

}
