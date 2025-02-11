using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Restart to last Checkpoint
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Death ! ");
            PlayerManager.Instance.player.position = PlayerManager.Instance.currentCheckPoint.position;

            //Reset
            PlayerManager.Instance.SetCP(0);
        }
    }
}
