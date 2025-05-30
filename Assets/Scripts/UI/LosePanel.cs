using UnityEngine;

namespace SecondProject
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private AudioSource _openSound;

        void Start()
        {
            _gameManager.Loss += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
            _openSound.Play();
        }
    }
}
