using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    public GameObject touchIndicator;
    public Canvas canvas;
    public Text text;

    List<GameObject> indicators = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(touch.position);
            text.text = "touch position: " + touch.position.ToString();

            if (indicators[0] == null)
            {
                indicators[0] = Instantiate(touchIndicator,canvas.transform);
            }
            ((RectTransform)indicators[0].transform).anchoredPosition = touch.position;

            /*
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10, 9))
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Material>().color =new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
            }
            */
        }
        else
        {
            text.text = null;
            foreach (GameObject obj in indicators)
            {
                Destroy(obj);
            }
        }
        
    }
}
