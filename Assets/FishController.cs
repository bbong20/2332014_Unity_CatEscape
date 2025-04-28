/*
 * ����Ⱑ ������ �Ʒ��� 3�ʿ� �ϳ��� �������� ���
 * ����Ⱑ ����ȭ�� ������ ������ ����� ������Ʈ�� �Ҹ��Ű�� ���
 */

using UnityEngine;

public class FishController : MonoBehaviour
{
    GameObject gPlayer = null; //�÷��̾� ������Ʈ ����
    GameObject gDirector = null; //���� ������Ʈ ����

    Vector2 vFishCirclePoint = Vector2.zero;    //����⸦ �ѷ��� ���� �߽� ��ǥ
    Vector2 vPlayerCirclePoint = Vector2.zero;      //�÷��̾ �ѷ��� ���� �߽� ��ǥ
    Vector2 vFishPlayerDistance = Vector2.zero;    //����⿡�� �÷��̾������ ���Ͱ�

    float fFishRadius = 0.5f;           //����� ���� ������
    float fPlayerRadius = 1.0f;         //�÷��̾� ���� ������
    float fFishPlayerDistance = 0.0f;   //������� �߽����� ���� �÷��̾� �߽ɱ����� �Ÿ� ����

    //int nFishCount = 0; //���� ����� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gPlayer = GameObject.Find("player"); //�÷��̾� ������Ʈ ã��
        gDirector = GameObject.Find("GameDirector"); //���ӵ��� ������Ʈ ã��
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, -0.1f, 0.0f); //����Ⱑ �Ʒ� �������� 0.1��ŭ �̵��Ѵ�.

        if (transform.position.y < -5.0f) //����� ������Ʈ�� y��ǥ -5.0f�� ���� �Ʒ��� ���ٸ� ������Ʈ�� �ı�
        {
            Destroy(gameObject);
        }

        vFishCirclePoint = transform.position;                          //������� ��ġ ����
        vPlayerCirclePoint = gPlayer.transform.position;                //�÷��̾��� ��ġ ����
        vFishPlayerDistance = vFishCirclePoint - vPlayerCirclePoint;    //������ �÷��̾�� �Ÿ�

        fFishPlayerDistance = vFishPlayerDistance.magnitude;    //������ ���̸� ���ϴ� magnitude �޼ҵ带 ����Ͽ� �浹 ������ ���� �Ÿ��� �����Ѵ�.

        if(fFishPlayerDistance < fFishRadius + fPlayerRadius)   //������ �÷��̾� ������ �Ÿ� < ����� ������ + �÷��̾� ������ : �浹
        {
            gDirector.GetComponent<GameDirector>().f_UpdateFishAmountCount(); //����� ���� ī��Ʈ �޼ҵ� ȣ��

            Destroy(gameObject); //������Ʈ ����
        }
        
    }
}
