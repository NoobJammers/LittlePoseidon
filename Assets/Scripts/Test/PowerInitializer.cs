using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInitializer : MonoBehaviour
{
    public GameObject Spiral;
    public bool held = false;
    private SkillTreeData skillTreeData;
    // Start is called before the first frame update

    void Awake()
    {
        skillTreeData = GameObject.FindObjectOfType<SkillTreeData>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !held)
        {
            held = true;
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            SpiralController controller = Instantiate(Spiral, new Vector3(worldPosition.x, worldPosition.y, 1), Quaternion.identity).GetComponent<SpiralController>();
            controller.UpdateSkillTreePropertiesObject(skillTreeData.GetPropertiesObject("SkillTreeSpiral"));
        }
        else
        {

        }
    }
}
