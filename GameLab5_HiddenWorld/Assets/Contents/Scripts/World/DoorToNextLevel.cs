using UnityEngine;

public class DoorToNextLevel : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelController levelController = GameManager.Instance.CurrentLevel;
            if (levelController != null)
            {
                levelController.LoadNextLevel();
            }
        }
    }
}
