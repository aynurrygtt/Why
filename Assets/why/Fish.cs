using UnityEngine;
using System;


using UnityEngine.UI;
using TMPro;

public class Meyve : MonoBehaviour
{
    [SerializeField] TMP_Text textMashProMetin;
    [SerializeField] Text legacyTextMetin;
    int skor = 0;
   // [SerializeField] AudioSource ses;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        legacyTextMetin.text = "Skor = " + skor;
        textMashProMetin.text = Convert.ToString(DateTime.Now);


    }

    // Update is called once per frame
    void Update()
    {
        legacyTextMetin.text = "Skor = " + skor;
        textMashProMetin.text = Convert.ToString(DateTime.Now);
    }

    private void OnTriggerEnter2D(Collider2D collision) // colideer alýyor nesneyi deðil 
    {
        if (collision.gameObject.tag == "Toplanabilir")
        {
             // ses.Play();
            Destroy(collision.gameObject);   // nesneyi yok ettik 
            skor++;
        }
    }
}
