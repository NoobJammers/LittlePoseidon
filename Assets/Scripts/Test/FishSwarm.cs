using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwarm : MonoBehaviour
{ public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        Debug.Log("rect " + rend.sprite.rect);
        Debug.Log("rect " + rend.sprite.bounds);
        Debug.Log("rect " + rend.sprite.texture.width);
        Texture2D image = rend.sprite.texture;
    Debug.Log(image);
        rend.color = new Color(0, 0, 0, 0);
        int k = 0;
        // Iterate through it's pixels
        for (int i = 0; i < image.width; i++)
        {
            for (int j = 0; j < image.height; j++)
            {
                Color pixel = image.GetPixel(i, j);

                if (pixel == Color.black)
                {
                    k++;
                    if (k % 100 == 0)
                    {
                        var ratiox = 2 * i / rend.sprite.rect.width;//half the width as we are that's how extents work)
                        var ratioy = 2 * j / rend.sprite.rect.height;//half the height as we are that's how extents work)
                        float positionoffsetx = (1 - ratiox) * (rend.sprite.bounds.extents.x * gameObject.transform.localScale.x);
                        float positionoffsety = (1 - ratioy) * (rend.sprite.bounds.extents.y * gameObject.transform.localScale.y);

                        GameObject.Instantiate(prefab, new Vector3(transform.position.x - positionoffsetx, transform.position.y - positionoffsety, transform.position.z), Quaternion.identity);
                    }
                }
               
            }
        }
       
       
       
        
    }
    private void Update()
    {
      
    }


}
