using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    public Transform cameraTrans;
    public Tilemap map;

    //public TileBase tileBase;
    //public Grid grid;
    public Dictionary<int, TileBase> sundayTileDictionary = new Dictionary<int, TileBase>();
    public Dictionary<int, TileBase> rainyTileDictionary = new Dictionary<int, TileBase>();

    public TileBase[] sundayTile;
    public TileBase[] rainyTile;

    public TileBase thisTileBase;

    public int boundsSizeX;
    public int boundsSizeY;
    public bool isChanging;
    public enum WeatherState
    {
        sunday,
        
        rainy,
    }
    public WeatherState currentWeather;
    // Start is called before the first frame update
    void Start()
    {
        sundayTile = new TileBase[9];
        rainyTile = new TileBase[9];
        for (int i = 0;i<9; i++)
        {
            sundayTile[i] = Resources.Load<TileBase>("Tilemap/sundayMaptile" + i);
            rainyTile[i] = Resources.Load<TileBase>("Tilemap/rainyMaptile"+ i);
        }

        for(int i = 0; i <sundayTile.Length;i++)
        {
            sundayTileDictionary.Add(i, sundayTile[i]);
            rainyTileDictionary.Add(i, rainyTile[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather();
    }

    void ChangeWeather()
    {
        if(currentWeather == WeatherState.sunday&&isChanging == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //ChangeTile();
                //StartCoroutine(ChangeTileDelay());
                StartCoroutine(ChangeWeatherDelay());
                //currentWeather = WeatherState.rainy;
            }
        }
        else if (currentWeather == WeatherState.rainy && isChanging == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //ChangeTile();
                //StartCoroutine(ChangeTileDelay());
                StartCoroutine(ChangeWeatherDelay());
                //currentWeather = WeatherState.sunday;
            }
        }
    }

    void ChangeTile()
    {
        Vector3Int startPosition = Vector3Int.FloorToInt(cameraTrans.position); // 起始位置
        BoundsInt bounds = new BoundsInt(startPosition, new Vector3Int(boundsSizeX, boundsSizeY, 0)); // 边界区域

        if(currentWeather == WeatherState.sunday)
        {
            
            for (int x = bounds.xMin-boundsSizeX; x <= bounds.xMax; x++)
            {
                
                for (int y = bounds.yMin - boundsSizeY; y <= bounds.yMax; y++)
                {
                    
                    //map.SetTile(new Vector3Int(x, y, 0), rainyTile[1]);
                    thisTileBase = map.GetTile(new Vector3Int(x, y, 0));
                    Debug.Log(thisTileBase);
                    if (map.HasTile(new Vector3Int(x, y, 0)))
                    {
                        
                        thisTileBase = map.GetTile(new Vector3Int(x, y, 0));
                        map.SetTile(new Vector3Int(x, y, 0), null); // 移除原有的图块
                        map.SetTile(new Vector3Int(x, y, 0),rainyTile[GetNumber(thisTileBase.name)]); // 添加新的图块
                    }
                }
            }
            
        }
        if (currentWeather == WeatherState.rainy)
        {
            for (int x = bounds.xMin - boundsSizeX; x <= bounds.xMax; x++)
            {
                for (int y = bounds.yMin - boundsSizeY; y <= bounds.yMax; y++)
                {
                    if (map.HasTile(new Vector3Int(x, y, 0)))
                    {
                        thisTileBase = map.GetTile(new Vector3Int(x, y, 0));
                        map.SetTile(new Vector3Int(x, y, 0), null); // 移除原有的图块
                        map.SetTile(new Vector3Int(x, y, 0), sundayTile[GetNumber(thisTileBase.name)]); // 添加新的图块
                    }
                }
            }
            
        }
    }
    IEnumerator ChangeWeatherDelay()
    {
        if(currentWeather == WeatherState.sunday)
        {
            isChanging= true;
            yield return ChangeTileDelay();
            isChanging= false;
            currentWeather = WeatherState.rainy;
        }
        else if (currentWeather == WeatherState.rainy)
        {
            isChanging= true;
            yield return ChangeTileDelay();
            isChanging= false;
            currentWeather = WeatherState.sunday;
        }
    }
    IEnumerator ChangeTileDelay()
    {
        Vector3Int startPosition = Vector3Int.FloorToInt(cameraTrans.position); // 起始位置
        BoundsInt bounds = new BoundsInt(startPosition, new Vector3Int(boundsSizeX, boundsSizeY, 0)); // 边界区域

        if (currentWeather == WeatherState.sunday)
        {

            for (int x = bounds.xMin - boundsSizeX; x <= bounds.xMax; x++)
            {

                for (int y = bounds.yMin - boundsSizeY; y <= bounds.yMax; y++)
                {

                    //map.SetTile(new Vector3Int(x, y, 0), rainyTile[1]);
                    thisTileBase = map.GetTile(new Vector3Int(x, y, 0));
                    Debug.Log(thisTileBase);
                    if (map.HasTile(new Vector3Int(x, y, 0)))
                    {
                        thisTileBase = map.GetTile(new Vector3Int(x, y, 0));
                        map.SetTile(new Vector3Int(x, y, 0), null); // 移除原有的图块
                        map.SetTile(new Vector3Int(x, y, 0), rainyTile[GetNumber(thisTileBase.name)]); // 添加新的图块
                        yield return new WaitForSeconds(0.01f);
                    }
                }
            }

        }
        if (currentWeather == WeatherState.rainy)
        {
            for (int x = bounds.xMin - boundsSizeX; x <= bounds.xMax; x++)
            {
                for (int y = bounds.yMin - boundsSizeY; y <= bounds.yMax; y++)
                {
                    if (map.HasTile(new Vector3Int(x, y, 0)))
                    {
                        thisTileBase = map.GetTile(new Vector3Int(x, y, 0));
                        map.SetTile(new Vector3Int(x, y, 0), null); // 移除原有的图块
                        map.SetTile(new Vector3Int(x, y, 0), sundayTile[GetNumber(thisTileBase.name)]); // 添加新的图块
                        yield return new WaitForSeconds(0.01f);
                    }
                }
            }

        }
    }
    int  GetNumber(string objName)
    {
        string objectName = objName; 

        Regex regex = new Regex(@"\d+"); // 创建正则表达式对象，并指定要匹配的模式（这里是匹配连续的数字）
        Match match = regex.Match(objectName); // 进行匹配操作

        if (match.Success)
        { 
            int number = int.Parse(match.Value);
            return number;
        }
        else
        {
            return 0;
        }
    }
}
