using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header ("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;



    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();
     
        //Kiểm tra người chơi đang ở đâu trong 4 hướng
        for(int i = 0; i < directions.Length; i ++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;//Hướng phải
        directions[1] = -transform.right * range;//Hướng trái
        directions[2] = transform.up * range;//HƯớng trên
        directions[3] = -transform.up * range;//hướng dưới
    }
    private void Stop()
    {
        destination = transform.position;//Sét vị trí ban đầu và k di chuyển
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
            Stop();//Dừng lại khi va chạm
    }
}
