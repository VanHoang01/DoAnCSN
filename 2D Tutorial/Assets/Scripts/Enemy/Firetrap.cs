using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            if(active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //Chuyen mau de biet da kich hoat bay
        triggered = true;
        spriteRend.color = Color.red;

        //chờ độ trễ , kích hoạt bẫy, bật ảnh họa , màu trơ lại bình thường 
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;//Trở lại màu ban đầu
        active = true;
        anim.SetBool("activated", true);


        //hủy kích hoạt bẫy, tất cả trở lại ban đầu
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
