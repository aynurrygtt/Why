//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class karakter_hareket : MonoBehaviour
//{
//    Rigidbody2D rb;
//    Animator animator;
//    [SerializeField] float yatayHiz = 50f;
//    [SerializeField] float dikeyHiz = 90f;
//    BoxCollider2D kutu;
//    [SerializeField] LayerMask ziplamaKatmani;
//    Animator animasyonDurumlari;
//    [SerializeField] AudioSource ses;

//    SpriteRenderer goruntu;

//    private bool YerdeMi()
//    {
//        return Physics2D.BoxCast(kutu.bounds.center, kutu.bounds.size, 0f, Vector2.down, 0.2f, ziplamaKatmani);
//    }

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        kutu = GetComponent<BoxCollider2D>();
//        animator = GetComponent<Animator>();
//        goruntu = GetComponent<SpriteRenderer>();

//    }

//    void Update()
//    {
//        // Zıplama
//        if (Input.GetButtonDown("Jump") && YerdeMi())
//        {
//            rb.linearVelocity = new Vector2(rb.linearVelocity.x, dikeyHiz);
//            ses.Play();
//        }

//        // Yatay hareket
//        float xInput = Input.GetAxisRaw("Horizontal");

//        if (xInput != 0)
//        {
//            rb.linearVelocity = new Vector2(xInput * yatayHiz, rb.linearVelocity.y);
//            goruntu.flipX = xInput < 0;
//        }

//        // --- AN�MASYON KONTROL� ---

//        // RUN (Durum = 1)
//        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && YerdeMi())
//        {
//            animator.SetInteger("Durum", 1);
//        }

//        // IDLE (Durum = 0)
//        if (Mathf.Abs(rb.linearVelocity.x) <= 0.1f && YerdeMi())
//        {
//            animator.SetInteger("Durum", 0);
//        }

//        // JUMP (Durum = 2)
//        if (!YerdeMi() && rb.linearVelocity.y > 0.1f)
//        {
//            animator.SetInteger("Durum", 2);
//        }

//        // FALL (Durum = 3)
//        if (!YerdeMi() && rb.linearVelocity.y < -0.1f)
//        {
//            animator.SetInteger("Durum", 3);
//        }
//    }

//    // ÖNEMLİ: Eğer kodunun en üstünde yoksa, şu satırı kodun en tepesine (using UnityEngine; olan yere) ekle:
//    // using UnityEngine.SceneManagement;

//    // BU FONKSİYONU KODUN İÇİNDE BOŞ BİR YERE YAPIŞTIR:
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // Karakterimiz 'Engel' etiketli katı bir buzula çarptı mı?
//        if (collision.gameObject.CompareTag("Tuzak"))
//        {

//            SceneManager.LoadScene("URP2DSceneTemplate");
//            // Çarptıysa, aktif olan sahneyi en baştan tertemiz yükle!
//            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//        }
//    }
//}


using UnityEngine;
using UnityEngine.SceneManagement;

