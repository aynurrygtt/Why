using UnityEngine;
using UnityEngine.SceneManagement;

public class Ev : MonoBehaviour
{
   // [SerializeField] AudioSource ses;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Karakter")
        {
           //  ses.Play();
            Invoke("SonrakiBölüm", 1f);
        }
    }
    private void SonrakiBölüm()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}