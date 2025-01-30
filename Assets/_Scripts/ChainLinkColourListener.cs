using System;
using System.Collections;
using UnityEngine;
using Colour = UnityEngine.Color;

public class ChainLinkColourListener : MonoBehaviour
{
    [SerializeField] private ChainEventChannel chainEventChannel;
    
    private MeshRenderer[] _linkMeshRenderers;

    private bool _inDangerZone = true;

    private void OnEnable() => chainEventChannel.OnChainInDangerZone += SetChainInDangerZone;
    private void OnDisable() => chainEventChannel.OnChainInDangerZone -= SetChainInDangerZone;
    
    private void Awake()
    {
        // Get the child count of this chain link. 
        int childCount = transform.childCount;
        
        // Setup the link mesh renderers array.
        _linkMeshRenderers = new MeshRenderer[childCount];

        // Get all of the link mesh renderers. This all gets completed in Awake to save performance.
        for (int i = 0; i < childCount; i++)
        {
            _linkMeshRenderers[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        }
    }

    // Iterates through the cached link mesh renderers and starts a colour change coroutine. 
    private void SetChainInDangerZone(bool inDangerZone)
    {
        _inDangerZone = inDangerZone;
        
        StartLinkColourChange(inDangerZone ? Colour.red : Colour.white, inDangerZone);
    }
    
    private void StartLinkColourChange(Colour linkColour, bool pingPong)
    {
        foreach (MeshRenderer linkMeshRenderer in _linkMeshRenderers)
        {
            StartCoroutine(PingPongLinkColour(linkMeshRenderer, linkColour, pingPong));
        }
    }

    // Handles the colour change for the given link mesh renderer.
    private IEnumerator PingPongLinkColour(MeshRenderer linkMeshRenderer, Colour targetColour, bool shouldPingPong)
    {
        Colour originalColour = linkMeshRenderer.material.color;

        if (!_inDangerZone)
        {
            // This could be smoothed out too, but it will do for now. 
            linkMeshRenderer.material.color = targetColour;
            yield return null;
        }
        else
        {
            // Starting from 0 as the time variable for Mathf.PingPong ensures the lerpValue starts at 0 also. This
            // makes the colour change smooth, rather than immediately jumping to a different colour value. 
            float time = 0f; 
            
            while (_inDangerZone)
            {
                float lerpValue = Mathf.PingPong(time, 1f);
                linkMeshRenderer.material.color = Colour.Lerp(originalColour, targetColour, lerpValue);
                time += Time.deltaTime * 3f;
                yield return null; 
            }
        }
    }
}