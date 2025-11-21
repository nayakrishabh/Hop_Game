using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            // If the player exits the platform, deactivate it
            gameObject.SetActive(false);
            PoolManager.Instance.getPoolDictionary()["Panel"].Enqueue(gameObject);
        }
    }
}
