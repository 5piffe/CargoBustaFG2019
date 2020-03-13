using UnityEngine;
using UnityEngine.Assertions;

public class AtmosphereGradient : MonoBehaviour
{
	[SerializeField]
	private GameObject player = null;
	[SerializeField]
	private SpriteRenderer spriteRenderer = null;

	void Start()
	{
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();

		Assert.IsNotNull(spriteRenderer, "Could not find spriteRenderer");
		Assert.IsNotNull(player, "Player not attached");
	}

	void Update()
	{
		Color c = spriteRenderer.color;
		if (player)
		{
			float newA = (255f - player.transform.position.y * 5) / 255f;
			if (newA > 1f)
				newA = 1f;
			spriteRenderer.color = new Color(c.r, c.g, c.b, newA);
		}
	}
}