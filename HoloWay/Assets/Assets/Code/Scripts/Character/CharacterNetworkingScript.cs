using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkingScript : NetworkBehaviour
{

    [SerializeField]
    public NetworkVariable<FixedString512Bytes> Network_PlayerName = new NetworkVariable<FixedString512Bytes>();


    [Header("Object References")]
    public TMP_Text NetworkPlayerNameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NetworkPlayerNameText.text = Network_PlayerName.Value.ToString();
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
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangePlayerNameServerRpc(FixedString512Bytes name)
    {
        Network_PlayerName.Value = name;
    }
}
