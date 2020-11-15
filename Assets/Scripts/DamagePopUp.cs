using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagePopUp : MonoBehaviour
{  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateDamageText(Color color, string text, Transform parent)
    {
        var tempTextObject = DataStorage.DamagePopUpPool.GetObject();

        tempTextObject.transform.position = parent.position;

        TextMeshPro tempText = tempTextObject.gameObject.GetComponentInChildren<TextMeshPro>();

        tempText.color = color;
        tempText.text = text;

        tempTextObject.gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        DataStorage.DamagePopUpPool.ReturnToPool(gameObject.GetComponentInParent<DamagePopUp>());
    }


}
