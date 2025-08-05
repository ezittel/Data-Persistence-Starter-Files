using UnityEngine;

public class WallZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip wallSound;
    private void OnCollisionEnter(Collision other)
    {
        AudioSource.PlayClipAtPoint(wallSound, transform.position);
    }
}
