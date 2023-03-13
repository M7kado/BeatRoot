using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bettie : Clockable
{
    System.Random rand = new System.Random();

    private SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite attackSprite;
    [SerializeField] GameObject projectile;
    int nextAttack = 10;

    int count = 0;

    new private void Start()
    {
        base.Start();
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }

    public override void Action()
    {
        count = (++count) % sprites.Length;
        sr.sprite = sprites[count];

        if (Clock.Instance.Timer > nextAttack)
        {
            nextAttack += rand.Next(8, 15);
            sr.sprite = attackSprite;
            Attack();
        }
    }

    public void Attack()
    {
        GameObject tileObj = Instantiate(Resources.Load<GameObject>("Prefabs/Projectile"));
        tileObj.transform.position = new Vector2(MapManager.Instance.mapWidth - 1,
                                    rand.Next(MapManager.Instance.mapHeight)) + MapManager.Instance.renderOffset;
        tileObj.gameObject.SetActive(true);
    }
}
