using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//반드시 필요하기에 스크립트 추가할 때 컴포넌트 추가가 되며, 삭제할 수 없다.
//[RequireComponent(typeof(VideoPlayer))]

//영상을 끄고 키는 기능만 담겨져 있다.
public class BScreen : MonoBehaviour
{
    private VideoPlayer vp = null;

    private void Awake()
    {
        //동적으로 컴포넌트 추가
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

    //플레이어 거리를 측정해서 가까울 때만 영상 재생.
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