public class karakter_hareket : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float yatayHiz = 50f;
    [SerializeField] float dikeyHiz = 90f;
    BoxCollider2D kutu;
    [SerializeField] LayerMask ziplamaKatmani;
    Animator animasyonDurumlari;
    [SerializeField] AudioSource ses;

    SpriteRenderer goruntu;

    // --- SENİN İÇİN EKLEDİĞİMİZ YENİ DEĞİŞKEN ---
    // Müfettiş (Inspector) panelinden oyunun en başındaki doğma noktasını buraya sürükleyeceğiz.
    [SerializeField] private Transform enBaslangicNoktasi;

    private bool YerdeMi()
    {
        return Physics2D.BoxCast(kutu.bounds.center, kutu.bounds.size, 0f, Vector2.down, 0.2f, ziplamaKatmani);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kutu = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        goruntu = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Zıplama
        if (Input.GetButtonDown("Jump") && YerdeMi())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, dikeyHiz);
            ses.Play();
        }

        // Yatay hareket
        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0)
        {
            rb.linearVelocity = new Vector2(xInput * yatayHiz, rb.linearVelocity.y);
            goruntu.flipX = xInput < 0;
        }

        // --- ANİMASYON KONTROLLERİ ---

        // RUN (Durum = 1)
        if (Mathf.Abs(rb.linearVelocity.x) > 0.5f && YerdeMi())
        {
            animator.SetInteger("Durum", 1);
        }

        // IDLE (Durum = 0)
        if (Mathf.Abs(rb.linearVelocity.x) <= 0.5f && YerdeMi())
        {
            animator.SetInteger("Durum", 0);
        }

        // JUMP (Durum = 2)
        if (!YerdeMi() && rb.linearVelocity.y > 0.1f)
        {
            animator.SetInteger("Durum", 2);
        }

        // FALL (Durum = 3)
        if (!YerdeMi() && rb.linearVelocity.y < -0.1f)
        {
            animator.SetInteger("Durum", 3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Karakterimiz 'Tuzak' etiketli katı bir buzula çarptı mı?
        if (collision.gameObject.CompareTag("Tuzak"))
        {
            // Eğer Unity üzerinden bir başlangıç noktası belirlediysek karakteri oraya ışınla!
            if (enBaslangicNoktasi != null)
            {
                transform.position = enBaslangicNoktasi.position;

                // Karakter ışınlandığında hızı sıfırlansın ki tuzağa çarpma hızıyla fırlamaya devam etmesin
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                // Eğer noktayı bağlamayı unutursan hata vermesin diye eski sahnene dönme kodun yedekte kalıyor knk:
                SceneManager.LoadScene("URP2DSceneTemplate");
            }
        }
    }
}


//using UnityEngine;

//public class karakter_hareket : MonoBehaviour
//{
//    Rigidbody2D rb;
//    Animator animator;
//    BoxCollider2D kutu;
//    SpriteRenderer goruntu;

//    [SerializeField] float yatayHiz = 8f; // Hız mantıklı bir seviyeye çekildi
//    [SerializeField] float dikeyHiz = 12f; // Zıplama gücü dengelendi
//    [SerializeField] LayerMask ziplamaKatmani;
//    [SerializeField] AudioSource ses;

//    private bool YerdeMi()
//    {
//        // BoxCast mesafesini biraz daha hassaslaştırdık
//        return Physics2D.BoxCast(kutu.bounds.center, kutu.bounds.size, 0f, Vector2.down, 0.1f, ziplamaKatmani);
//    }

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        kutu = GetComponent<BoxCollider2D>();
//        animator = GetComponent<Animator>();
//        goruntu = GetComponent<SpriteRenderer>();
//    }

//    void Update()
//    {
//        // Yatay hareket girdisi
//        float xInput = Input.GetAxisRaw("Horizontal");

//        // HAREKET VE HIZ SIFIRLAMA MANTIĞI
//        if (xInput != 0)
//        {
//            rb.linearVelocity = new Vector2(xInput * yatayHiz, rb.linearVelocity.y);
//            goruntu.flipX = xInput < 0;
//        }
//        else
//        {
//            // Elini tuştan çektiğinde karakterin kaymasını engeller, hızı tamamen sıfırlar!
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//        }

//        // ZIPLAMA
//        if (Input.GetButtonDown("Jump") && YerdeMi())
//        {
//            rb.linearVelocity = new Vector2(rb.linearVelocity.x, dikeyHiz);
//            if (ses != null) ses.Play();
//        }

//        // --- ANİMASYON KONTROLLERİ ---
//        AnimasyonlariGuncelle();
//    }

//    void AnimasyonlariGuncelle()
//    {
//        // RUN (Durum = 1) -> Yatayda hız varsa ve yerdeyse
//        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && YerdeMi())
//        {
//            animator.SetInteger("Durum", 1);
//        }
//        // IDLE (Durum = 0) -> Yatayda duruyorsa ve yerdeyse
//        else if (Mathf.Abs(rb.linearVelocity.x) <= 0.1f && YerdeMi())
//        {
//            animator.SetInteger("Durum", 0);
//        }

//        // HAVADAYKEN (Yerde değilse)
//        if (!YerdeMi())
//        {
//            if (rb.linearVelocity.y > 0.1f)
//            {
//                // JUMP (Durum = 2)
//                animator.SetInteger("Durum", 2);
//            }
//            else if (rb.linearVelocity.y < -0.1f)
//            {
//                // FALL (Durum = 3)
//                animator.SetInteger("Durum", 3);
//            }
//        }
//    }
//}