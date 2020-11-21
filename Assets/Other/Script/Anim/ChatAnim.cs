using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAnim : MonoBehaviour
{
    private Animator _anim;
    int count = 0;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetMouseButton(1))
        {
            _anim.SetBool("push", false);
        }

        #region Mouse On Object
        //RaycastHit hit;
        //int mask = 1 << 15;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, 100f , mask))
        //{
        //  print("Yes");
        //  if (hit.transform != null)
        //  {
        //      PrintName(hit.transform.gameObject);
        //    }
        //  }
        // else
        // {
        //      print("NO");
        // }
        #endregion
    }

    public void ChatOnAnim()
    {
        count += 1;
        if (count == 1)
        {
            _anim.SetBool("push", true);
        } else if (count == 2)
        {
            _anim.SetBool("push", false);
            count = 0;
        }
    }
}
