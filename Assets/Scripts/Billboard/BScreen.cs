using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//�ݵ�� �ʿ��ϱ⿡ ��ũ��Ʈ �߰��� �� ������Ʈ �߰��� �Ǹ�, ������ �� ����.
//[RequireComponent(typeof(VideoPlayer))]

//������ ���� Ű�� ��ɸ� ����� �ִ�.
public class BScreen : MonoBehaviour
{
    private VideoPlayer vp = null;

    private void Awake()
    {
        //�������� ������Ʈ �߰�
        vp = GetComponent<VideoPlayer>();
        if (vp == null)
        {
            vp = gameObject.AddComponent<VideoPlayer>();
        }

        vp.playOnAwake = true;
        vp.isLooping = true;


        
    }

    public void SetVideoClip(VideoClip _clip)
    {
        vp.clip = _clip;
    }

    //�÷��̾� �Ÿ��� �����ؼ� ����� ���� ���� ���.
    public void Play()
    {
        if (vp.isPlaying) return;
        vp.Play();
    }

    public void Pause()
    {
        if (vp.isPaused) return;
        vp.Pause();
    }


}
