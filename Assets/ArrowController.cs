/*
 * ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� �� transform.Translate()
 * ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� �� Destroy()
 */

using UnityEditor;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    //������� ����
    GameObject gPlayer = null; //Player Object�� ������ GameObject ����, GameObject ������ �ʱ갪�� null
    GameObject gDirector = null; //���� ���� ������Ʈ ����

    Vector2 vArrowCirclePoint = Vector2.zero;       //ȭ���� �ѷ��� ���� �߽� ��ǥ
    Vector2 vPlayerCirclePoint = Vector2.zero;      //�÷��̾ �ѷ��� ���� �߽� ��ǥ
    Vector2 vArrowPlayerDistance = Vector2.zero;    //ȭ�쿡�� �÷��̾������ ���Ͱ�

    float fArrowRadius = 0.5f;  //ȭ�� ���� ������
    float fPlayerRadius = 1.0f; //�÷��̾� ���� ������

    float fArrowPlayerDistance = 0.0f; //ȭ���� �߽�(vArrowCirclePoint) ����
                                       //�÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)���� �Ÿ�

    float fArrowFallSpeed = 0.0f; //ȭ���� ���� �ӵ� ���� 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * �� �ȿ��� ������Ʈ�� ã�� �޼ҵ� : Find
         * Find �޼ҵ�� ������Ʈ �̸��� �μ��� �����ϰ�, �μ� �̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         * �÷��̾��� ��ǥ�� ���ϱ� ���ؼ� �÷��̾ �˻��Ͽ� ������Ʈ ������ ����
         * �� ������Ʈ ���ڿ� ����ϴ� ������Ʈ�� �� �ȿ��� ã�� �־�� ��
         */
        gPlayer = GameObject.Find("player");
        gDirector = GameObject.Find("GameDirector"); //���� ���� ������Ʈ�� ã�ƿ�
    }

    // Update is called once per frame
    void Update()
    {

        fArrowFallSpeed = gDirector.GetComponent<GameDirector>().fArrowFallSpeed; //���� ���� �� ����Ǵ� ȭ�� ���� �ӵ��� ȭ�� ��Ʈ�ѷ� ȭ�� ���� �ӵ� ������ ����

        /*
         * ȭ���� ������ �Ʒ��� 1�ʿ� �ϳ��� �������� ��� �� tranform.Translate()
         * Translate �޼ҵ� : ������Ʈ�� ���� ��ǥ���� �μ� �� ��ŭ �̵���Ű�� �޼ҵ�
         * Y��ǥ�� -0.1f�� �����ϸ� ������Ʈ�� ���ݾ� ������ �Ʒ��� �����δ�.
         * �����Ӹ��� ������� ���Ͻ�Ų��.
         */
        transform.Translate(0, -fArrowFallSpeed, 0); //�Ʒ� �������� fArrowFallSpeed��ŭ �̵�

        /*
         * ȭ���� ����ȭ�� ������ ������ ȭ�� ������Ʈ�� �Ҹ��Ű�� ��� �� Destroy()
         * ȭ�� ������ ���� ȭ�� �Ҹ��Ű��
         * ȭ���� ������ �θ� ȭ�� ������ ������ �ǰ�, ���� �������� ������ ��� ������
         * ȭ���� ������ �ʴ� ������ ��� �������� ��ǻ�� ���� ����� �ؾ��ϹǷ� �޸� ����
         * �޸𸮰� ������� �ʵ��� ȭ���� ȭ�� ������ ������ ������Ʈ�� �Ҹ���Ѿ� ��
         * Destroy �޼ҵ� : �Ű������� ������ ������Ʈ�� ����
         * �Ű������� �ڽ�(ȭ�� ������Ʈ)�� ����Ű�� gameObject ������ �����ϹǷ� ȭ����
         * ȭ�� ������ ������ ��, �ڱ� �ڽ��� �Ҹ��Ŵ
         */
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        /*
         * �浹���� : ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ���� �˰���
         * ȭ���� �߽�(vArrowCirclePoint)���� �÷��̾ �ѷ��� ���� �߽�(vPlayerCirclePoint)���� 
         * �Ÿ��� ��Ÿ��� ������ �̿��Ͽ� ���Ѵ�.
         * fArrowRadius : ȭ���� �ѷ��� ���� ������, fPlayerRadius : �÷��̾ �ѷ��� ���� ������
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance > fArrowRadius + fPlayerRadius : �浹���� ����
         * �� ���� �߽ɰ��� �Ÿ� fArrowPlayerDistance < fArrowRadius + fPlayerRadius : �浹��
         */

        vArrowCirclePoint = transform.position;
        vPlayerCirclePoint = gPlayer.transform.position;
        vArrowPlayerDistance = vArrowCirclePoint - vPlayerCirclePoint;

        /*
         * �� ���Ͱ��� ���̸� ���ϴ� �޼ҵ� : magnitude
         * �޼ҵ� ���� : public float Magnitude(Vector3 vector)
         * ���ʹ� ũ��� ������ ���� ������, ������(Initial Point)�� ����(Terminal Point)���� �����Ǹ�,
         * �� �� ������ �Ÿ��� �� ������ ũ�Ⱑ �ȴ�.
         * �Ϲ������� �������� ������ ����, �� ���� ������ �Ӹ���� �θ���.
         * ���ʹ� �������� ������ ��ġ�� ���� ����, �� ������ ũ��� ������ ���ٸ� ���� ���� ���ͷ� ����Ѵ�.
         * ���ʹ� ���� ��ġ�� ��Ÿ���� ��ġ ���͸� �̿��� ǥ���Ѵ�.
         * ȭ���� �߽ɺ��� �÷��̾ �ѷ��� ���� �߽ɱ����� �Ÿ�
         */
        fArrowPlayerDistance = vArrowPlayerDistance.magnitude;

        /*
         * �÷��̾ ȭ�쿡 �¾Ҵ��� ����, �� ȭ��� �÷��̾��� �浹 ����
         * ���� �߽� ��ǥ�� �ݰ��� ����� �浹 ����
         * r1 : ȭ���� �ѷ��� ���� ������, r2 : �÷��̾ �ѷ��� ���� ������, d : ȭ��� �߽ɿ��� �÷��̾�� �߽ɱ��� �Ÿ�
         * ���浹 : �� ���� �߽� �� �Ÿ� d�� (r1+r2)���� ũ�� �� ���� �浹���� ����(d > r1+r2)
         * �浹(fArrowPlayerDistance < (fArrowRadius + fPlayerRadius)) �̸� ȭ�� ������Ʈ �Ҹ�
         */
        if (fArrowPlayerDistance < fArrowRadius + fPlayerRadius)
        {
            /*
             * �÷��̾ ȭ�쿡 ������ ȭ�� ��Ʈ�ѷ����� ���� ��ũ��Ʈ�� f_DecreaseHp() �޼ҵ带 ȣ��
             * ��, ArrowController���� GameDirector ������Ʈ�� �ִ� f_DecreaseHp() �޼ҵ带 ȣ���ϱ� ������
             * Find �޼ҵ带 ã�Ƽ� GameDirector ������Ʈ�� ã�´�.
             */
            //GameObject gDirector = GameObject.Find("GameDirector");


            /*
             * GetComponent �޼ҵ带 ����� GameDirector ������Ʈ�� GameDirector ��ũ��Ʈ�� ���ϰ�,
             * f_DecreaseHp() �޼ҵ带 �����Ͽ�, ���� ��ũ���� �÷��̾�� ȭ���� �浹�ߴٰ� ����
             */
            gDirector.GetComponent<GameDirector>().f_DecreaseHp();
            Destroy(gameObject); //ȭ��� �÷��̾� �浹, ȭ�� ������Ʈ�� �Ҹ�
        }
    }

}
