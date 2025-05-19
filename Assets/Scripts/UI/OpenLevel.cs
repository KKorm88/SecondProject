using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SecondProject.PickUp;

public class OpenLevel : MonoBehaviour
{
    private Button _openLevelButton;

    [SerializeField]
    private List<int> _levelsList = new List<int>();

    private void Start()
    {
        _openLevelButton = GetComponent<Button>();
        _openLevelButton.onClick.RemoveAllListeners();
        _openLevelButton.onClick.AddListener(OpenRandomLevel);
    }

    private void OpenRandomLevel()
    {
        Time.timeScale = 1f;
        PlayerAndEnemyStatus.StatusPlayerSpawned = false;
        PlayerAndEnemyStatus.StatusEnemySpawned = false;
        int level = _levelsList[Random.Range(0, _levelsList.Count)];
        SceneManager.LoadScene(level);
    }
}
