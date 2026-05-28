using UnityEngine;

public class Dýken : MonoBehaviour
{
    [SerializeField] private GameObject[] noktalar;
    private int noktaIndisi = 0;
    [SerializeField] private float hiz = 2f;
    // Update is called once per frame void
    void Update()  // her kare çalýþýr 
    {
        if (Vector2.Distance(noktalar[noktaIndisi].transform.position, this.transform.position) < .1f)  // eger dogruysa çalýþtýr deðilse yok bu dogru deðilse hiç bir þey yapma  a v be arasýnda ki masafeye bakar.
        {
            noktaIndisi++;
            if (noktaIndisi >= noktalar.Length)
            {
                noktaIndisi = 0;   // döngü olmasý için 
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, noktalar[noktaIndisi].transform.position, Time.deltaTime * hiz);
        // asýl hareketi bu saðlar. / Devam et  //  baþlangýç noktasýndan þuanki ,                             önce ki kareden þimdi ki kareye geçen zaman          // ne kadar hýz ile gidicek.
    }

}
