using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SecondProject.Enemy;

namespace SecondProject
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;

        public PlayerCharacter Player { get; private set; }
        //public List<EnemyCharacter> Enemies { get; private set; }
        public List<EnemyCharacter> Enemies { get; private set; } = new List<EnemyCharacter>();

        public TimerUI Timer { get; private set; }

        private bool _isGameActive = true;
        public bool IsGameActive => _isGameActive;

        public int _enemiesKilledCount = 0;

        public int totalKilledCount = 0;

        private void Start()
        {
            SceneEvents.OnPlayerSpawned += OnPlayerSpawned;

            //Enemies = FindObjectsOfType<EnemyCharacter>().ToList();

            Timer = FindObjectOfType<TimerUI>();

            //foreach (var enemy in Enemies)
                //enemy.Dead += OnEnemyDead;

            Timer.TimeEnd += PlayerLose;

        }

        private void OnPlayerSpawned(PlayerCharacter player)
        {
            Player = player;
            Player.Dead += OnPlayerDead;

            if (Timer != null)
            {
                Timer.StartTimer();
            }
        }

        private void OnPlayerDead(BaseCharacter sender)
        {
            if (!_isGameActive) 
                return;
            Loss?.Invoke();
            Player.Dead -= OnPlayerDead;
            _isGameActive = false;
            Time.timeScale = 0f;
        }

        private void OnEnemyDead(BaseCharacter sender)
        {

            var enemy = sender as EnemyCharacter;
            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;

            totalKilledCount--;

            string ownerName = enemy.LastBulletOwner.Replace("(Clone)", "").Trim();
            if (ownerName == "Player")
            {
                _enemiesKilledCount++;
            }

            if (_enemiesKilledCount >= CountEnemyUI.TotalEnemiesToKill)
            //if (Enemies.Count == 0)
            {
                if (!_isGameActive)
                    return;
                Win?.Invoke();
                _isGameActive = false;
                Time.timeScale = 0f;
            }
        }

        private void PlayerLose()
        {
            Timer.TimeEnd -= PlayerLose;
            if (!_isGameActive)
                return;
            Loss?.Invoke();
            _isGameActive = false;
            Time.timeScale = 0f;
        }

        public void RegisterEnemy(EnemyCharacter enemy)
        {
            if (enemy == null) return;
            Enemies.Add(enemy);
            enemy.Dead += OnEnemyDead;

            totalKilledCount++;
        }

    }
}
