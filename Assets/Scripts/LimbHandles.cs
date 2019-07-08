using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbHandles : MonoBehaviour
{
    GameObject[] bodyParts;
    GameObject[] handles;

    public GameObject handlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        bodyParts = FindObjectOfType<Dance>().bodyParts.ToArray();
        handles = new GameObject[bodyParts.Length];

        for (int i = 0; i < bodyParts.Length; i++)
        {
            handles[i] = Instantiate(handlePrefab, transform);
            if (bodyParts[i].GetComponent<BodyPart>().tier > 1)
            {
                handles[i].SetActive(false);
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

            //hacky as hell
            if (fingerDown)
            {
                handles[i].GetComponent<Image>().color = Color.red;
            }
            else { handles[i].GetComponent<Image>().color = Color.white; }
            fingerDown = false;
        }

    }
    /*
    public bool fingerDown = false;

    public void FingerDown()
    {
        fingerDown = true;
    }
    */
}
