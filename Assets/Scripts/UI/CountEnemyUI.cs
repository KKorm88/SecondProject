using SecondProject;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountEnemyUI : MonoBehaviour
{
    public static int TotalEnemiesToKill { get; private set; }

    [SerializeField]
    public int totalEnemiesToKill;
    [Tooltip("Количество противников, которых нужно уничтожить для победы")]

    private int enemiesKilledCount;

    [SerializeField]
    private TextMeshProUGUI _outputTextEnemy;
    private string _formatEnemy;

    private GameManager _gameManager;

    private void Start()
    {
        TotalEnemiesToKill = totalEnemiesToKill;

        _gameManager = FindObjectOfType<GameManager>();

        _formatEnemy = _outputTextEnemy.text;
        _outputTextEnemy.text = string.Format(_formatEnemy, enemiesKilledCount, totalEnemiesToKill);
    }

    private void Update()
    {
        if (_gameManager != null)
        {
            int currentKilled = _gameManager._enemiesKilledCount;
            if (currentKilled != enemiesKilledCount)
            {
                UpdateKilledCount(currentKilled);
            }
        }
    }

    public void UpdateKilledCount(int currentKilled)
    {
        enemiesKilledCount = currentKilled;
        _outputTextEnemy.text = string.Format(_formatEnemy, enemiesKilledCount, totalEnemiesToKill);
    }
}
