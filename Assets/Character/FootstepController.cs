using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FootstepController : MonoBehaviour
{
    public AudioManager audioManager;
    public float raycastDistance = 1.0f;
    public float rayOriginOffset = 0.5f;
    public float footstepInterval = 0.5f;
    public float sprintFootstepInterval = 0.3f;
    public float jumpFootstepVolume = 1.5f;
    public float walkFootstepVolume = 1.0f;
    public float footstepCooldown = 0.1f; // Cooldown between footstep sounds
    public KeyCode sprintKey = KeyCode.LeftShift;

    private CharacterController characterController;
    private float footstepTimer;
    private bool hasJumped;
    private string currentGroundTag;
    private bool canPlayFootstep = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepTimer = footstepInterval;
        hasJumped = false;
        currentGroundTag = null;
    }

    private void Update()
    {
        bool isSprinting = Input.GetKey(sprintKey);
        float currentFootstepInterval = isSprinting ? sprintFootstepInterval : footstepInterval;

        if (characterController.isGrounded)
        {
            if (hasJumped)
            {
                PlayJumpLandingSound();
                hasJumped = false;
            }

            if (characterController.velocity.magnitude > 0.1f)
            {
                footstepTimer -= Time.deltaTime;

                if (footstepTimer <= 0)
                {
                    if (canPlayFootstep)
                    {
                        PlayFootstepSound(walkFootstepVolume);
                        footstepTimer = currentFootstepInterval;
                        canPlayFootstep = false;
                        Invoke("ResetFootstepCooldown", footstepCooldown);
                    }
                }
            }
        }
        else
        {
            if (!hasJumped && characterController.velocity.y < 0)
            {
                PlayJumpLandingSound();
                hasJumped = true;
            }
        }

        Vector3 rayOrigin = transform.position + Vector3.up * rayOriginOffset;
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
        {
            string groundTag = hit.collider.tag;

            // Only play sound if the detected ground tag has changed
            if (groundTag != currentGroundTag)
            {
                currentGroundTag = groundTag;
                audioManager.PlayFootstepSound(groundTag);
            }

            // Debug raycast visualization where the ray hits
            Debug.DrawRay(rayOrigin, Vector3.down * hit.distance, Color.green);
        }
        else
        {
            // No ground detected, reset current ground tag
            currentGroundTag = null;

            // Debug raycast visualization for no ground detected
            Debug.DrawRay(rayOrigin, Vector3.down * raycastDistance, Color.red);
        }
    }

    private void PlayFootstepSound(float volume)
    {
        audioManager.PlayFootstepSound(currentGroundTag, volume);
    }

    private void PlayJumpLandingSound()
    {
        audioManager.PlayFootstepSound(currentGroundTag, jumpFootstepVolume);
    }

    private void ResetFootstepCooldown()
    {
        canPlayFootstep = true;
    }
}
