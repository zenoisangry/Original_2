using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] public Transform spawnPoint;
    
    public void MovePlayer()
    {
        Vector3 distance = GameManager.Instance.CurrentPlayer.transform.position - Camera.main.transform.position;
        CharacterController characterController = GameManager.Instance.CurrentPlayer.GetComponent<CharacterController>();
        characterController.enabled = false;
        GameManager.Instance.CurrentPlayer.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        Camera.main.transform.position = GameManager.Instance.CurrentPlayer.transform.position - distance;
        characterController.enabled = true;
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
