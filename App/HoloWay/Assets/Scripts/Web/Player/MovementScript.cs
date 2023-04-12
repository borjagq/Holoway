using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UMA.CharacterSystem;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class MovementScript : NetworkBehaviour
{
    
    private INIFile file = new INIFile();
    [SerializeField]
    public Renderer PlayerRenderer;
    [SerializeField]
    public TMP_Text PlayerText;
    [SerializeField]
    public ClientNetworkAnimator Animator;
    [SerializeField]
    public Camera PlayerCamera;
    public Transform ForwardObject;
    [SerializeField]
    public DynamicCharacterAvatar Avatar;
    [Header("Variables")]
    public float WalkSpeedMultiplier = 1.0f;
    public float WalkDirectionMultiplier = 20.0f;
    public float SprintSpeedMultiplier = 5.0f;
    public float SprintDirectionMultiplier = 40.0f;

    public bool HasLoadedOnce = false;

    public NetworkVariable<FixedString4096Bytes> PlayerAvatarRecipe = new NetworkVariable<FixedString4096Bytes>();
    public NetworkVariable<Color> PlayerColor = new NetworkVariable<Color>(Color.red);
    //public NetworkVariable<FixedString128Bytes> PlayerName = new NetworkVariable<FixedString128Bytes>(GlobalNetworkDetails.gPlayerName);
    private void Start()
    {
       
        
        //PlayerAnimator = GetComponent<Animator>();
    }
    public override void OnNetworkSpawn()
    {
        PlayerCamera.gameObject.SetActive(IsOwner);
        base.OnNetworkSpawn();
        Avatar = this.GetComponent<DynamicCharacterAvatar>();
        PlayerColor.OnValueChanged += (Color oldColor, Color newColor) =>
        {
            PlayerColor.Value = newColor;
        };
        PlayerAvatarRecipe.OnValueChanged += (FixedString4096Bytes oldRecipe, FixedString4096Bytes newRecipe) =>
        {
            PlayerAvatarRecipe.Value = newRecipe;
            Avatar.LoadFromRecipeString(newRecipe.ToString());
        };
        /*PlayerName.OnValueChanged += (FixedString128Bytes oldName, FixedString128Bytes newName) =>
        {
            PlayerName.Value = newName;
        };*/
        //PlayerRenderer = GetComponent<Renderer>();
        Debug.Log($"Connected with clientID: {OwnerClientId}");

        GameObject Room = GameObject.FindGameObjectWithTag("Room");
        Debug.Log("Room Object: " + Room);
        if(Room != null)
        {
            GameObject SpawnPosition = GameObject.Find(Room.name + "/SpawnPosition");
            this.transform.position = SpawnPosition.transform.position;
            Debug.Log("Spawning player at position " + SpawnPosition.transform.position);
        }
        file.LoadFromFile("./Data.ini");
        string recipe = file.IniReadValue("AvatarDetails", "AvatarData");
        Debug.Log("Loaded recipe: " + recipe);
        Avatar.LoadFromRecipeString(recipe);
        PlayerAvatarRecipe.Value = recipe;
        OnSpawnServerRpc();
        
    }


    void Update()
    {

/*        if (PlayerAvatarRecipe != null)
        {
            if (!HasLoadedOnce)
            {
                Debug.Log(PlayerAvatarRecipe.Value.ToString());
                (PlayerAvatarRecipe.Value = 
                HasLoadedOnce = true;
            }
        }*/
        if (!IsOwner) return;
        //PlayerRenderer.material.color = PlayerColor.Value;
        //PlayerText.text = PlayerName.Value.ToString();
        Animator.Animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        Animator.Animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
        Animator.Animator.SetBool("Sprint", Input.GetKey(KeyCode.LeftShift));
        float ForwardMultiplier = WalkSpeedMultiplier;
        float DirectionMultiplier = WalkDirectionMultiplier;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ForwardMultiplier = SprintSpeedMultiplier;
            DirectionMultiplier = SprintDirectionMultiplier;
        }
        else
        {
            ForwardMultiplier = WalkSpeedMultiplier;
            DirectionMultiplier = WalkDirectionMultiplier;
        }
        this.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Vertical") * Input.GetAxis("Horizontal") * Time.deltaTime * DirectionMultiplier, 0.0f));
        Vector3 ForwardVector = ForwardObject.transform.position - this.transform.position;
        this.transform.position += ForwardVector * Input.GetAxis("Vertical") * ForwardMultiplier * Time.deltaTime;
    }
    [ServerRpc(RequireOwnership = false)]
    public void OnSpawnServerRpc()
    {
        Debug.Log($"Called OnSpawn ServerRPC {OwnerClientId}");
    }

    [ServerRpc]
    public void ChangeColorServerRpc(Color color)
    {
        PlayerColor.Value = color;
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeAvatarServerRpc(FixedString4096Bytes avatarstring)
    {
        PlayerAvatarRecipe.Value = avatarstring;
        
    }
/*    [ServerRpc]
    public void ChangeNameServerRpc(FixedString128Bytes NewName)
    {
        PlayerName.Value = NewName;
    }*/
}
