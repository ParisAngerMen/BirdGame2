using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthVisual : MonoBehaviour
{
    [Header("Heart Sprites")]
    [SerializeField] private Sprite heartFull;   // Full heart
    [SerializeField] private Sprite heartHalf;   // Half heart
    [SerializeField] private Sprite heartEmpty;   // Empty heart

    [Header("Layout Settings")]
    [SerializeField] private Vector2 heartSize = new Vector2(30f, 30f);
    [SerializeField] private float heartSpacing = 35f;
    [SerializeField] private Vector2 startPosition = new Vector2(20f, -20f);

    private List<HeartImage> heartImageList;
    private float maxHealth;    // e.g. 10 = 5 full hearts (each heart = 2 HP)
    private float currentHealth;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }

    /// <summary>
    /// Call this to initialize the heart display.
    /// Each heart = 2 HP. So maxHealth of 10 = 5 hearts.
    /// </summary>
    public void SetupHearts(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;

        // Clear any existing hearts
        ClearHearts();

        // Create a heart for every 2 HP
        int heartCount = Mathf.CeilToInt(maxHealth / 2f);

        for (int i = 0; i < heartCount; i++)
        {
            Vector2 position = startPosition + new Vector2(i * heartSpacing, 0f);
            CreateHeartImage(position);
        }

        RefreshHeartVisuals();
    }

    /// <summary>
    /// Call this whenever health changes.
    /// </summary>
    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        RefreshHeartVisuals();
    }

    /// <summary>
    /// Convenience methods for damage/heal.
    /// </summary>
    public void TakeDamage(int amount)
    {
        SetHealth(currentHealth - amount);
    }

    public void Heal(int amount)
    {
        SetHealth(currentHealth + amount);
    }

    /// <summary>
    /// Updates every heart sprite based on currentHealth.
    /// </summary>
    private void RefreshHeartVisuals()
    {
        for (int i = 0; i < heartImageList.Count; i++)
        {
            // Each heart index represents 2 HP
            float hpForThisHeart = currentHealth - (i * 2);

            if (hpForThisHeart >= 2)
            {
                heartImageList[i].SetState(HeartState.Full, heartFull);
            }
            else if (hpForThisHeart == 1)
            {
                heartImageList[i].SetState(HeartState.Half, heartHalf);
            }
            else
            {
                heartImageList[i].SetState(HeartState.Empty, heartEmpty);
            }
        }
    }

    /// <summary>
    /// Creates a single heart UI Image at the given anchored position.
    /// </summary>
    private void CreateHeartImage(Vector2 anchoredPos)
    {
        GameObject heartGO = new GameObject("Heart", typeof(Image));

        // Parent to this transform (should be on a Canvas)
        heartGO.transform.SetParent(transform, false);

        RectTransform rect = heartGO.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 1f);   // top-left anchor
        rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = anchoredPos;
        rect.sizeDelta = heartSize;

        Image imageComponent = heartGO.GetComponent<Image>();
        imageComponent.sprite = heartFull;
        imageComponent.raycastTarget = false;     // no click blocking

        HeartImage heartImage = new HeartImage(imageComponent);
        heartImageList.Add(heartImage);
    }

    /// <summary>
    /// Destroys all existing heart GameObjects and clears the list.
    /// </summary>
    private void ClearHearts()
    {
        foreach (HeartImage heart in heartImageList)
        {
            if (heart.GetImageComponent() != null)
            {
                Destroy(heart.GetImageComponent().gameObject);
            }
        }
        heartImageList.Clear();
    }

    // ─────────────────────────────────────────────
    //  INNER CLASSES / ENUMS
    // ─────────────────────────────────────────────

    public enum HeartState
    {
        Full,
        Half,
        Empty
    }

    public class HeartImage
    {
        private Image heartImage;
        private HeartState currentState;

        public HeartImage(Image image)
        {
            this.heartImage = image;
            this.currentState = HeartState.Full;
        }

        public void SetState(HeartState state, Sprite sprite)
        {
            currentState = state;
            heartImage.sprite = sprite;
        }

        public HeartState GetState()
        {
            return currentState;
        }

        public Image GetImageComponent()
        {
            return heartImage;
        }
    }
}