using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform Playertransform, Destination;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = player.GetComponent<Player>();
            player.SetActive(false);
            playerScript.Respawn(Destination.position);
            player.SetActive(true);
        }
    }

}
