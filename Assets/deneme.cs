using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Top Layer"), 1f);
            animator.SetLayerWeight(animator.GetLayerIndex("Bottom Layer"), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Eğer Space tuşuna basıldıysa
        {
            // Top layer ve bottom layer'ın weight'lerini sıfırla
            animator.SetLayerWeight(animator.GetLayerIndex("Top Layer"), 0f);
            animator.SetLayerWeight(animator.GetLayerIndex("Bottom Layer"), 0f);
        }
    }
}
