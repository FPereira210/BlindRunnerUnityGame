// Script simples para clicar sobre objetos e ir criando um line renderer entre eles.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndConnect : MonoBehaviour
{
    [SerializeField] private Material lineMaterial;
    [SerializeField] private List<GameObject> objects = new List<GameObject>();    
    private GameObject firstObject;
    LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit) && hit.collider.tag == "Player")
            {
                if (firstObject == null)
                {
                    firstObject = hit.collider.gameObject;

                    lineRenderer = firstObject.AddComponent<LineRenderer>();
                    lineRenderer.startWidth = 0.2f;
                    lineRenderer.endWidth = 0.2f;
                    lineRenderer.material = lineMaterial;
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.cyan;
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, firstObject.transform.position);

                    objects.Add(firstObject);
                }
                else
                {
                    if (!objectWasClicked(hit.collider.gameObject.transform) && hit.collider.gameObject != firstObject)
                    {
                        objects.Add(hit.collider.gameObject);
                        lineRenderer.positionCount ++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, objects[objects.Count - 1].transform.position);
                    }
                    else if (hit.collider.gameObject == firstObject && objects.Count > 2)
                    {
                        print("Close the line draw.");
                        lineRenderer.loop = true;
                    }
                }
            }

        }
    }

    bool objectWasClicked(Transform t)
    {
        foreach (GameObject g in objects)
        {
            if (t == g.transform && g != firstObject)
            {
                print(g.name + " already was clicked.");
                return true;
            }
        }
        return false;
    }
}
