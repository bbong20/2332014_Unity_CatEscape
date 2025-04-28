/*
 * ����� ������Ʈ�� 5�ʿ� �� ���� �����ϴ� �˰��� 
 */

using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    /*
     * ���ʷ����� ��ũ���� ������ ���� ���
     * fishPrefab ������ ������ ��ü�� �����ϱ� ���ؼ� public ���� ������
     * ������� ���� �� public���� �����ϸ� Inspector â���� Prefab ���赵 ������ �� �ֵ��� ����
     * ����� �뷮 ������ ���ؼ� �����(���ʷ����ͽ�ũ��Ʈ)�� �Ѱ� �� Prefab ���赵�� �Ѱ� �־�� ��
     */
    public GameObject gFishPrefab = null; //����� ������ ������Ʈ ����
    GameObject gFishInstance = null;      //����� �ν��Ͻ� ����

    [SerializeField]                //private ���� ��ȿ
    float fFishCreateSpan = 2.0f;   //����� ���� ���� : ����⸦ �⺻ 2�ʸ��� ����
    

    float fDeltaTime = 0.0f;        //�� �����Ӱ� ���� ������ ������ �ð� ���̸� �����ϴ� ����
    int nFishPositionRange = 0;    //������� X��ǥ Range ���� ����

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
             * Instantiate �޼ҵ� : ����� �������� �̿��Ͽ�, ����� �ν��Ͻ��� �����ϴ� �޼ҵ�
             * �Ű������� �������� �����ϸ�, ��ȯ������ ������ �ν��Ͻ��� �����ش�.
             * Instantiate �޼ҵ带 ����ϸ� ������ �����ϴ� ���߿� ���ӿ�����Ʈ�� ������ �� ����
             * RPG �����̶�� ������ ������, ĳ����, ��� �� ���͵��� ��� �̸� ����� ���� �� ������?
             * �׷��Ƿ� ���ӿ�����Ʈ�� �������� ����
             * Instantiate(GameObejct original, Vector3 position, Quaternion rotation)
             * GameObejct original : �����ϰ��� �ϴ� ���ӿ�����Ʈ��, ���� ���� �ִ� ���ӿ�����Ʈ�� Prefab���� ����� ��ü�� �ǹ���
             * Vector3 position : Vector3���� ������ ��ġ�� ������
             * Quaternion rotation : ������ ���ӿ�����Ʈ�� ȸ������ ����
             */

        if (fDeltaTime > fFishCreateSpan)
        {
            fDeltaTime = 0.0f;

            gFishInstance = Instantiate(gFishPrefab);

            nFishPositionRange = Random.Range(-6, 7); // nFishPositionRange�� -6~7 ���� ������ �߻����� ����

            gFishInstance.transform.position = new Vector3(nFishPositionRange, 7, 0); //�߻��� ������ ���� ����� ��ǥ �̵�
        }
    }
}
