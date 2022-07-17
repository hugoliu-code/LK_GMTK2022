using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    #region FMOD Variables
    public float distance = 0.1f;
    private string Material;
    #endregion

    void FixedUpate()
    {
        MaterialCheck();
        Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);
    }

    #region FMOD Functions
    void MaterialCheck()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, distance, 1 << 6);

        if (hit.collider)
        {
            if (hit.collider.tag == "Material: Carpet")
                Material = "Carpet";
            else if (hit.collider.tag == "Material: Marble")
                Material = "Marble";
            else if (hit.collider.tag == "Material: Wood")
                Material = "Wood";
        }
    }

    void PlayFootsteps(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
    #endregion
}
