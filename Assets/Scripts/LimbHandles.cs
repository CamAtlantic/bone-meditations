using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbHandles : MonoBehaviour
{
    BodyPart[] bodyParts;
    UIHandle[] handles;

    public GameObject handlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        bodyParts = Dance.danceScript.bodyParts.ToArray();
        handles = new UIHandle[bodyParts.Length];

        for (int i = 0; i < bodyParts.Length; i++)
        {
            handles[i] = Instantiate(handlePrefab, transform).GetComponent<UIHandle>();
            handles[i].linkedBodyPart = bodyParts[i];
            if (bodyParts[i].tier > 1)
            {
                handles[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //stick on body part
        for (int i = 0; i < bodyParts.Length; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(bodyParts[i].transform.position);
            Vector2 rectPoint = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(((RectTransform)transform), screenPoint, null, out rectPoint);
            ((RectTransform)handles[i].transform).anchoredPosition = rectPoint;
        }

        //Handle Anti-Overlap
        for (int i = 0; i < handles.Length; i++)
        {

            //loop through handles
            for (int j = 0; j < handles.Length; j++)
            {
                //skip this handle
                if (handles[j] != handles[i])
                {
                    //if overlap
                    if (handles[i].GetComponent<RectTransform>().rect.Overlaps(handles[j].GetComponent<RectTransform>().rect))
                    {
                        //get vector
                        Vector2 vector =
                            handles[j].GetComponent<RectTransform>().anchoredPosition -
                            handles[i].GetComponent<RectTransform>().anchoredPosition;
                        Debug.Log(vector);

                        handles[i].GetComponent<RectTransform>().anchoredPosition -= vector.normalized * 10;
                    }
                }
            }
            Vector3 anchoredPos = handles[i].GetComponent<RectTransform>().anchoredPosition;
            //Vector3 handleLinePos = Camera.main.ScreenToWorldPoint(new Vector3(anchoredPos.x, anchoredPos.y, Camera.main.nearClipPlane));
                
            //handles[i].lineRenderer.SetPosition(0, handleLinePos);
            //handles[i].lineRenderer.SetPosition(1, bodyParts[i].transform.position);

        }



    }
    
}
