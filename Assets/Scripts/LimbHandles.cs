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
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(bodyParts[i].transform.position);
            Vector2 rectPoint = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(((RectTransform)transform), screenPoint, null, out rectPoint);
            ((RectTransform)handles[i].transform).anchoredPosition = rectPoint;
        }
    }
}
