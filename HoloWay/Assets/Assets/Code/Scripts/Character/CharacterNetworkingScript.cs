using System.Collections;
using System.Collections.Generic;
using TMPro;
using UMA.CharacterSystem;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkingScript : NetworkBehaviour
{

    

    [SerializeField]
    public NetworkVariable<FixedString512Bytes> Network_PlayerName = new NetworkVariable<FixedString512Bytes>();
    public NetworkVariable<FixedString4096Bytes> Network_PlayerAvatarRecipe = new NetworkVariable<FixedString4096Bytes>();

    [Header("Object References")]
    public TMP_Text NetworkPlayerNameText;
    public DynamicCharacterAvatar Avatar;
    public string AvatarRecipe = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NetworkPlayerNameText.text = Network_PlayerName.Value.ToString();
        if(AvatarRecipe != Network_PlayerAvatarRecipe.Value.ToString())
        {
            AvatarRecipe = Network_PlayerAvatarRecipe.Value.ToString();
            Avatar.LoadFromRecipeString(AvatarRecipe);
        }
        if (!IsOwner) return;
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        this.AttachNetworkVariableCallbacks();
    }

    public void AttachNetworkVariableCallbacks()
    {
        // Name
        Network_PlayerName.OnValueChanged += (FixedString512Bytes OldName, FixedString512Bytes NewName) =>
        {
            Network_PlayerName.Value = NewName;
        };
        Network_PlayerAvatarRecipe.OnValueChanged += (FixedString4096Bytes OldRecipeName, FixedString4096Bytes NewRecipeName) =>
        {
            Network_PlayerAvatarRecipe.Value = NewRecipeName;
        };
    }

    //[ServerRpc(RequireOwnership = false)]
    [ServerRpc]
    public void ChangePlayerNameServerRpc(FixedString512Bytes name)
    {
        Network_PlayerName.Value = name;
    }
    //[ServerRpc(RequireOwnership = false)]
    [ServerRpc]
    public void AvatarRecipeChangeServerRpc(FixedString4096Bytes name)
    {
        Network_PlayerAvatarRecipe.Value = name;
    }
}
