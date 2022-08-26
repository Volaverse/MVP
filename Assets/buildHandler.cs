using System.Collections;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;
using LandABI.Contracts.Land.ContractDefinition;
using UnityEngine.Networking;
using SimpleJSON;

public class buildHandler : MonoBehaviour
{
    private const float API_CHECK_MAXTIME = 10f; //seconds
    private float apiCheckCountdown = API_CHECK_MAXTIME;

    public GameObject[] prefabs;

    private string publicKey, privateKey;

    private JSONNode oldBuildings;
    public class Item
    {
        public string url;
        public string landContractAddress;
        public string nftTraderContractAddress;
        public Item(string a, string b, string c)
        {
            url = a; landContractAddress = b; nftTraderContractAddress = c;
        }
    }
    Item myconfig;
    public void LoadJson()
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
        oldBuildings = null;
        gameManager manager = gameManager.Instance;
        publicKey = manager.getPublicKey();
        privateKey = manager.getPrivateKey();
        StartCoroutine(Rebuild());

    }

    // Update is called once per frame
    void Update()
    {
        apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            StartCoroutine(Rebuild());
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }

    //private IEnumerator Rebuild()
    //{
    //    var queryRequest = new QueryUnityRequest<GetBuildingsFunction, GetBuildingsOutputDTO >(myconfig.url, publicKey);
    //    yield return queryRequest.Query(new GetBuildingsFunction() { }, myconfig.landContractAddress);
    //    if (queryRequest.Exception != null)
    //    {
    //        Debug.Log("Error in getting buildings in Build Handler: " + queryRequest.Exception.Message);
    //    }
    //    else
    //    {
    //        var buildings = queryRequest.Result.ReturnValue1;
    //        int startX = -88;
    //        int startY = 96;
    //        float xoffset = 6f;
    //        float yoffset = 7f;
    //        float hoffset = 6.4f;
    //        for (int i = 0; i < 120; i++)
    //        {
    //            int x = i / 10;
    //            int y = i - x * 10;
    //            int y_roads = y / 2;
    //            int x_roads = x / 4;
    //            int num_floors = (int) buildings[i];
    //            for (int j = numBuild[i]; j < num_floors; j++)
    //            {
    //                float roomX, roomY, roomZ, rotY;
    //                float wbX, wbZ;
    //                if (y % 2 == 0)
    //                {
    //                    roomX = startX + y * 16 + y_roads * 8 + yoffset;
    //                    roomY = 1.6f + j * hoffset;
    //                    roomZ = startY - x * 16 - x_roads * 8 + xoffset;
    //                    rotY = -90f;
    //                    wbX = -5;
    //                    wbZ = -2;

    //                }
    //                else
    //                {
    //                    roomX = startX + y * 16 + y_roads * 8 - yoffset;
    //                    roomY = 1.6f + j * hoffset;
    //                    roomZ = startY - x * 16 - x_roads * 8 - xoffset;
    //                    rotY = 90f;
    //                    wbX = 5;
    //                    wbZ = 2;
    //                }
    //                GameObject room = Instantiate(room_prefab, new Vector3(roomX, roomY, roomZ), Quaternion.identity);
    //                room.name = "room_" + i + "_" + j;
    //                var roomTransform = room.GetComponent<Transform>();
    //                roomTransform.Rotate(0, rotY, 0);
    //                if (j == 0)
    //                {
    //                    GameObject wb = Instantiate(whiteboard_prefab, new Vector3(roomX + wbX, roomY + 2.6f, roomZ + wbZ), Quaternion.identity);
    //                    wb.name = "whiteboard" + i;
    //                    Debug.Log("Created " + wb.name);
    //                    wb.transform.Rotate(0f, 0f, 180f);
    //                    wb.transform.localScale = new Vector3(7f, 4f, 0.2f);
    //                }
    //            }
    //            numBuild[i] = num_floors;
    //        }
    //    }
    //}

    private IEnumerator Rebuild()
    {
        string databaseURL = "https://tarush.pythonanywhere.com";
        string url = string.Format(databaseURL + "/lands");

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                string data = request.downloadHandler.text;
                JSONNode buildings = JSON.Parse(data)["result"];
                int startX = -88 - 8;
                int startZ = 80 - 8;
                for (int i = 0; i < 120; i++)
                {
                    if (oldBuildings != null && oldBuildings[i] == buildings[i]) continue;

                    int row = i / 10;
                    int col = i - row * 10;
                    int hor_roads = row / 4;
                    int ver_roads = col /2;

                    float landx = startX + col * 16 + ver_roads * 8;
                    float landy = 1.501f;
                    float landz = startZ - row * 16 - hor_roads * 8;
                    if (oldBuildings != null)
                    {
                        for (int j = 0; j < oldBuildings[i].Count; j++)
                        {
                            string newname;
                            if (oldBuildings[i][j]["id"] == 5) newname = "room_" + i + "_" + j;
                            else newname = "str_" + i + "_" + j;
                            GameObject go = GameObject.Find(newname);
                            if (go != null) Destroy(go);
                        }
                    }
                    for (int j = 0; j < buildings[i].Count; j++)
                    {
                        GameObject str = Instantiate(prefabs[buildings[i][j]["id"]], new Vector3(landx + buildings[i][j]["posX"], landy + buildings[i][j]["posY"], landz + buildings[i][j]["posZ"]), Quaternion.identity);
                        string newname;
                        if (buildings[i][j]["id"] == 5) newname = "room_" + i + "_" + j;
                        else newname = "str_" + i + "_" + j;
                        str.name = newname;
                        var strTransform = str.GetComponent<Transform>();
                        strTransform.Rotate(buildings[i][j]["rotX"], buildings[i][j]["rotY"], buildings[i][j]["rotZ"]);
                        strTransform.localScale = new Vector3(buildings[i][j]["scaleX"], buildings[i][j]["scaleY"], buildings[i][j]["scaleZ"]);
                    }
                }
                oldBuildings = buildings;
            }
        }
        
    }
}
