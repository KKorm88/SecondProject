using SecondProject;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowUI : MonoBehaviour
{
    public GameObject arrowPrefab;

    private GameManager _gameManager;

    [SerializeField]
    [Tooltip("Минимальный размер стрелки")]
    private float minScale;

    [SerializeField]
    [Tooltip("Максимальный размер стрелки")]
    private float maxScale;

    [SerializeField]
    [Tooltip("Максимальная дистанция для минимальной стрелочки")]
    private float maxDistance;

    private List<GameObject> arrows = new List<GameObject>();

    float padding = 20f;

    void Start()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
            if (_gameManager == null)
            {
                Debug.LogError("GameManager не найден в сцене!");
                return;
            }
        }

        var enemies = _gameManager.Enemies;
        foreach (var enemy in enemies)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrows.Add(arrow);
        }
    }

    void Update()
    {
        var enemies = _gameManager.Enemies;

        while (arrows.Count < enemies.Count)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrows.Add(arrow);
        }
        while (arrows.Count > enemies.Count)
        {
            GameObject excessArrow = arrows[arrows.Count - 1];
            arrows.RemoveAt(arrows.Count - 1);
            Destroy(excessArrow);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            var arrow = arrows[i];

            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);

            if (screenPos.z <= 0)
            {
                arrow.SetActive(false);
                continue;
            }
            else
            {
                if (!arrow.activeSelf)
                    arrow.SetActive(true);
            }

            bool isOnScreen = screenPos.z > 0 &&
                              screenPos.x >= 0 && screenPos.x <= Screen.width &&
                              screenPos.y >= 0 && screenPos.y <= Screen.height;

            if (isOnScreen)
            {
                arrow.SetActive(false);
                continue;
            }
            else
            {
                Vector2 borderPoint = GetBorderPoint(new Vector2(Screen.width / 2f, Screen.height / 2f),
                                                      new Vector2(screenPos.x, screenPos.y),
                                                      padding);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform)transform,
                    borderPoint,
                    null,
                    out Vector2 canvasPos);

                RectTransform rt = arrow.GetComponent<RectTransform>();
                rt.localPosition = canvasPos;

                Vector3 directionForArrow;
                if (screenPos.z > 0)
                {
                    directionForArrow = new Vector3(
                        borderPoint.x - Screen.width / 2f,
                        borderPoint.y - Screen.height / 2f,
                        0).normalized;
                }
                else
                {
                    directionForArrow = new Vector3(
                        (Screen.width / 2f) - borderPoint.x,
                        (Screen.height / 2f) - borderPoint.y,
                        0).normalized;
                }

                float angle = Mathf.Atan2(directionForArrow.y, directionForArrow.x) * Mathf.Rad2Deg;
                rt.rotation = Quaternion.Euler(0, 0, angle);

                float distance = Vector3.Distance(Camera.main.transform.position, enemy.transform.position);
                float t = Mathf.Clamp01(distance / maxDistance);
                float scaleFactor = Mathf.Lerp(maxScale, minScale, t);
                rt.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            }
        }
    }

    private Vector2 GetBorderPoint(Vector2 screenCenter, Vector2 targetScreenPos, float padding)
    {
        Vector2 direction = (targetScreenPos - screenCenter).normalized;

        float mXPositiveSide = (direction.x > 0) ? (Screen.width - screenCenter.x - padding) / direction.x : float.PositiveInfinity;
        float mXNegativeSide = (direction.x < 0) ? (-screenCenter.x + padding) / direction.x : float.PositiveInfinity;

        float mYPositiveSide = (direction.y > 0) ? (Screen.height - screenCenter.y - padding) / direction.y : float.PositiveInfinity;
        float mYNegativeSide = (direction.y < 0) ? (-screenCenter.y + padding) / direction.y : float.PositiveInfinity;

        float minScaleX = Mathf.Min(mXPositiveSide, mXNegativeSide);
        float minScaleY = Mathf.Min(mYPositiveSide, mYNegativeSide);

        float scaleFactor = Mathf.Min(minScaleX, minScaleY);

        Vector2 borderPoint = screenCenter + direction * scaleFactor;

        borderPoint.x = Mathf.Clamp(borderPoint.x, padding, Screen.width - padding);
        borderPoint.y = Mathf.Clamp(borderPoint.y, padding, Screen.height - padding);

        return borderPoint;
    }
}