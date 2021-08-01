using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputArrow : MonoBehaviour
{
    [SerializeField] private Field _field;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _field.Move(Vector2.up);
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _field.Move(Vector2.down);
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _field.Move(Vector2.right);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _field.Move(Vector2.left);
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
