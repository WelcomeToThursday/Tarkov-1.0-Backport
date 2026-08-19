using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace EFT.UI
{
    public class StreamingVideoPlayer : MonoBehaviour
    {
        [SerializeField] private string streamingAssetsPath;
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private string materialProperty = "_EmissionMap"; // Standard supports this

        private void Awake()
        {
            if (videoPlayer == null)
                videoPlayer = GetComponent<VideoPlayer>();

            if (targetRenderer == null)
                targetRenderer = GetComponent<Renderer>() ?? GetComponentInChildren<Renderer>();
        }

        private void Start()
        {
            if (videoPlayer == null || string.IsNullOrEmpty(streamingAssetsPath))
            {
                Debug.LogError("[StreamingVideoPlayer] Missing VideoPlayer or path.");
                return;
            }

            if (targetRenderer == null)
            {
                Debug.LogError("[StreamingVideoPlayer] No targetRenderer assigned.");
                return;
            }

            string assemblyDir = Path.GetDirectoryName(typeof(StreamingVideoPlayer).Assembly.Location);
            string fullPath = Path.Combine(assemblyDir, streamingAssetsPath);

            Debug.Log($"[StreamingVideoPlayer] Full path: {fullPath}");
            Debug.Log($"[StreamingVideoPlayer] Exists: {File.Exists(fullPath)}");

            videoPlayer.errorReceived += OnError;
            videoPlayer.prepareCompleted += OnPrepared;
            videoPlayer.started += OnStarted;

            videoPlayer.source = VideoSource.Url;
            videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
            videoPlayer.targetMaterialRenderer = targetRenderer;
            videoPlayer.targetMaterialProperty = materialProperty;

            videoPlayer.playOnAwake = true;
            videoPlayer.isLooping = true;
            videoPlayer.waitForFirstFrame = true;
            videoPlayer.skipOnDrop = true;
            videoPlayer.url = fullPath;

            Debug.Log($"[StreamingVideoPlayer] RenderMode: {videoPlayer.renderMode}");
            Debug.Log($"[StreamingVideoPlayer] TargetMaterialRenderer: {videoPlayer.targetMaterialRenderer}");
            Debug.Log($"[StreamingVideoPlayer] TargetMaterialProperty: {videoPlayer.targetMaterialProperty}");

            videoPlayer.Prepare();
        }

        private void OnPrepared(VideoPlayer vp)
        {
            Debug.Log("[StreamingVideoPlayer] Prepared successfully.");
            Debug.Log($"[StreamingVideoPlayer] width={vp.width}, height={vp.height}, frameCount={vp.frameCount}");
            vp.Play();
        }

        private void OnStarted(VideoPlayer vp)
        {
            Debug.Log("[StreamingVideoPlayer] Playback started.");
        }

        private void OnError(VideoPlayer vp, string message)
        {
            Debug.LogError($"[StreamingVideoPlayer] Video error: {message}");
        }
    }
}
