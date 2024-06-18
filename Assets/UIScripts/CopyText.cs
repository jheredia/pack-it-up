using TMPro;
using UnityEngine;

public class CopyText : MonoBehaviour
{
    public TMP_Text textToCopy;
    TMP_Text textMeshPro;

    private void OnEnable()
    {
        textMeshPro = GetComponent<TMP_Text>();    
    }
    void Update()
    {
        if(textMeshPro == null)
        {
            Debug.Log("text mesh pro component null");
        }
        textMeshPro.text = textToCopy.text;
    }
}
