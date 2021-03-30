using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
[RequireComponent(typeof(Rigidbody2D))]
public class SpiralController : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    // public SpriteRenderer spriteRenderer;
    private Vector2 worldPosition;
    private SkillTreeProperties skilltreeproperties;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Debug.Log("index" + body.transform.GetSiblingIndex());
        body.gravityScale = 1;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if (Input.GetMouseButton(0))
        {
            body.mass = skilltreeproperties.s_mass_during_click;
            body.drag = skilltreeproperties.s_drag;
            // body.mass = 2;
            // body.drag = 11;

            body.AddForce((worldPosition - new Vector2(transform.position.x, transform.position.y)) * speed);
        }
        else
        {
            body.drag = 0;
            body.mass = skilltreeproperties.s_mass_After_click;
            // body.mass = 8;
        }



    }

    public void UpdateSkillTreePropertiesObject(SkillTreeProperties skillTreeProperties)
    {
        this.skilltreeproperties = skillTreeProperties;
    }
}
