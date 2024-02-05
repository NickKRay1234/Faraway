using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.Command.Commands
{
    public class BehaviourGameUI : MonoBehaviour
    {
        [Header("Components Ui")] 
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _repeatGame;
        [Header("Components Games")] 
        [SerializeField] private Player _player;

        private void Awake() {
            _startGame.onClick.AddListener(StartGame);
            _repeatGame.onClick.AddListener(RepeatGame);
            DisableMovement();
        }

        private void StartGame() {
            EnableMovement(); 
            _startGame.gameObject.SetActive(false);
            _canvas.enabled = false;
        }
        
        [ContextMenu("FinishGame")]
        private void FinishGame() {
            _repeatGame.gameObject.SetActive(true);
            DisableMovement();
        }

        public void RepeatGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void DisableMovement() => _player.enabled = false;
        private void EnableMovement() => _player.enabled = true;

    }
}