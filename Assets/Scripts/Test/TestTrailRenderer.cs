using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrailRenderer : MonoBehaviour
{
    public GameObject Spiral;
    public bool held = false;
    Vector2 prevWorldPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            if (worldPosition != prevWorldPosition)
            {
                transform.localPosition = Vector3.zero;
                prevWorldPosition = worldPosition;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                transform.Rotate(Vector3.zero, 360 * Time.deltaTime);
            }

            // {
            //     held = true;
            //     Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //     Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            //     SpiralController controller = Instantiate(Spiral, new Vector3(worldPosition.x, worldPosition.y, 1), Quaternion.identity).GetComponent<SpiralController>();

        }
    }
}
