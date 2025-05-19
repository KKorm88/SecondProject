using SecondProject;
using TMPro;
using UnityEngine;

public class TotalNumberEnemyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _outputTotalTextEnemy;
    private string _formatTotalEnemy;

    private GameManager _gameManager;

    private int _totalKilledCount;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();


        _formatTotalEnemy = _outputTotalTextEnemy.text;
        _outputTotalTextEnemy.text = string.Format(_formatTotalEnemy, _totalKilledCount);
    }

    private void Update()
    {
        if (_gameManager != null)
        {
            int currentTotalKilled = _gameManager.totalKilledCount;
            UpdateTotalKilledCount(currentTotalKilled);
        }
    }

    public void UpdateTotalKilledCount(int currentKilled)
    {
        _totalKilledCount = currentKilled;
        _outputTotalTextEnemy.text = string.Format(_formatTotalEnemy, _totalKilledCount);
    }
}
