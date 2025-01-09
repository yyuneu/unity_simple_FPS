using System.Collections;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 5.0f; // 탄피 등장 후 비활성화되는 시간
    [SerializeField] private float casingSpin = 1.0f; // 탄피가 회전하는 속력 계수
    [SerializeField] private AudioClip[] audioClips; // 탄피가 부딪혔을 때 재생되는 사운드
    [SerializeField] private float audioVolume = 0.5f; // 사운드 볼륨 (0 ~ 1)

    private Rigidbody rigidbody3D; // Rigidbody 컴포넌트
    private AudioSource audioSource; // AudioSource 컴포넌트
    private MemoryPool memoryPool; // 메모리 풀 참조

    public void Setup(MemoryPool pool, Vector3 direction)
    {
        rigidbody3D = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        memoryPool = pool;

        // 탄피 이동 속도와 회전 속도 설정
        rigidbody3D.linearVelocity = new Vector3(direction.x, 1.0f, direction.z);
        rigidbody3D.angularVelocity = new Vector3(
            Random.Range(-casingSpin, casingSpin),
            Random.Range(-casingSpin, casingSpin),
            Random.Range(-casingSpin, casingSpin)
        );

        // 탄피 자동 비활성화를 위한 코루틴 실행
        StartCoroutine(DeactivateAfterTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 배열이 비어있는지 확인하여 안전하게 처리
        if (audioClips == null || audioClips.Length == 0) return;

        // 여러 개의 탄피 사운드 중 임의의 사운드 선택
        int index = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[index], audioVolume); // 볼륨 설정 추가
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);
        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
