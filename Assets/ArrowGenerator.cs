/*
 * ȭ�� ������Ʈ�� 1�ʿ� �� ���� �����ϴ� �˰���
 * Update �޼ҵ�� �����Ӹ��� ����ǰ� �� �����Ӱ� ���� ������ ������ �ð� ���̴� Time.deltaTime�� ����
 * �����Ӱ� ������ ������ �ð� ���̸� �볪�� ��(delta����)�� ������(�հ�) 1�� �̻��� �Ǹ� �볪�� ���� ���
 * �볪�� ���� ���� ������ 1�ʿ� �� ���� ȭ���� ������
 * Instantiate �޼ҵ�
 * ������ �����ϴ� ���߿� ���� ������Ʈ�� ������ �� ����
 * ȭ�� �������� �̿��Ͽ�, ȭ�� �ν��Ͻ��� �����ϴ� �޼ҵ�
 * Random.Range �޼ҵ� : ���� ���� ���� ������ �� �ִ� ���
 * Random Ŭ������ ���� �䱸�Ǵ� �پ��� Ÿ���� ���� ���� ���� ������ �� �ִ� ����� ����
 * ����ڰ� ������ �ּҰ��� �ִ밪 ������ ������ ���ڸ� ������
 * ù ��° �Ű��������� ũ�ų� ����, �� ��° �Ű��������� ���� �������� ������ ���� �����ϰ� ��ȯ
 */


using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    /*
     * ���ʷ����� ��ũ���� ������ ���� ���
     * arrowPrefab ������ ������ ��ü�� �����ϱ� ���ؼ� public ���� ������
     * ������� ���� �� public���� �����ϸ� Inspector â���� Prefab ���赵 ������ �� �ֵ��� ����
     * ȭ�� �뷮 ������ ���ؼ� �����(���ʷ����ͽ�ũ��Ʈ)�� �Ѱ� �� Prefab ���赵�� �Ѱ� �־�� ��
     */
    public GameObject gArrowPrefab = null; //ȭ�� Prefab�� ���� �������Ʈ ���� ����

    GameObject gArrowInstance = null; //ȭ�� �ν��Ͻ� ���� ����

    float fArrowCreateSpan = 1.0f;   //ȭ�� ���� ���� : ȭ���� 1�ʸ��� ���� ����
    float fDeltaTime = 0.0f;        //�� �����Ӱ� ���� ������ ������ �ð� ���̸� �����ϴ� ����

    int nArrowPositionRange = 0;    //ȭ���� X��ǥ Range ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Update �޼ҵ�� �����Ӹ��� ����ǰ� �� �����Ӱ� ���� ������ ������ �ð� ������ Time.deltaTime�� ���Ե�
         * Time.deltaTime�� �� ������ �� �����ϴ� �ð��� ���ϴµ�, ���� float ���·� ��ȯ�ϰ� ������ �ʸ� �����
         * ��, �����Ӱ� ������ ������ �ð� ���̸� fDeltaTime ������ ����
         */
        fDeltaTime += Time.deltaTime;

        /*
         * ȭ���� 1��(fArrowCreateSpan = 1.0f)���� �� ���� ����
         * �����Ӵ� ���� �ð��� 1�ʰ� ������, ȭ�� ����
         */
        if(fDeltaTime > fArrowCreateSpan)
        {
            fDeltaTime = 0.0f; //�����Ӱ� ������ ������ �ð� ���� ���� ���� �ʱ�ȭ

            /*
             * Instantiate �޼ҵ� : ȭ�� �������� �̿��Ͽ�, ȭ�� �ν��Ͻ��� �����ϴ� �޼ҵ�
             * �Ű������� �������� �����ϸ�, ��ȯ������ ������ �ν��Ͻ��� �����ش�.
             * Instantiate �޼ҵ带 ����ϸ� ������ �����ϴ� ���߿� ���ӿ�����Ʈ�� ������ �� ����
             * RPG �����̶�� ������ ������, ĳ����, ��� �� ���͵��� ��� �̸� ����� ���� �� ������?
             * �׷��Ƿ� ���ӿ�����Ʈ�� �������� ����
             * Instantiate(GameObejct original, Vector3 position, Quaternion rotation)
             * GameObejct original : �����ϰ��� �ϴ� ���ӿ�����Ʈ��, ���� ���� �ִ� ���ӿ�����Ʈ�� Prefab���� ����� ��ü�� �ǹ���
             * Vector3 position : Vector3���� ������ ��ġ�� ������
             * Quaternion rotation : ������ ���ӿ�����Ʈ�� ȸ������ ����
             */
            gArrowInstance = Instantiate(gArrowPrefab);

            /*
             * Random Ŭ������ ���� �䱸�Ǵ� �پ��� Ÿ���� ���� ���� ������ �� �ִ� ����� ����
             * Random.Range �޼ҵ� : ����ڰ� ������ �ּҰ��� �ִ밪 ������ ������ ���ڸ� ������
             * ������ �ּҰ��� �ִ밪�� �������� �Ǽ������� ���� ���� �Ǵ� �Ǽ��� ��ȯ��
             * ù ��° �Ű��������� ũ�ų� ����, �� ���� �Ű��������� ���� �������� ������ ���� ������ ��ȯ
             * ȭ���� X��ǥ�� -6 6 ���̿� �ұ�Ģ�ϰ� ��ġ
             */
            nArrowPositionRange = Random.Range(-6, 7);

            gArrowInstance.transform.position = new Vector3(nArrowPositionRange, 7, 0);
        }
    }
}
