using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using LandABI.Contracts.Land.ContractDefinition;
using NftManagerABI.Contracts.NftManager.ContractDefinition;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using SimpleJSON;
public class Maphandler : MonoBehaviour
{
    string nftTraderAbi = @"
	[
	{
		""inputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""constructor""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""contractAddress"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""closeSale"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""contractAddress"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""category"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""num"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""string"",
				""name"": ""ipfsHash"",
				""type"": ""string""
			}
		],
		""name"": ""establish"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""contractAddress"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""getInfo"",
		""outputs"": [
			{
				""components"": [
					{
						""internalType"": ""address"",
						""name"": ""owner"",
						""type"": ""address""
					},
					{
						""internalType"": ""bool"",
						""name"": ""onSale"",
						""type"": ""bool""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""price"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""category"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""tokenId"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""address"",
						""name"": ""contractAddress"",
						""type"": ""address""
					},
					{
						""internalType"": ""string"",
						""name"": ""ipfsHash"",
						""type"": ""string""
					}
				],
				""internalType"": ""struct NftManager.Detail"",
				""name"": """",
				""type"": ""tuple""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""category"",
				""type"": ""uint256""
			}
		],
		""name"": ""getNFTs"",
		""outputs"": [
			{
				""components"": [
					{
						""internalType"": ""address"",
						""name"": ""owner"",
						""type"": ""address""
					},
					{
						""internalType"": ""bool"",
						""name"": ""onSale"",
						""type"": ""bool""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""price"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""category"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""tokenId"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""address"",
						""name"": ""contractAddress"",
						""type"": ""address""
					},
					{
						""internalType"": ""string"",
						""name"": ""ipfsHash"",
						""type"": ""string""
					}
				],
				""internalType"": ""struct NftManager.Detail[]"",
				""name"": """",
				""type"": ""tuple[]""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""name"": ""nfts"",
		""outputs"": [
			{
				""internalType"": ""address"",
				""name"": ""owner"",
				""type"": ""address""
			},
			{
				""internalType"": ""bool"",
				""name"": ""onSale"",
				""type"": ""bool""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""price"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""bool"",
				""name"": ""valid"",
				""type"": ""bool""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""contractAddress"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""purchase"",
		""outputs"": [],
		""stateMutability"": ""payable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""price"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""address"",
				""name"": ""contractAddress"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""startSale"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""testforWorking"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	}
]
";
    string landAbi = @"[
	{
		""inputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""constructor""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""owner"",
				""type"": ""address""
			},
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""approved"",
				""type"": ""address""
			},
			{
				""indexed"": true,
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""Approval"",
		""type"": ""event""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""owner"",
				""type"": ""address""
			},
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""operator"",
				""type"": ""address""
			},
			{
				""indexed"": false,
				""internalType"": ""bool"",
				""name"": ""approved"",
				""type"": ""bool""
			}
		],
		""name"": ""ApprovalForAll"",
		""type"": ""event""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""to"",
				""type"": ""address""
			},
			{
				""indexed"": true,
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""Transfer"",
		""type"": ""event""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""to"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""approve"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""owner"",
				""type"": ""address""
			}
		],
		""name"": ""balanceOf"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""landId"",
				""type"": ""uint256""
			}
		],
		""name"": ""build"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""getApproved"",
		""outputs"": [
			{
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""getBuildings"",
		""outputs"": [
			{
				""internalType"": ""uint256[]"",
				""name"": """",
				""type"": ""uint256[]""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""getMetaData"",
		""outputs"": [
			{
				""components"": [
					{
						""internalType"": ""string"",
						""name"": ""name"",
						""type"": ""string""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""category"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""string"",
						""name"": ""description"",
						""type"": ""string""
					}
				],
				""internalType"": ""struct Land.Metadata"",
				""name"": """",
				""type"": ""tuple""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""getOwners"",
		""outputs"": [
			{
				""internalType"": ""address[]"",
				""name"": """",
				""type"": ""address[]""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""owner"",
				""type"": ""address""
			},
			{
				""internalType"": ""address"",
				""name"": ""operator"",
				""type"": ""address""
			}
		],
		""name"": ""isApprovedForAll"",
		""outputs"": [
			{
				""internalType"": ""bool"",
				""name"": """",
				""type"": ""bool""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""name"",
		""outputs"": [
			{
				""internalType"": ""string"",
				""name"": """",
				""type"": ""string""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""ownerOf"",
		""outputs"": [
			{
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""internalType"": ""address"",
				""name"": ""to"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""safeTransferFrom"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""internalType"": ""address"",
				""name"": ""to"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""bytes"",
				""name"": ""_data"",
				""type"": ""bytes""
			}
		],
		""name"": ""safeTransferFrom"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""operator"",
				""type"": ""address""
			},
			{
				""internalType"": ""bool"",
				""name"": ""approved"",
				""type"": ""bool""
			}
		],
		""name"": ""setApprovalForAll"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""bytes4"",
				""name"": ""interfaceId"",
				""type"": ""bytes4""
			}
		],
		""name"": ""supportsInterface"",
		""outputs"": [
			{
				""internalType"": ""bool"",
				""name"": """",
				""type"": ""bool""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""symbol"",
		""outputs"": [
			{
				""internalType"": ""string"",
				""name"": """",
				""type"": ""string""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""tokenURI"",
		""outputs"": [
			{
				""internalType"": ""string"",
				""name"": """",
				""type"": ""string""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""internalType"": ""address"",
				""name"": ""to"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""tokenId"",
				""type"": ""uint256""
			}
		],
		""name"": ""transferFrom"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	}
]";

    int curr = -1;
    bool wallet;
    string publicKey;
    string privateKey;
    string currSP = "0";
    bool canvasOpened = false;
    public Text message;
    public Text BP;
    public Text balance;
	public Text permissionButtonText;
    public InputField SP;

    public Button reload;
    public Button buy;
    public Button toggleSale;
    public Button updateSP;
    public Button build;
    public Button jump;
	public Button permissionButton;


	public GameObject sceneHolder;
	public Text sceneLoading;
	public Button addSceneButton;

    public Button land_prefab;
    public Canvas canvas;
    public GameObject panel;
	public GameObject defaultPanel;

    private Button[] landButton;

    public static gameManager manager;
    //public GameObject vc;

    public GameObject room_prefab;
    bool teleport_flag = false;

    List<string> owners;
    List<Detail> listings;

	[DllImport("__Internal")]
    private static extern string GetAccount();

    [DllImport("__Internal")]
    private static extern void BuyTransaction(string nftAbi, string nftTraderContractAddress, string landContractAddress, string landId, string amount);

    [DllImport("__Internal")]
    private static extern void StartSaleTransaction(string nftAbi, string nftTraderContractAddress, string landContractAddress, string landId, string amount);

	[DllImport("__Internal")]
    private static extern void CloseSaleTransaction(string nftAbi, string nftTraderContractAddress, string landContractAddress, string landId);

    [DllImport("__Internal")]
    private static extern void BuildTransaction(string landAbi, string landContractAddress, string landId);

	[DllImport("__Internal")]
	private static extern void ChangePermissionTransaction(string landAbi, string landContractAddress, string nftTraderContractAddress, bool perm);

	public class Item
    {
        public string url;
        public string landContractAddress;
        public string nftTraderContractAddress;
        public Item (string a, string b, string c)
        {
            url = a; landContractAddress = b; nftTraderContractAddress = c;
        }
    }
    Item myconfig;
    void LoadJson()
    {
        myconfig = new Item(
			"https://rinkeby.infura.io/v3/1519ee6966cb4f18a860f37ad4ba6e1b",
			"0x594Da1a6b76c62B384b5f4e4275e68c1E41b1bA4",
			"0xe248F26e92dbE612174aC3C5d6c3bB705Ef215bc"
		);
    }

    void Start()
    {
		LoadJson();
        Debug.Log("url: " + myconfig.url);
        Debug.Log("land: " + myconfig.landContractAddress);
        Debug.Log("NftTrader: " + myconfig.nftTraderContractAddress);
        message.text = "Loading...";
        manager = gameManager.Instance;

		string roomNo = manager.getRoomNo();

        reload.interactable = false;
        buy.interactable = false;
        updateSP.interactable = false;
        toggleSale.interactable = false;
        build.interactable = false;
        jump.interactable = false;
        SP.interactable = false;
		permissionButton.interactable = false;
        BP.text = "0.0";
        SP.text = "0.0";

        landButton = new Button[120];

        int startX = -1200;
        int startY = 550;
        for (int i=0;i<120;i++)
        {
            int x = i / 10;
            int y = i - x * 10;
            int y_roads = y / 2;
            int x_roads = x / 4;
            landButton[i] = Instantiate(land_prefab, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity) as Button;
            var rectTransform = landButton[i].GetComponent<RectTransform>();
            rectTransform.SetParent(panel.transform, false);
            rectTransform.localPosition = new UnityEngine.Vector3(startX+y*60+y_roads*30, startY-x*60-x_roads*30, 0);
            rectTransform.sizeDelta = new UnityEngine.Vector2(45, 45);
            landButton[i].interactable = false;
            int i_temp = i;
            landButton[i].onClick.AddListener(delegate {onLand(i_temp);});
        }
        wallet = manager.isWallet();
        if (wallet)
        {
            publicKey = GetAccount().ToLower();
        }
        else
        {
            publicKey = manager.getPublicKey();
            privateKey = manager.getPrivateKey();
        }
        Debug.Log("Public Key: " + publicKey);
		if (publicKey == null || publicKey == "")
        {
			Debug.LogError("Public key not received");
        }
        StartCoroutine(LoadPage());
		reset_scenes();
    }
    public void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            Debug.Log("M pressed");
            if (canvasOpened)
            {
                panel.SetActive(false);
				defaultPanel.SetActive(true);
            }
            else
            {
                panel.SetActive(true);
				defaultPanel.SetActive(false);
			}
            canvasOpened = !canvasOpened;
        }
    }
    public IEnumerator ReloadBalance()
    {
        Debug.Log("Extracting ETH balance! (this might take time)");
        var balanceRequest = new EthGetBalanceUnityRequest(myconfig.url);
        yield return balanceRequest.SendRequest(publicKey, BlockParameter.CreateLatest());

        if (balanceRequest.Exception != null)
        {
            Debug.Log("Error: " + balanceRequest.Exception.Message);
        }

        decimal _balance = UnitConversion.Convert.FromWei(balanceRequest.Result.Value);
        balance.text = _balance.ToString();

        Debug.Log("Balance of account:" + balance.text);
    }
    private IEnumerator LoadPage()
    {
        Debug.Log("Extracting ETH balance! (this might take time)");
        var balanceRequest = new EthGetBalanceUnityRequest(myconfig.url);
        yield return balanceRequest.SendRequest(publicKey, BlockParameter.CreateLatest());

        if (balanceRequest.Exception != null)
        {
            Debug.Log("Error: " + balanceRequest.Exception.Message);
        }
        decimal _balance = UnitConversion.Convert.FromWei(balanceRequest.Result.Value);
        balance.text = _balance.ToString();
        Debug.Log("Balance of account:" + balance.text);

        var queryRequest = new QueryUnityRequest<GetOwnersFunction, GetOwnersOutputDTO>(myconfig.url, publicKey);
        yield return queryRequest.Query(new GetOwnersFunction() {}, myconfig.landContractAddress);
        if (queryRequest.Exception != null)
        {
            Debug.Log("Error: " + queryRequest.Exception.Message);
            message.text = queryRequest.Exception.Message;
        }
        owners = queryRequest.Result.ReturnValue1;
        Debug.Log("Owners:" + owners);

        var queryRequest2 = new QueryUnityRequest<GetNFTsFunction, GetNFTsOutputDTO>(myconfig.url, publicKey);
        yield return queryRequest2.Query(new GetNFTsFunction() {Category = 0}, myconfig.nftTraderContractAddress);
        if (queryRequest2.Exception != null)
        {
            Debug.Log("Error: " + queryRequest2.Exception.Message);
            message.text = queryRequest2.Exception.Message;
        }
        listings = queryRequest2.Result.ReturnValue1;
        Debug.Log("Listings:" + listings);

		var queryRequest3 = new QueryUnityRequest<IsApprovedForAllFunction, IsApprovedForAllOutputDTO>(myconfig.url, publicKey);
		yield return queryRequest3.Query(new IsApprovedForAllFunction() {
			Owner = publicKey,
			Operator = myconfig.nftTraderContractAddress
		}, myconfig.landContractAddress);
		if (queryRequest3.Exception != null)
		{
			Debug.Log("Error: " + queryRequest3.Exception.Message);
			message.text = queryRequest3.Exception.Message;
		}
		bool permissionStatus = queryRequest3.Result.ReturnValue1;
		Debug.Log("Permission status:" + permissionStatus);

		if (permissionStatus) permissionButtonText.text = "Disallow";
		else permissionButtonText.text = "Allow";

		Debug.Log(publicKey);
        for (int i = 0; i < 120; i++)
        {
			owners[i] = owners[i].ToLower();
            string owner = owners[i];
			//Debug.Log(i.ToString() + ":" + owner + ":" + (owner == publicKey) + ":" + listings[i].Valid);
            bool open_for_sale = listings[i].OnSale;
            ColorBlock colors = landButton[i].colors;
            if (owner != null && owner == publicKey)
            {
                colors.normalColor = Color.blue;
            }
            else if (open_for_sale)
            {
                colors.normalColor = Color.green;
            }
            else
            {
                colors.normalColor = Color.gray;
            }
            landButton[i].colors = colors;
            landButton[i].interactable = true;
        }
        message.text = "";
        reload.interactable = true;
		permissionButton.interactable = true;
    }

    public void reset_panel()
    {
        buy.interactable = false;
        BP.text = "0.00";
        build.interactable = false;
        jump.interactable = true;
        SP.interactable = false;
        updateSP.interactable = false;
        toggleSale.GetComponentInChildren<Text>().text = "Start Sale";
        toggleSale.interactable = false;
        SP.text = "0.0";
        
    }

	public void onChangePermission()
    {
		Loading();
		StartCoroutine(onChangePermissionHelper());
    }

	private IEnumerator onChangePermissionHelper()
    {
		if (wallet)
		{
			if (permissionButtonText.text == "Allow")
			{
				ChangePermissionTransaction(landAbi, myconfig.landContractAddress, myconfig.nftTraderContractAddress, true);
			}
			else
            {
				ChangePermissionTransaction(landAbi, myconfig.landContractAddress, myconfig.nftTraderContractAddress, false);
			}
			Debug.Log("Changed Permission");
			custom_reload();
		}
		else
		{
			var request = new TransactionSignedUnityRequest(myconfig.url, privateKey);
			request.UseLegacyAsDefault = true;
			var transactionMessage = new SetApprovalForAllFunction
			{
				Operator = myconfig.nftTraderContractAddress,
				Approved = (permissionButtonText.text == "Allow")
			};
			yield return request.SignAndSendTransaction(transactionMessage, myconfig.landContractAddress);
			if (request.Exception != null)
			{
				Debug.Log("Error: " + request.Exception.Message);
				custom_reload();
				// TODO
				message.text = request.Exception.Message;
			}
			else
			{
				string hash = request.Result;
				Debug.Log("build hash: " + hash);

				TransactionReceiptPollingRequest trp = new TransactionReceiptPollingRequest(myconfig.url);
				yield return trp.PollForReceipt(hash, 2);
				var receipt = trp.Result;
				Debug.Log("Built");
				custom_reload();
				message.text = "";
			}
		}
	}

	public void reset_scenes()
	{
		Button [] gs = sceneHolder.GetComponentsInChildren<Button>();
		for (int i = 0; i < gs.Length; i++)
		{
            Destroy(gs[i].gameObject);
        }
		if (curr >= 0 && owners[curr] == publicKey)
        {
			sceneLoading.text = "Loading...";
			StartCoroutine(resetScenesHelper());
		}
		else
        {
			sceneLoading.text = "Click on your land to view scenes!";
		}
		
		
	}

	IEnumerator resetScenesHelper()
	{
		string databaseURL = "https://tarush.pythonanywhere.com";
		string url = string.Format(databaseURL + "/scenes/" + publicKey.ToLower());
		using (UnityWebRequest request = UnityWebRequest.Get(url))
		{
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(request.error);
			}
			else
			{
				JSONNode scenes = JSON.Parse(request.downloadHandler.text)["result"];
				Debug.Log(scenes);
                for (int i = 0; i < scenes.Count; i++)
                {
                    var button = Instantiate(addSceneButton, Vector3.zero, Quaternion.identity) as Button;
                    var rectTransform = button.GetComponent<RectTransform>();
                    rectTransform.SetParent(sceneHolder.transform);
                    rectTransform.localPosition = new Vector3(0, -100 * i, 0);
					rectTransform.sizeDelta = new Vector2(300, 75);
                    button.GetComponentInChildren<Text>().text = scenes[i]["name"];
                    var tempi = i;
                    button.onClick.AddListener(delegate { onAddScene(scenes[tempi]["id"]); });
                }
				sceneLoading.text = "";
            }
		}
	}
	public void onAddScene(int id)
    {
		StartCoroutine(addSceneHelper(id));
    }
	IEnumerator addSceneHelper(int id)
	{
		WWWForm form = new WWWForm();
		string databaseURL = "https://tarush.pythonanywhere.com";
		string url = string.Format(databaseURL + "/establish?scene_id=" + id + "&land_id=" + curr);
		using (UnityWebRequest request = UnityWebRequest.Post(url, form))
		{
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(request.error);
			}
			else
			{
				JSONNode result = JSON.Parse(request.downloadHandler.text);
				Debug.Log("establish API success : " + result);
			}
		}
	}


	public void onLand(int id)
    {
        curr = id;
        reset_panel();
		reset_scenes();
        Debug.Log(id);
        if (owners[id] == publicKey)
        {
            if (listings[id].OnSale)
            {
                updateSP.interactable = true;
                toggleSale.GetComponentInChildren<Text>().text = "Close Sale";
                toggleSale.interactable = true;
                build.interactable = true;
                SP.interactable = true;
                SP.text = listings[id].Price.ToString();
            }
            else
            {
                toggleSale.GetComponentInChildren<Text>().text = "Start Sale";
                toggleSale.interactable = true;
                SP.interactable = true;
                build.interactable = true;
                SP.text = "0.0";
            }
        }
        else
        {
            if (listings[id].OnSale)
            {
                BP.text = listings[id].Price.ToString();
                buy.interactable = true;
            }
            else
            {

            }
        }   
    }

    public void Loading()
    {
        reload.interactable = false;
		permissionButton.interactable = false;
        buy.interactable = false;
        updateSP.interactable = false;
        toggleSale.interactable = false;
        build.interactable = false;
        jump.interactable = false;

        SP.interactable = false;

        for (int i=0;i<120;i++)
        {
            landButton[i].interactable = false;
        }
    }

    public void custom_reload()
    {
        Loading();
        StartCoroutine(LoadPage());
    }

    public void onBuy()
    {
        Loading();
        StartCoroutine(onBuyHelper());
    }

    private IEnumerator onBuyHelper()
    {
        if (wallet)
        {
            BuyTransaction(nftTraderAbi, myconfig.nftTraderContractAddress, myconfig.landContractAddress, curr.ToString(), listings[curr].Price.ToString());
            custom_reload();
		}
        else
        {
            var request = new TransactionSignedUnityRequest(myconfig.url, privateKey);
            request.UseLegacyAsDefault = true;
            var transactionMessage = new PurchaseFunction
            {
                AmountToSend = listings[curr].Price,
                ContractAddress = myconfig.landContractAddress,
                TokenId = curr
            };
            yield return request.SignAndSendTransaction(transactionMessage, myconfig.nftTraderContractAddress);
            if (request.Exception != null)
            {
                Debug.Log("Error: " + request.Exception.Message);
                custom_reload();
                message.text = request.Exception.Message;
            }
            else
            {
                string hash = request.Result;
                Debug.Log("Purchase hash: " + hash);

                TransactionReceiptPollingRequest trp = new TransactionReceiptPollingRequest(myconfig.url);
                yield return trp.PollForReceipt(hash, 2);
                var receipt = trp.Result;
                Debug.Log("Nft transferred");
                custom_reload();
            }
        }
    }

    public void onToggleSale()
    {
        Loading();
        StartCoroutine(onToggleSaleHelper());
    }
    public void onChangeSP(string _sp)
    {
        Debug.Log("value:" + _sp);
        if (_sp == "") currSP = "0";
        else currSP = _sp;
    }
    private IEnumerator onToggleSaleHelper()
    {
		if (wallet)
        {
			if (listings[curr].OnSale)
            {
                CloseSaleTransaction(nftTraderAbi, myconfig.nftTraderContractAddress, myconfig.landContractAddress, curr.ToString());
            }
			else
            {
                StartSaleTransaction(nftTraderAbi, myconfig.nftTraderContractAddress, myconfig.landContractAddress, curr.ToString(), currSP);
            }
			Debug.Log("Sale toggled");
			custom_reload();
		}
		else
        {
			var request = new TransactionSignedUnityRequest(myconfig.url, privateKey);
			request.UseLegacyAsDefault = true;
			if (listings[curr].OnSale)
			{
				var transactionMessage = new CloseSaleFunction
				{
					ContractAddress = myconfig.landContractAddress,
					TokenId = curr
				};
				yield return request.SignAndSendTransaction(transactionMessage, myconfig.nftTraderContractAddress);
			}
			else
			{
				var transactionMessage = new StartSaleFunction
				{
					Price = int.Parse(currSP),
					ContractAddress = myconfig.landContractAddress,
					TokenId = curr
				};
				yield return request.SignAndSendTransaction(transactionMessage, myconfig.nftTraderContractAddress);
			}
			if (request.Exception != null)
			{
				Debug.Log("Error: " + request.Exception.Message);
				custom_reload();
				message.text = request.Exception.Message;
			}
			else
			{
				string hash = request.Result;
				Debug.Log("Toggle Sale hash: " + hash);

				TransactionReceiptPollingRequest trp = new TransactionReceiptPollingRequest(myconfig.url);
				yield return trp.PollForReceipt(hash, 2);
				var receipt = trp.Result;
				Debug.Log("Sale toggled");
				custom_reload();
			}
		}
        
    }

    public void onUpdateSP()
    {
        Loading();
        StartCoroutine(onUpdateSPHelper());
    }

    private IEnumerator onUpdateSPHelper()
    {
		if (wallet)
		{
			StartSaleTransaction(nftTraderAbi, myconfig.nftTraderContractAddress, myconfig.landContractAddress, curr.ToString(), currSP);
			Debug.Log("Sale toggled");
			custom_reload();
		}
		else
        {
			if (listings[curr].OnSale)
			{
				var request = new TransactionSignedUnityRequest(myconfig.url, privateKey);
				request.UseLegacyAsDefault = true;
				var transactionMessage = new StartSaleFunction
				{
					Price = int.Parse(currSP),
					ContractAddress = myconfig.landContractAddress,
					TokenId = curr
				};
				yield return request.SignAndSendTransaction(transactionMessage, myconfig.nftTraderContractAddress);
				if (request.Exception != null)
				{
					Debug.Log("Error: " + request.Exception.Message);
					custom_reload();
					message.text = request.Exception.Message;
				}
				else
				{
					string hash = request.Result;
					Debug.Log("Update SP hash: " + hash);

					TransactionReceiptPollingRequest trp = new TransactionReceiptPollingRequest(myconfig.url);
					yield return trp.PollForReceipt(hash, 2);
					var receipt = trp.Result;
					Debug.Log("Sale toggled");
					custom_reload();
					message.text = "";
				}
			}
			else
			{
				Debug.Log("Attempt to update SP but not listed");
			}
		}
    }

    public void onBuild()
    {
        Loading();
        StartCoroutine(onBuildHelper());

    }

    private IEnumerator onBuildHelper()
    {
		if (wallet)
        {
			BuildTransaction(landAbi, myconfig.landContractAddress, curr.ToString());
			Debug.Log("Built");
			custom_reload();
		}
		else
        {
			var request = new TransactionSignedUnityRequest(myconfig.url, privateKey);
			request.UseLegacyAsDefault = true;
			var transactionMessage = new BuildFunction
			{
				LandId = curr
			};
			yield return request.SignAndSendTransaction(transactionMessage, myconfig.landContractAddress);
			if (request.Exception != null)
			{
				Debug.Log("Error: " + request.Exception.Message);
				custom_reload();
				// TODO
				message.text = request.Exception.Message;
			}
			else
			{
				string hash = request.Result;
				Debug.Log("build hash: " + hash);

				TransactionReceiptPollingRequest trp = new TransactionReceiptPollingRequest(myconfig.url);
				yield return trp.PollForReceipt(hash, 2);
				var receipt = trp.Result;
				Debug.Log("Built");
				custom_reload();
				message.text = "";
			}
		}
    }

    public void OnJump()
    {
        //teleport_flag = true;

        panel.SetActive(false);
		defaultPanel.SetActive(true);
        canvasOpened = false;

        int x = curr / 10;
        int y = curr - x * 10;
        int y_roads = y / 2;
        int x_roads = x / 4;

        int startX = -88;
        int startY = 96;
        //CharacterController cc = player.GetComponent<CharacterController>();
        //Debug.Log(player.transform.position);
        //Debug.Log(cc);
        //cc.enabled = false;
        //Transform transform = player.GetComponent<Transform>();
        //transform.position = new Vector3(startX + y * 16 + y_roads * 8, transform.position[1], startY - x * 16 - x_roads * 8);
        //cc.enabled = true;
        manager.settpinfo(startX + y * 16 + y_roads * 8, startY - x * 16 - x_roads * 8);
        //Debug.Log(player.transform.position);
        //teleport_flag = false;
    }

    public bool isOwner(int landId)
    {
        return owners[landId] == publicKey;
    }


}
