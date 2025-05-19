using SecondProject;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public GameObject healthBarPrefab;
    private BaseCharacter character;

    private GameObject healthBarInstance;
    private Image healthFillImage;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            this.enabled = false;
            return;
        }

        character = GetComponent<BaseCharacter>();
        healthBarInstance = Instantiate(healthBarPrefab);
        healthBarInstance.transform.SetParent(transform);
        healthBarInstance.transform.localPosition = new Vector3(0, 2f, 0);
        healthFillImage = healthBarInstance.GetComponentInChildren<Image>();
    }

    void Update()
    {
        if (healthFillImage != null)
        {
            float currentHealth = character.CurrentHealth;
            float maxHealth = character.MaxHealth;
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

}
