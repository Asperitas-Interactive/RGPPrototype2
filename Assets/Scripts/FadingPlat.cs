using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlat : MonoBehaviour
{
    [SerializeField]
    private float maxFallTimer = 2.0f;
    private bool fadestart = false;

    private float fadeTimer = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fadestart == true)
        {
            fadeTimer -= 1 * Time.deltaTime;
            if (fadeTimer <= 0.0f)
            {
                Fade();
            }
        }
    }


    private void Fade()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x - 1 * Time.deltaTime, 0.001f, 100.0f),
            transform.localScale.y,
            Mathf.Clamp(transform.localScale.z - 1 * Time.deltaTime, 0.001f, 100.0f));
    }
}
