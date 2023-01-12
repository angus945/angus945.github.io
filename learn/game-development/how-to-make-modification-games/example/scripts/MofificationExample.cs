using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using MoonSharp.Interpreter;
using UnityEngine.SceneManagement;

public class MofificationExample : MonoBehaviour
{
    public List<EntityDefine> defines = new List<EntityDefine>();
    public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    public Dictionary<string, Script> scripts = new Dictionary<string, Script>();

    [Space]
    public GameEntity entityPrefab;
    public Button[] buttons;
    public Button relode;

    [Space]
    public Text logTest;
    public Button clear;

    void Start()
    {
        Script.DefaultOptions.DebugPrint = (string s) => logTest.text += s + "\n";

        LoadEntities();
        LoadTextures();
        LoadScripts();

        SetButton();

        Application.targetFrameRate = 120;
    }

    void LoadEntities()
    {
        string path = $"{Application.streamingAssetsPath}\\entities.xml";
        byte[] entitiesData = File.ReadAllBytes(path);
        string dataText = System.Text.Encoding.UTF8.GetString(entitiesData);

        XmlDocument dataXML = new XmlDocument();
        dataXML.LoadXml(dataText);

        XmlNode root = dataXML.DocumentElement;
        for (int i = 0; i < root.ChildNodes.Count; i++)
        {
            XmlNode node = root.ChildNodes[i];

            EntityDefine entity = ConvertNode<EntityDefine>(node);
            defines.Add(entity);
        }
    }
    void LoadTextures()
    {
        string directoryPath = $"{Application.streamingAssetsPath}\\Sprites";
        string[] files = Directory.GetFiles(directoryPath);

        for (int i = 0; i < files.Length; i++)
        {
            string path = files[i];

            if (path.EndsWith(".png"))
            {
                byte[] data = File.ReadAllBytes(path);

                Texture2D image = new Texture2D(2, 2);
                image.filterMode = FilterMode.Point;
                image.LoadImage(data);

                string name = path.Replace(directoryPath + "\\", "");
                textures.Add(name, image);

                Debug.Log(name);
            }
        }
    }
    void LoadScripts()
    {
        string directoryPath = $"{Application.streamingAssetsPath}\\Scripts";
        string[] files = Directory.GetFiles(directoryPath);

        for (int i = 0; i < files.Length; i++)
        {
            string path = files[i];
            if (path.EndsWith(".lua"))
            {
                byte[] data = File.ReadAllBytes(path);
                string code = System.Text.Encoding.UTF8.GetString(data);

                Script script = new Script();
                script.DoString(code);

                string name = path.Replace(directoryPath + "\\", "");
                scripts.Add(name, script);
            }
        }
    }

    void SetButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < defines.Count)
            {
                int index = i;

                EntityDefine entity = defines[i];
                buttons[i].GetComponentInChildren<Text>().text = entity.id;
                buttons[i].onClick.AddListener(() =>
                {
                    GenerateEntity(defines[index]);
                });
            }
            else buttons[i].gameObject.SetActive(false);
        }

        relode.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        clear.onClick.AddListener(() => logTest.text = "");
    }
    void GenerateEntity(EntityDefine define)
    {
        GameEntity entity = Instantiate(entityPrefab);

        Texture2D texture = textures[define.sprite];
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);

        Script script = scripts[define.script];
        DynValue function = script.Globals.Get("awake");
        entity.SetEntity(define.id, sprite, function);
    }

    public static T ConvertNode<T>(XmlNode node) where T : class
    {
        MemoryStream stm = new MemoryStream();

        StreamWriter stw = new StreamWriter(stm);
        stw.Write(node.OuterXml);
        stw.Flush();

        stm.Position = 0;

        XmlSerializer ser = new XmlSerializer(typeof(T));
        T result = (ser.Deserialize(stm) as T);

        return result;
    }
}
