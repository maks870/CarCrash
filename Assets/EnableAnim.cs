using UnityEngine;

public class EnableAnim : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("enable", true);
    }

    private void OnDisable()
    {
        animator.SetBool("enable", false);
    }
}
