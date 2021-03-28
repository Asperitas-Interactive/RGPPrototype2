using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableCount : MonoBehaviour
{
    public bool counted = false;
    [SerializeField]
    private Sprite Unchecked;
    [SerializeField]
    private Sprite Checked;
    private Image UIImage;
    // Start is called before the first frame update
    void Start()
    {
        UIImage = GetComponent<Image>();
        UIImage.sprite = Unchecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCollection()
    {
        UIImage.sprite = Checked;
        counted = true;
    }
}
