using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squish : MonoBehaviour
{
    #region Squishy Base Values
    public float squishX;
    public float squishY;
    float orgSquishX;
    [SerializeField] float squishVel = 0.5f;
    [SerializeField] float squishVelMod = 0.1f;
    #endregion

    #region Squishy Mods
    public float len = 0.15f;
    public float lerpStep = 0.1f;
    [SerializeField] [Range(0.1f, 1f)] float speedMultiplier = 0.2f;
    #endregion
    bool mousedOver = false;

    public float LenghtDirX(float len, float dir) //dodaj si to v neki library
    {
        return len * Mathf.Cos(dir * Mathf.Deg2Rad);
    }
    void Start()
    {
        squishX = transform.localScale.x;

        squishY = transform.localScale.y;
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            squishX = 3;
        }


        PassiveSquishy();
    }

    void PassiveSquishy()
    {
        #region Squishy Scale   
        float msTime = Time.time * 1000;

        squishX = Mathf.Lerp(squishX, (1 + LenghtDirX(len, msTime * speedMultiplier)), lerpStep);
        squishY = 1 / squishX;
        Vector2 newScale = new Vector2(squishX, squishY);
        #endregion

        #region Squishy Rotation
        if (squishX > 1)
            squishVel = Mathf.Lerp(squishVel, squishVel - squishVelMod, 0.5f);
        else if (squishX < 1)
            squishVel = Mathf.Lerp(squishVel, squishVel + squishVelMod, 0.5f);

        Vector3 rotateTo = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, squishVel);
        transform.rotation = Quaternion.Euler(rotateTo);
        #endregion

        transform.localScale = newScale;

    }

    void SquashedSquishy()
    {
        squishX = 1;
    }

    private void OnMouseOver()
    {
        if (!mousedOver)
        {
            mousedOver = true;
            squishX *= 1.3f;
        }
    }

    private void OnMouseExit()
    {
        mousedOver = false;
    }

    private void OnMouseDown()
    {
        squishX = 1.8f;
    }


}
